using System.Data;
using System.Text.RegularExpressions;

namespace Fabrics
{
    internal class MySqlSchema : ISchema
    {
        public Model.Database GetSchema(string connectionString, Model.Database.DatabaseType type)
        {
            Model.Database database = new Model.Database();
            database.ConnectionString = connectionString;
            database.Type = type;

            //得到获取MySql结构的语句
            string dbName;
            string connStr = database.ConnectionString;
            Match mDatabase = Regex.Match(connStr, @"Database=(?<Database>[^\;]*);");
            if (mDatabase.Success)
            {
                dbName = mDatabase.Groups["Database"].Value;
                connStr = connStr.Replace(string.Format("Database={0};", dbName), "Database=information_schema;");
            }
            else
            {
                return null;
            }

            DBUtility.DBHelper dbHelper = new DBUtility.DBHelper(DBUtility.DBHelper.DatabaseTypes.MySql, connStr);

            // 获取表
            DataSet dsTables = dbHelper.ExecuteQuery(CommandType.Text,
                string.Format("select distinct TABLE_NAME from information_schema.TABLES where TABLE_SCHEMA='{0}' and TABLE_TYPE='BASE TABLE'", dbName),
                null);
            foreach (DataRow rTable in dsTables.Tables[0].Rows)
            {
                Model.Table table = GetTable(dbHelper, dbName, rTable);
                table.Fields.Sort();
                database.Tables.Add(table);
            }

            // 获取视图
            DataSet dsViews = dbHelper.ExecuteQuery(CommandType.Text,
                string.Format("select distinct TABLE_NAME from information_schema.TABLES where TABLE_SCHEMA='{0}' and TABLE_TYPE='VIEW'", dbName),
                null);
            foreach (DataRow rView in dsViews.Tables[0].Rows)
            {
                Model.Table view = GetTable(dbHelper, dbName, rView);
                view.Fields.Sort();
                database.Views.Add(view);
            }

            // 获取存储过程
            DataSet dsStoreProcedure = dbHelper.ExecuteQuery(CommandType.Text,
                string.Format("select distinct SPECIFIC_NAME from ROUTINES where ROUTINE_SCHEMA='{0}'", dbName),
                null);
            foreach (DataRow r in dsStoreProcedure.Tables[0].Rows)
            {
                database.StoreProcedures.Add(r[0].ToString());
            }

            return database;
        }

        private static Model.Table GetTable(DBUtility.DBHelper dbHelper, string dbName, DataRow rTable)
        {
            string tableName = rTable[0].ToString();

            Model.Table table = new Model.Table();
            table.Name = tableName;

            // 对每个表取字段属性
            DataSet dsColumns = dbHelper.ExecuteQuery(CommandType.Text,
               string.Format("select * from COLUMNS where TABLE_SCHEMA='{0}' and TABLE_NAME='{1}'", dbName, tableName),
               null);

            foreach (DataRow rField in dsColumns.Tables[0].Rows)
            {
                Model.Field field = new Model.Field();
                field.IsIdentifier = rField["EXTRA"].ToString().ToLower() == "auto_increment";
                field.IsKeyField = rField["COLUMN_KEY"].ToString().ToLower() == "pri";
                field.AllowNull = rField["IS_NULLABLE"].ToString().ToLower() == "yes";

                field.MySqlTypeString = SchemeHelper.GetString(rField["DATA_TYPE"]);
                field.DefaultValue = SchemeHelper.GetString(rField["COLUMN_DEFAULT"]);
                field.FieldDescn = SchemeHelper.GetString(rField["COLUMN_COMMENT"]);
                field.FieldLength = SchemeHelper.GetLong(rField["CHARACTER_MAXIMUM_LENGTH"]);
                field.FieldName = SchemeHelper.GetString(rField["COLUMN_NAME"]);
                field.FieldNumber = SchemeHelper.GetInt(rField["ORDINAL_POSITION"]);
                table.Fields.Add(field);
            }
            return table;
        }
    }
}
