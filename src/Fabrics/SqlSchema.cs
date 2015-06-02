using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Fabrics
{
    internal class SqlSchema : ISchema
    {
        public Model.Database GetSchema(string connectionString, Model.Database.DatabaseType type)
        {
            Model.Database database = new Model.Database();
            database.ConnectionString = connectionString;
            database.Type = type;

            using (SqlConnection connection = new SqlConnection())
            {
                DBUtility.DBHelper dbHelper = new DBUtility.DBHelper(DBUtility.DBHelper.DatabaseTypes.Sql, database.ConnectionString);

                DataSet ds = dbHelper.ExecuteQuery(CommandType.Text,
                    GetSql(database.Type, SchemeHelper.SchemaType.Table), null);
                database.Tables = GetSQLTableList(ds);

                ds = dbHelper.ExecuteQuery(CommandType.Text,
                    GetSql(database.Type, SchemeHelper.SchemaType.View), null);
                database.Views = GetSQLTableList(ds);

                ds = dbHelper.ExecuteQuery(CommandType.Text, SqlForGetStoreProcedures, null);
                database.StoreProcedures = GetSQLStoreProcedureList(ds);

                return database;
            }
        }

        /// <summary>
        /// 取得表结构的SQL语句
        /// </summary>
        private string GetSql(Model.Database.DatabaseType type, SchemeHelper.SchemaType schemaType)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT d.name N'TableName',a.colorder N'FieldNumber',a.name N'FieldName', ");
            strSql.Append("(case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '1'else '0' end) N'IsIdentifier',");
            strSql.Append("(case when (SELECT count(*) ");
            strSql.Append(" FROM sysobjects WHERE (name in (SELECT name FROM sysindexes ");
            strSql.Append(" WHERE (id = a.id) AND (indid in (SELECT indid FROM sysindexkeys");
            strSql.Append(" WHERE (id = a.id) AND (colid in (SELECT colid FROM syscolumns");
            strSql.Append(" WHERE (id = a.id) AND (name = a.name))))))) AND (xtype = 'PK'))>0 ");
            strSql.Append(" then '1' else '0' end) N'IsKeyField', b.name N'FieldType',a.length N'FieldSize', ");
            strSql.Append(" COLUMNPROPERTY(a.id,a.name,'PRECISION') as N'FieldLength', ");
            strSql.Append(" isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0) as N'DecimalDigits', ");
            strSql.Append(" (case when a.isnullable=1 then '1'else '0' end) N'AllowNull', isnull(e.text,'') N'DefaultValue', ");
            strSql.Append(" isnull(g.[value],'') AS N'FieldDescn' ");
            strSql.Append(" FROM syscolumns a left join systypes b on a.xtype=b.xusertype inner join sysobjects d  on a.id=d.id");

            switch (schemaType)
            {
                case SchemeHelper.SchemaType.View:
                    strSql.Append(" and d.xtype='V'");
                    break;
                case SchemeHelper.SchemaType.Table:
                default:
                    strSql.Append(" and d.xtype='U'");
                    break;
            }

            strSql.Append("and d.name<>'dtproperties' left join syscomments e on a.cdefault=e.id");

            if (type == Model.Database.DatabaseType.Sql2005)
            {
                strSql.Append(" left join sys.extended_properties g on a.id=g.major_id AND a.colid = g.minor_id order by object_name(a.id),a.colorder");
            }
            else
            {
                strSql.Append(" left join sysproperties g on a.id=g.id AND a.colid = g.smallid order by object_name(a.id),a.colorder ");
            }

            return strSql.ToString();
        }

        /// <summary>
        /// SqlServer中取得存储过程的SQL语句
        /// </summary>
        private string SqlForGetStoreProcedures = "select distinct(name) from sysobjects where type='p' ";

        private List<Model.Table> GetSQLTableList(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            List<Model.Table> lTable = new List<Model.Table>();
            foreach (DataRow r in dt.Rows)
            {
                Model.Field model = GetField(r);
                bool hasTable = false;
                foreach (Model.Table modelTable in lTable)
                {
                    if (model.TableName == modelTable.Fields[0].TableName)
                    {
                        modelTable.Fields.Add(model);
                        hasTable = true;
                        break;
                    }
                }
                if (!hasTable)
                {
                    Model.Table newTable = new Model.Table();
                    newTable.Name = model.TableName;
                    List<Model.Field> lFields = new List<Model.Field>();
                    lFields.Add(model);
                    newTable.Fields = lFields;
                    lTable.Add(newTable);
                }
            }

            foreach (Model.Table table in lTable)
            {
                table.Fields.Sort();
            }
            return lTable;
        }

        /// <summary>
        /// 获取所有存储过程集合
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private List<string> GetSQLStoreProcedureList(DataSet ds)
        {
            List<string> storeProcedures = new List<string>();
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                storeProcedures.Add(r[0].ToString());
            }
            return storeProcedures;
        }

        private Model.Field GetField(DataRow r)
        {
            Model.Field model = new Model.Field();
            model.AllowNull = SchemeHelper.GetBool(r["AllowNull"]);
            model.DefaultValue = SchemeHelper.GetString(r["DefaultValue"]);
            model.FieldDescn = SchemeHelper.GetString(r["FieldDescn"]);
            model.FieldLength = SchemeHelper.GetInt(r["FieldLength"]);
            model.FieldName = SchemeHelper.GetString(r["FieldName"]);
            model.FieldNumber = SchemeHelper.GetInt(r["FieldNumber"]);
            model.FieldSize = SchemeHelper.GetInt(r["FieldSize"]);
            model.SqlTypeString = SchemeHelper.GetString(r["FieldType"]);
            model.IsIdentifier = SchemeHelper.GetBool(r["IsIdentifier"]);
            model.IsKeyField = SchemeHelper.GetBool(r["IsKeyField"]);
            model.TableName = SchemeHelper.GetString(r["TableName"]);
            return model;
        }
    }
}
