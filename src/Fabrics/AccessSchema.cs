using System;
using System.Data;
using System.Data.OleDb;

namespace Fabrics
{
    internal class AccessSchema : ISchema
    {
        public Model.Database GetSchema(string connectionString, Model.Database.DatabaseType type)
        {
            Model.Database database = new Model.Database();
            database.ConnectionString = connectionString;
            database.Type = type;

            //获取所有表
            DataTable dtAllTable = GetDbSchema(database.ConnectionString, OleDbSchemaGuid.Tables, new object[] { null, null, null, "table" });
            foreach (DataRow rt in dtAllTable.Rows)
            {
                Model.Table tb = new Model.Table();
                tb.Name = rt["TABLE_NAME"].ToString();
                DataTable dtColumns = GetDbSchema(database.ConnectionString, OleDbSchemaGuid.Columns, new object[] { null, null, tb.Name });
                foreach (DataRow rc in dtColumns.Rows)
                {
                    tb.Fields.Add(GetField(database.ConnectionString, tb.Name, rc));
                }
                tb.Fields.Sort();
                database.Tables.Add(tb);
            }

            //获取所有视图
            DataTable dtAllView = GetDbSchema(database.ConnectionString, OleDbSchemaGuid.Views, null);
            foreach (DataRow rv in dtAllView.Rows)
            {
                Model.Table tb = new Model.Table();
                tb.Name = rv["TABLE_NAME"].ToString();
                DataTable dtColumns = GetDbSchema(database.ConnectionString, OleDbSchemaGuid.Columns, new object[] { null, null, tb.Name });
                foreach (DataRow rc in dtColumns.Rows)
                {
                    tb.Fields.Add(GetField(database.ConnectionString, tb.Name, rc));
                }
                tb.Fields.Sort();
                database.Views.Add(tb);
            }

            //获取所有存储过程
            DataTable dtAllStoreProcedure = GetDbSchema(database.ConnectionString, OleDbSchemaGuid.Procedures, null);
            foreach (DataRow rsp in dtAllStoreProcedure.Rows)
            {
                database.StoreProcedures.Add(rsp["PROCEDURE_NAME"].ToString());
            }
            return database;
        }

        private Model.Field GetField(string connectionString, string tbName, DataRow r)
        {
            Model.Field model = new Model.Field();
            model.AllowNull = SchemeHelper.GetBool(r["IS_NULLABLE"]);
            model.DefaultValue = SchemeHelper.GetString(r["COLUMN_DEFAULT"]);
            model.FieldDescn = SchemeHelper.GetString(r["DESCRIPTION"]);
            model.FieldLength = SchemeHelper.GetInt(r["CHARACTER_MAXIMUM_LENGTH"]);
            model.FieldName = SchemeHelper.GetString(r["COLUMN_NAME"]);
            model.FieldNumber = SchemeHelper.GetInt(r["ORDINAL_POSITION"]);
            model.FieldSize = SchemeHelper.GetInt(r["CHARACTER_OCTET_LENGTH"]);
            model.OleDbTypeString = SchemeHelper.GetString(r["DATA_TYPE"]);
            model.IsIdentifier = SchemeHelper.GetInt(r["COLUMN_FLAGS"]) == 90 && SchemeHelper.GetInt(r["DATA_TYPE"]) == 3;

            DataTable dtPrimanyKey = GetDbSchema(connectionString, OleDbSchemaGuid.Primary_Keys, null);
            foreach (DataRow rp in dtPrimanyKey.Rows)
            {
                if (rp[2].ToString() == tbName && rp[3].ToString() == model.FieldName)
                {
                    model.IsKeyField = true;
                }
            }

            model.TableName = SchemeHelper.GetString(r["TABLE_NAME"]);
            return model;
        }

        private DataTable GetDbSchema(string connString, Guid schema, object[] restrictions)
        {
            OleDbConnection myConn = new OleDbConnection(connString);
            myConn.Open();
            DataTable table1 = myConn.GetOleDbSchemaTable(schema, restrictions);
            myConn.Close();
            return table1;
        }
    }
}
