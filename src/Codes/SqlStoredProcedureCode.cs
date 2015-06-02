using System.Text;

namespace Codes
{
    public class SqlStoredProcedureCode : CodeHelper
    {
        /// <summary>
        /// 创建存储过程
        /// </summary>
        public static string GetSqlStoredProcedureCode(Model.Table table, Model.CodeStyle style)
        {
            StringBuilder code = new StringBuilder();
            Add(table, code, style);
            code.AppendLine();
            Update(table, code);
            code.AppendLine();
            Delete(table, code);
            code.AppendLine();
            GetModel(table, code);
            code.AppendLine();
            Exists(table, code);
            code.AppendLine();
            GetCount(table, code);
            code.AppendLine();
            GetAllList(table, code);
            return code.ToString();
        }

        private static void GetAllList(Model.Table table, StringBuilder code)
        {
            AppendFormatLine(code, 0, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_{0}_GetAllList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)",
                table.Name);
            AppendFormatLine(code, 0, "drop procedure [dbo].[sp_{0}_GetAllList]", table.Name);
            AppendFormatLine(code, 0, "GO");
            code.AppendLine(CommonCode.GetSQLCopyrightCode("得到所有实体"));
            AppendFormatLine(code, 0, "CREATE PROCEDURE sp_{0}_GetAllList", table.Name);
            AppendFormatLine(code, 0, "AS");
            code.AppendLine();

            AppendFormatLine(code, 0, "SELECT * FROM [{0}]", table.Name);

            code.AppendLine();
            AppendFormatLine(code, 0, "GO");
        }

        private static void GetModel(Model.Table table, StringBuilder code)
        {
            AppendFormatLine(code, 0, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_{0}_GetModel]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)",
                table.Name);
            AppendFormatLine(code, 0, "drop procedure [dbo].[sp_{0}_GetModel]", table.Name);
            AppendFormatLine(code, 0, "GO");
            code.AppendLine(CommonCode.GetSQLCopyrightCode("得到一个实体"));
            AppendFormatLine(code, 0, "CREATE PROCEDURE sp_{0}_GetModel", table.Name);
            GetArgumentsOfSQL(table, code);
            AppendFormatLine(code, 0, "AS");
            code.AppendLine();
            AppendFormatLine(code, 0, "SELECT * FROM {0}", table.Name);
            AppendFormatLine(code, 0, "WHERE");
            AppendFormatLine(code, 1, "{0}", GetConditonOfSql(table));

            code.AppendLine();
            AppendFormatLine(code, 0, "GO");
        }

        private static void Exists(Model.Table table, StringBuilder code)
        {
            AppendFormatLine(code, 0, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_{0}_Exists]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)",
                table.Name);
            AppendFormatLine(code, 0, "drop procedure [dbo].[sp_{0}_Exists]", table.Name);
            AppendFormatLine(code, 0, "GO");
            code.Append(CommonCode.GetSQLCopyrightCode("是否已经存在"));
            AppendFormatLine(code, 0, "CREATE PROCEDURE sp_{0}_Exists", table.Name);
            GetArgumentsOfSQL(table, code);
            AppendFormatLine(code, 0, "AS");
            code.AppendLine();
            AppendFormatLine(code, 0, "SELECT count(*) FROM [{0}] WHERE {1}", table.Name, GetConditonOfSql(table));
            code.AppendLine();
            AppendFormatLine(code, 0, "GO");
        }

        private static void GetCount(Model.Table table, StringBuilder code)
        {
            AppendFormatLine(code, 0, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_{0}_GetCount]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)",
                table.Name);
            AppendFormatLine(code, 0, "drop procedure [dbo].[sp_{0}_GetCount]", table.Name);
            AppendFormatLine(code, 0, "GO");
            code.Append(CommonCode.GetSQLCopyrightCode("是否已经存在"));
            AppendFormatLine(code, 0, "CREATE PROCEDURE sp_{0}_GetCount", table.Name);
            AppendFormatLine(code, 0, "AS");
            code.AppendLine();
            AppendFormatLine(code, 0, "SELECT count(*) FROM [{0}]", table.Name);
            code.AppendLine();
            AppendFormatLine(code, 0, "GO");
        }

        private static void Delete(Model.Table table, StringBuilder code)
        {
            AppendFormatLine(code, 0, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_{0}_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)",
                table.Name);
            AppendFormatLine(code, 0, "drop procedure [dbo].[sp_{0}_Delete]", table.Name);
            AppendFormatLine(code, 0, "GO");
            code.Append(CommonCode.GetSQLCopyrightCode("删除一条记录"));
            AppendFormatLine(code, 0, "CREATE PROCEDURE sp_{0}_Delete", table.Name);
            GetArgumentsOfSQL(table, code);
            AppendFormatLine(code, 0, "AS");
            code.AppendLine();
            AppendFormatLine(code, 0, "DELETE FROM {0}", table.Name);
            AppendFormatLine(code, 0, "WHERE ");
            AppendFormatLine(code, 1, "{0}", GetConditonOfSql(table));
            code.AppendLine();
            AppendFormatLine(code, 0, "GO");
        }

        private static void Update(Model.Table table, StringBuilder code)
        {
            AppendFormatLine(code, 0, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_{0}_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)",
                table.Name);
            AppendFormatLine(code, 0, "drop procedure [dbo].[sp_{0}_Update]", table.Name);
            AppendFormatLine(code, 0, "GO");
            code.Append(CommonCode.GetSQLCopyrightCode("修改一条记录"));
            AppendFormatLine(code, 0, "CREATE PROCEDURE [sp_{0}_Update]", table.Name);

            foreach (Model.Field field in table.Fields)
            {
                if (ShouldAddLength(field))
                    AppendFormatLine(code, 1, "{0} {1}({2}),", GetSqlStoreProcedureParameter(field), field.SqlTypeString, field.FieldLength);
                else
                    AppendFormatLine(code, 1, "{0} {1},", GetSqlStoreProcedureParameter(field), field.SqlTypeString);
            }
            code.Remove(code.ToString().LastIndexOf(","), 1);

            AppendFormatLine(code, 0, "AS");
            code.AppendLine();
            AppendFormatLine(code, 0, "UPDATE [{0}]", table.Name);
            AppendFormatLine(code, 0, "SET");

            foreach (Model.Field field in table.UpdateRows)
            {
                AppendFormatLine(code, 1, "[{0}]={1},", field.FieldName, GetSqlStoreProcedureParameter(field));
            }
            code.Remove(code.ToString().LastIndexOf(","), 1);

            AppendFormatLine(code, 0, "WHERE");
            AppendFormatLine(code, 1, "{0}", GetConditonOfSql(table));
            code.AppendLine();
            AppendFormatLine(code, 0, "GO");
        }

        private static void Add(Model.Table table, StringBuilder code, Model.CodeStyle style)
        {
            AppendFormatLine(code, 0, "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_{0}_Add]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)",
                table.Name);
            AppendFormatLine(code, 0, "drop procedure [dbo].[sp_{0}_Add]", table.Name);
            AppendFormatLine(code, 0, "GO");
            code.Append(CommonCode.GetSQLCopyrightCode("增加一条记录"));
            AppendFormatLine(code, 0, "CREATE PROCEDURE sp_{0}_Add", table.Name);

            foreach (Model.Field field in table.Fields)
            {
                if (ShouldBeParameter(table, field, style))
                {
                    if (ShouldAddLength(field))
                        AppendFormatLine(code, 1, "{0} {1}({2}),", GetSqlStoreProcedureParameter(field), field.SqlTypeString, field.FieldLength);
                    else
                        AppendFormatLine(code, 1, "{0} {1},", GetSqlStoreProcedureParameter(field), field.SqlTypeString);
                }
            }
            code.Remove(code.ToString().LastIndexOf(","), 1);

            AppendFormatLine(code, 0, "AS");
            code.AppendLine();
            AppendFormatLine(code, 0, "INSERT INTO [{0}](", table.Name);

            foreach (Model.Field field in table.Fields)
            {
                if (ShouldBeParameter(table, field, style))
                {
                    AppendFormatLine(code, 1, "[{0}],", field.FieldName);
                }
            }
            code.Remove(code.ToString().LastIndexOf(","), 1);

            AppendFormatLine(code, 0, ")VALUES(");

            foreach (Model.Field field in table.Fields)
            {
                if (ShouldBeParameter(table, field, style))
                {
                    AppendFormatLine(code, 1, "{0},", GetSqlStoreProcedureParameter(field));
                }
            }
            code.Remove(code.ToString().LastIndexOf(","), 1);

            AppendFormatLine(code, 0, ")");

            AppendFormatLine(code, 0, "GO");
        }
    }
}
