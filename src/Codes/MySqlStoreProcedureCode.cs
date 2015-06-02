using System.Text;

namespace Codes
{
    public class MySqlStoreProcedureCode : CodeHelper
    {
        public static string GetMySqlStoreProcedureCode(Model.Table table, Model.CodeStyle style)
        {
            StringBuilder code = new StringBuilder();

            Add(table, code, style);

            code.AppendLine();

            Update(table, code);

            code.AppendLine();

            Delete(table, code);

            code.AppendLine();

            Exists(table, code);

            code.AppendLine();

            GetCount(table, code);

            code.AppendLine();

            GetModel(table, code);

            code.AppendLine();

            GetAllList(table, code);

            return code.ToString();
        }

        private static void GetAllList(Model.Table table, StringBuilder code)
        {
            AppendFormatLine(code, 0, "DELIMITER $$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DROP PROCEDURE IF EXISTS `sp_{0}_GetAllList`$$", table.Name);
            code.AppendLine();
            AppendFormatLine(code, 0, "CREATE PROCEDURE `sp_{0}_GetAllList`()", table.Name);
            AppendFormatLine(code, 0, "BEGIN");
            AppendFormatLine(code, 1, "SELECT * FROM {0};", table.Name);
            AppendFormatLine(code, 0, "END$$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DELIMITER ;");
        }

        private static void GetModel(Model.Table table, StringBuilder code)
        {
            AppendFormatLine(code, 0, "DELIMITER $$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DROP PROCEDURE IF EXISTS `sp_{0}_GetModel`$$", table.Name);
            code.AppendLine();
            AppendFormatLine(code, 0, "CREATE PROCEDURE `sp_{0}_GetModel`", table.Name);
            AppendFormatLine(code, 0, "(");
            GetArgumentsOfSQL(table, code);
            AppendFormatLine(code, 0, ")");
            AppendFormatLine(code, 0, "BEGIN");
            AppendFormatLine(code, 1, "SELECT * FROM {0} WHERE {1};", table.Name, GetConditonOfMySql(table));
            AppendFormatLine(code, 0, "END$$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DELIMITER ;");
        }

        private static void Exists(Model.Table table, StringBuilder code)
        {
            AppendFormatLine(code, 0, "DELIMITER $$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DROP PROCEDURE IF EXISTS `sp_{0}_Exists`$$", table.Name);
            code.AppendLine();
            AppendFormatLine(code, 0, "CREATE PROCEDURE `sp_{0}_Exists`", table.Name);
            AppendFormatLine(code, 0, "(");
            GetArgumentsOfSQL(table, code);
            AppendFormatLine(code, 0, ")");
            AppendFormatLine(code, 0, "BEGIN");
            AppendFormatLine(code, 1, "SELECT COUNT(*) FROM {0} WHERE {1};", table.Name, GetConditonOfMySql(table));
            AppendFormatLine(code, 0, "END$$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DELIMITER ;");
        }

        private static void GetCount(Model.Table table, StringBuilder code)
        {
            AppendFormatLine(code, 0, "DELIMITER $$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DROP PROCEDURE IF EXISTS `sp_{0}_GetCount`$$", table.Name);
            code.AppendLine();
            AppendFormatLine(code, 0, "CREATE PROCEDURE `sp_{0}_GetCount`", table.Name);
            AppendFormatLine(code, 0, "BEGIN");
            AppendFormatLine(code, 1, "SELECT COUNT(*) FROM {0};", table.Name);
            AppendFormatLine(code, 0, "END$$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DELIMITER ;");
        }

        private static void Delete(Model.Table table, StringBuilder code)
        {
            AppendFormatLine(code, 0, "DELIMITER $$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DROP PROCEDURE IF EXISTS `sp_{0}_Delete`$$", table.Name);
            code.AppendLine();
            AppendFormatLine(code, 0, "CREATE PROCEDURE `sp_{0}_Delete`", table.Name);
            AppendFormatLine(code, 0, "(");
            GetArgumentsOfSQL(table, code);
            AppendFormatLine(code, 0, ")");
            AppendFormatLine(code, 0, "BEGIN");
            AppendFormatLine(code, 0, "DELETE FROM {0}", table.Name);
            AppendFormatLine(code, 0, "WHERE ");
            AppendFormatLine(code, 1, "{0};", GetConditonOfMySql(table));
            AppendFormatLine(code, 0, "END$$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DELIMITER ;");
        }

        private static void Update(Model.Table table, StringBuilder code)
        {
            AppendFormatLine(code, 0, "DELIMITER $$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DROP PROCEDURE IF EXISTS `sp_{0}_Update`$$", table.Name);
            code.AppendLine();
            AppendFormatLine(code, 0, "CREATE PROCEDURE `sp_{0}_Update`", table.Name);
            AppendFormatLine(code, 0, "(");

            foreach (Model.Field field in table.Fields)
            {
                if (ShouldAddLength(field))
                    AppendFormatLine(code, 1, "IN {0} {1}({2}),",GetMySqlStoreProcedureParameter( field), field.MySqlTypeString, field.FieldLength);
                else
                    AppendFormatLine(code, 1, "IN {0} {1},", GetMySqlStoreProcedureParameter(field), field.MySqlTypeString);
            }
            code.Remove(code.ToString().LastIndexOf(","), 1);

            AppendFormatLine(code, 0, ")");
            AppendFormatLine(code, 0, "BEGIN");
            AppendFormatLine(code, 0, "UPDATE {0}", table.Name);
            AppendFormatLine(code, 0, "SET");

            foreach (Model.Field field in table.UpdateRows)
            {
                AppendFormatLine(code, 1, "{0}={1},", field.FieldName, GetMySqlStoreProcedureParameter(field));
            }
            code.Remove(code.ToString().LastIndexOf(","), 1);

            AppendFormatLine(code, 0, "WHERE");
            AppendFormatLine(code, 1, "{0};", GetConditonOfMySql(table));
            AppendFormatLine(code, 0, "END$$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DELIMITER ;");
        }

        private static void Add(Model.Table table, StringBuilder code, Model.CodeStyle style)
        {
            AppendFormatLine(code, 0, "DELIMITER $$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DROP PROCEDURE IF EXISTS `sp_{0}_Add`$$", table.Name);
            code.AppendLine();
            AppendFormatLine(code, 0, "CREATE PROCEDURE `sp_{0}_Add`", table.Name);
            AppendFormatLine(code, 0, "(");

            foreach (Model.Field field in table.Fields)
            {
                if (ShouldBeParameter(table, field, style))
                {
                    if (ShouldAddLength(field))
                        AppendFormatLine(code, 1, "{0} {1}({2}),",GetMySqlStoreProcedureParameter( field), field.MySqlTypeString, field.FieldLength);
                    else
                        AppendFormatLine(code, 1, "{0} {1},", GetMySqlStoreProcedureParameter(field), field.MySqlTypeString);
                }
            }
            code.Remove(code.ToString().LastIndexOf(","), 1);

            AppendFormatLine(code, 0, ")");
            AppendFormatLine(code, 0, "BEGIN");
            AppendFormatLine(code, 0, "INSERT INTO {0}(", table.Name);

            foreach (Model.Field field in table.Fields)
            {
                if (ShouldBeParameter(table, field, style))
                {
                    AppendFormatLine(code, 1, "{0},", field.FieldName);
                }
            }
            code.Remove(code.ToString().LastIndexOf(","), 1);

            AppendFormatLine(code, 0, ")VALUES(");

            foreach (Model.Field field in table.Fields)
            {
                if (ShouldBeParameter(table, field, style))
                {
                    AppendFormatLine(code, 1, "{0},", GetMySqlStoreProcedureParameter(field));
                }
            }
            code.Remove(code.ToString().LastIndexOf(","), 1);

            AppendFormatLine(code, 0, ");");
            AppendFormatLine(code, 0, "END$$");
            code.AppendLine();
            AppendFormatLine(code, 0, "DELIMITER ;");
        }

        private static void GetArguments(Model.Table table, StringBuilder code)
        {
            foreach (Model.Field field in table.ConditionRows)
            {
                if (ShouldAddLength(field))
                    AppendFormatLine(code, 1, "{0} {1}({2}),", GetSqlStoreProcedureParameter(field), field.SqlTypeString, field.FieldLength);
                else
                    AppendFormatLine(code, 1, "{0} {1},", GetSqlStoreProcedureParameter(field), field.SqlTypeString);
            }
            code.Remove(code.Length - 1, 1);
        }
    }
}
