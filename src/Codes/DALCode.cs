using System.Data;
using System.Text;

namespace Codes
{
    public partial class DALCode : CodeHelper
    {
        /// <summary>
        /// 生成DALHelper
        /// </summary>
        public static string GetDALHelperCode(Model.Database db, Model.CodeStyle style)
        {
            return ReadFromTemplate(Model.CreateStyle.CURRENT_PATH + "\\DAL\\DALHelper.Template", db, null, style);
        }

        /// <summary>
        /// 生成DAL数据层
        /// </summary>
        public static string GetDALCode(Model.Database db, Model.Table table, Model.CodeStyle style)
        {
            StringBuilder code = new StringBuilder(CommonCode.GetCSharpCopyrightCode());

            Head(db, table, style, code);

            AppendFormatLine(code, 0, "{");
            AppendFormatLine(code, 1, "/// <summary>");
            AppendFormatLine(code, 1, "/// 数据访问类 {0}", table.Name);
            AppendFormatLine(code, 1, "/// </summary>");

            if (style.SlnFrame == Model.CodeStyle.SlnFrames.Factory)
                AppendFormatLine(code, 1, "public partial class {0} : DALHelper, {1}.I{0}", table.Name, style.IDALNameSpace);
            else
                AppendFormatLine(code, 1, "public partial class {0} : DALHelper", table.Name);

            AppendFormatLine(code, 1, "{");

            Add(db, table, style, code);

            code.AppendLine();

            Update(db, table, style, code);

            code.AppendLine();

            Delete(db, table, style, code);

            code.AppendLine();

            GetModel(db, table, style, code);

            code.AppendLine();

            Exists(db, table, style, code);

            code.AppendLine();

            GetCount(db, table, style, code);

            code.AppendLine();

            GetList(db, table, style, code);

            code.AppendLine();

            GetPageList(db, table, style, code);

            code.AppendLine();

            AppendFormatLine(code, 2, "#region -------- 私有方法 --------");

            GetModelPrivate(db, table, style, code);

            code.AppendLine();

            GetListPrivate(db, table, style, code);

            if (style.PageStyle == Model.CodeStyle.PageStyles.DataReader)
            {
                code.AppendLine();
                GetPageListPrivate(db, table, style, code);
            }

            AppendFormatLine(code, 2, "#endregion");

            AppendFormatLine(code, 1, "}");
            AppendFormatLine(code, 0, "}");

            return code.ToString();
        }

        /// <summary>
        /// 生成DAL数据层(Internal)
        /// </summary>
        public static string GetInternalDALCode(Model.Database db, Model.Table table, Model.CodeStyle style)
        {
            StringBuilder code = new StringBuilder(CommonCode.GetCSharpCopyrightCode());

            Head(db, table, style, code);

            AppendFormatLine(code, 0, "{");
            AppendFormatLine(code, 1, "/// <summary>");
            AppendFormatLine(code, 1, "/// 数据访问类 {0} ，此类请勿动，以方便表字段更改时重新生成覆盖", table.Name);
            AppendFormatLine(code, 1, "/// </summary>");

            AppendFormatLine(code, 1, "public partial class {0}", table.Name);

            AppendFormatLine(code, 1, "{");

            PrepareAddCommand(db, table, style, code);

            code.AppendLine();

            PrepareUpdateCommand(db, table, style, code);

            code.AppendLine();

            PrepareDeleteCommand(db, table, style, code);

            code.AppendLine();

            PrepareExistCommand(db, table, style, code);

            code.AppendLine();

            PrepareGetModelCommand(db, table, style, code);

            code.AppendLine();

            PrepareModel(db, table, style, code);

            AppendFormatLine(code, 1, "}");
            AppendFormatLine(code, 0, "}");

            return code.ToString();
        }

        private static void Head(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 0, "using System;");
            AppendFormatLine(code, 0, "using System.Collections.Generic;");
            AppendFormatLine(code, 0, "using System.Data;");
            AppendFormatLine(code, 0, "using System.Data.Common;");
            AppendFormatLine(code, 0, "using System.Text;");
            code.AppendLine();
            AppendFormatLine(code, 0, "namespace {0}", style.DALNameSpace);
        }

        private static void GetModel(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 得到一个对象实体");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "public {0}.{1} GetModel({2})",
                style.ModelNameSpace,
                table.Name,
                GetArgumentsOfFunction(table));
            AppendFormatLine(code, 2, "{");

            if (style.CmdType != CommandType.StoredProcedure)
                AppendFormatLine(code, 3, "StringBuilder strSql;");

            AppendFormatLine(code, 3, "DbParameter[] cmdParms;");

            switch (style.CmdType)
            {
                case CommandType.StoredProcedure:
                    AppendFormatLine(code, 3, "PrepareGetModelCommand({0}, out cmdParms);", GetArgumentValuesOfFunction(table));
                    AppendFormatLine(code, 3, "using (DbDataReader dr = {0}.ExecuteReader(CommandType.StoredProcedure,  \"sp_{1}_GetModel\", cmdParms))",
                        style.DBHelperName, table.Name);
                    break;
                case CommandType.TableDirect:
                case CommandType.Text:
                default:
                    AppendFormatLine(code, 3, "PrepareGetModelCommand({0}, out strSql, out cmdParms);", GetArgumentValuesOfFunction(table));
                    AppendFormatLine(code, 3, "using (DbDataReader dr = {0}.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))",
                        style.DBHelperName);
                    break;
            }
            AppendFormatLine(code, 3, "{");
            AppendFormatLine(code, 4, "if (dr.Read())");
            AppendFormatLine(code, 4, "{");
            AppendFormatLine(code, 5, "return GetModel(dr);");
            AppendFormatLine(code, 4, "}");
            AppendFormatLine(code, 4, "return null;");
            AppendFormatLine(code, 3, "}");
            AppendFormatLine(code, 2, "}");
        }

        private static void PrepareGetModelCommand(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 为获取一条数据准备参数");
            AppendFormatLine(code, 2, "/// </summary>");

            switch (style.CmdType)
            {
                case CommandType.StoredProcedure:
                    AppendFormatLine(code, 2, "internal static void PrepareGetModelCommand({0}, out DbParameter[] cmdParms)", GetArgumentsOfFunction(table));
                    break;
                case CommandType.TableDirect:
                case CommandType.Text:
                    AppendFormatLine(code, 2, "internal static void PrepareGetModelCommand({0}, out StringBuilder strSql, out DbParameter[] cmdParms)", GetArgumentsOfFunction(table));
                    break;
                default:
                    break;
            }
            AppendFormatLine(code, 2, "{");

            if (style.CmdType != CommandType.StoredProcedure)
            {
                AppendFormatLine(code, 3, "strSql = new StringBuilder();");
                AppendFormatLine(code, 3, "strSql.Append(\"SELECT * FROM {0} \");", table.Name);
                AppendFormatLine(code, 3, "strSql.Append(\" WHERE {0}\");", GetConditonOfMySql(table));
            }

            GetConditionParms(table, style, code);

            AppendFormatLine(code, 2, "}");
        }

        private static void Delete(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 删除一条数据");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "public int Delete({0})", GetArgumentsOfFunction(table));
            AppendFormatLine(code, 2, "{");

            if (style.CmdType != CommandType.StoredProcedure)
                AppendFormatLine(code, 3, "StringBuilder strSql;");
            AppendFormatLine(code, 3, "DbParameter[] cmdParms;");

            switch (style.CmdType)
            {
                case System.Data.CommandType.StoredProcedure:
                    AppendFormatLine(code, 3, "PrepareDeleteCommand({0}, out cmdParms);", GetArgumentValuesOfFunction(table));
                    AppendFormatLine(code, 3, "return {0}.ExecuteNonQuery(CommandType.StoredProcedure, \"sp_{1}_Delete\", cmdParms);",
                        style.DBHelperName,
                        table.Name);
                    break;
                case System.Data.CommandType.Text:
                default:
                    AppendFormatLine(code, 3, "PrepareDeleteCommand({0}, out strSql, out cmdParms);", GetArgumentValuesOfFunction(table));
                    AppendFormatLine(code, 3, "return {0}.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);",
                        style.DBHelperName);
                    break;
            }
            AppendFormatLine(code, 2, "}");
        }

        private static void PrepareDeleteCommand(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 为删除一条数据准备参数");
            AppendFormatLine(code, 2, "/// </summary>");

            switch (style.CmdType)
            {
                case CommandType.StoredProcedure:
                    AppendFormatLine(code, 2, "internal static void PrepareDeleteCommand({0}, out DbParameter[] cmdParms)", GetArgumentsOfFunction(table));
                    break;
                case CommandType.TableDirect:
                case CommandType.Text:
                    AppendFormatLine(code, 2, "internal static void PrepareDeleteCommand({0}, out StringBuilder strSql, out DbParameter[] cmdParms)", GetArgumentsOfFunction(table));
                    break;
                default:
                    break;
            }
            AppendFormatLine(code, 2, "{");

            if (style.CmdType != CommandType.StoredProcedure)
            {
                AppendFormatLine(code, 3, "strSql = new StringBuilder();");
                AppendFormatLine(code, 3, "strSql.Append(\"DELETE FROM {0} \");", table.Name);
                AppendFormatLine(code, 3, "strSql.Append(\" WHERE {0}\");", GetConditonOfMySql(table));
            }

            GetConditionParms(table, style, code);

            AppendFormatLine(code, 2, "}");
        }

        private static void Update(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 更新一条数据");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "public int Update({0}.{1} model)", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 2, "{");
            if (style.CmdType != CommandType.StoredProcedure)
                AppendFormatLine(code, 3, "StringBuilder strSql;");

            AppendFormatLine(code, 3, "DbParameter[] cmdParms;");
            switch (style.CmdType)
            {
                case System.Data.CommandType.StoredProcedure:
                    AppendFormatLine(code, 3, "PrepareUpdateCommand(model, out cmdParms);");
                    AppendFormatLine(code, 3, "return {0}.ExecuteNonQuery(CommandType.StoredProcedure, \"sp_{1}_Update\", cmdParms);",
                        style.DBHelperName, table.Name);
                    break;
                case System.Data.CommandType.Text:
                default:
                    AppendFormatLine(code, 3, "PrepareUpdateCommand(model, out strSql, out cmdParms);");
                    AppendFormatLine(code, 3, "return {0}.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);",
                        style.DBHelperName);
                    break;
            }
            AppendFormatLine(code, 2, "}");
        }

        private static void PrepareUpdateCommand(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 为更新一条数据准备参数");
            AppendFormatLine(code, 2, "/// </summary>");

            switch (style.CmdType)
            {
                case CommandType.StoredProcedure:
                    AppendFormatLine(code, 2, "internal static void PrepareUpdateCommand({0}.{1} model, out DbParameter[] cmdParms)", style.ModelNameSpace, table.Name);
                    break;
                case CommandType.TableDirect:
                case CommandType.Text:
                    AppendFormatLine(code, 2, "internal static void PrepareUpdateCommand({0}.{1} model, out StringBuilder strSql, out DbParameter[] cmdParms)", style.ModelNameSpace, table.Name);
                    break;
                default:
                    break;
            }
            AppendFormatLine(code, 2, "{");

            if (style.CmdType != CommandType.StoredProcedure)
            {
                AppendFormatLine(code, 3, "strSql = new StringBuilder();");
                AppendFormatLine(code, 3, "strSql.Append(\"UPDATE {0} SET \");", table.Name);

                foreach (Model.Field field in table.UpdateRows)
                {
                    AppendFormatLine(code, 3, "strSql.Append(\"{0}={1},\");", field.FieldName, GetSqlStoreProcedureParameter(field));
                }
                code.Remove(code.Length - 6, 1);

                AppendFormatLine(code, 3, "strSql.Append(\" WHERE {0}\");", GetConditonOfMySql(table));
            }

            AppendFormatLine(code, 3, "cmdParms = new DbParameter[]{");
            foreach (Model.Field field in table.UpdateRows)
            {
                AddInDbParameter(code, db, field, style);
            }
            foreach (Model.Field field in table.ConditionRows)
            {
                AddInDbParameter(code, db, field, style);
            }

            code.Remove(code.Length - 3, 3);
            code.AppendLine("};");

            AppendFormatLine(code, 2, "}");
        }

        private static void Add(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 增加一条数据");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "public int Add({0}.{1} model)", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 2, "{");
            if (style.CmdType != CommandType.StoredProcedure)
                AppendFormatLine(code, 3, "StringBuilder strSql;");

            AppendFormatLine(code, 3, "DbParameter[] cmdParms;");

            switch (style.CmdType)
            {
                case System.Data.CommandType.StoredProcedure:
                    AppendFormatLine(code, 3, "PrepareAddCommand(model, out cmdParms);");
                    AppendFormatLine(code, 3, "return {0}.ExecuteNonQuery(CommandType.StoredProcedure, \"sp_{1}_Add\", cmdParms);", style.DBHelperName, table.Name);
                    break;
                case System.Data.CommandType.Text:
                default:
                    AppendFormatLine(code, 3, "PrepareAddCommand(model, out strSql, out cmdParms);");
                    AppendFormatLine(code, 3, "return {0}.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);", style.DBHelperName);
                    break;
            }
            AppendFormatLine(code, 2, "}");
        }

        private static void PrepareAddCommand(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 为新增一条数据准备参数");
            AppendFormatLine(code, 2, "/// </summary>");

            switch (style.CmdType)
            {
                case CommandType.StoredProcedure:
                    AppendFormatLine(code, 2, "internal static void PrepareAddCommand({0}.{1} model, out DbParameter[] cmdParms)", style.ModelNameSpace, table.Name);
                    break;
                case CommandType.TableDirect:
                case CommandType.Text:
                    AppendFormatLine(code, 2, "internal static void PrepareAddCommand({0}.{1} model, out StringBuilder strSql, out DbParameter[] cmdParms)", style.ModelNameSpace, table.Name);
                    break;
                default:
                    break;
            }
            AppendFormatLine(code, 2, "{");

            if (style.CmdType != CommandType.StoredProcedure)
            {
                AppendFormatLine(code, 3, "strSql = new StringBuilder();");
                AppendFormatLine(code, 3, "strSql.Append(\"INSERT INTO {0}(\");", table.Name);
                code.Append("            strSql.Append(\"");
                foreach (Model.Field field in table.Fields)
                {
                    //把非标识且无默认值的字段排成如下格式：Id,Name,Value
                    if (ShouldBeParameter(table, field, style))
                        AppendFormat(code, 0, "{0},", field.FieldName);
                }
                code.Remove(code.Length - 1, 1); //删除最后一个逗号
                code.Append(")\");\r\n");
                AppendFormatLine(code, 3, "strSql.Append(\" VALUES (\");");
                code.Append("            strSql.Append(\"");
                foreach (Model.Field field in table.Fields)
                {
                    //把非标识且无默认值的字段排成如下格式：@Id,@Name,@Value
                    if (ShouldBeParameter(table, field, style))
                        AppendFormat(code, 0, "{0},", GetSqlStoreProcedureParameter(field));
                }
                code.Remove(code.Length - 1, 1);
                code.Append(")\");\n");
            }

            AppendFormatLine(code, 3, "cmdParms = new DbParameter[]{");

            foreach (Model.Field field in table.Fields)
            {
                if (ShouldBeParameter(table, field, style))
                    AddInDbParameter(code, db, field, style);
            }
            code.Remove(code.Length - 3, 3); //删除最后一个逗号和换行
            code.AppendLine("};");
            AppendFormatLine(code, 2, "}");
        }

        private static void Exists(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 是否存在该记录");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "public bool Exists({0})", GetArgumentsOfFunction(table));
            AppendFormatLine(code, 2, "{");

            if (style.CmdType != CommandType.StoredProcedure)
                AppendFormatLine(code, 3, "StringBuilder strSql;");

            AppendFormatLine(code, 3, "DbParameter[] cmdParms;");

            switch (style.CmdType)
            {
                case System.Data.CommandType.StoredProcedure:
                    AppendFormatLine(code, 3, "PrepareExistCommand({0}, out cmdParms);", GetArgumentValuesOfFunction(table));
                    AppendFormatLine(code, 3, "object obj = {0}.ExecuteScalar(CommandType.StoredProcedure, \"sp_{1}_Exists\", cmdParms);",
                         style.DBHelperName, table.Name);
                    break;
                case System.Data.CommandType.Text:
                default:
                    AppendFormatLine(code, 3, "PrepareExistCommand({0}, out strSql, out cmdParms);", GetArgumentValuesOfFunction(table));
                    AppendFormatLine(code, 3, "object obj = {0}.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);",
                        style.DBHelperName);
                    break;
            }

            AppendFormatLine(code, 3, "return int.Parse(obj.ToString()) > 0;");
            AppendFormatLine(code, 2, "}");
        }

        private static void PrepareExistCommand(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 为查询是否存在一条数据准备参数");
            AppendFormatLine(code, 2, "/// </summary>");

            switch (style.CmdType)
            {
                case CommandType.StoredProcedure:
                    AppendFormatLine(code, 2, "internal static void PrepareExistCommand({0}, out DbParameter[] cmdParms)", GetArgumentsOfFunction(table));
                    break;
                case CommandType.TableDirect:
                case CommandType.Text:
                    AppendFormatLine(code, 2, "internal static void PrepareExistCommand({0}, out StringBuilder strSql, out DbParameter[] cmdParms)", GetArgumentsOfFunction(table));
                    break;
                default:
                    break;
            }
            AppendFormatLine(code, 2, "{");

            if (style.CmdType != CommandType.StoredProcedure)
            {
                AppendFormatLine(code, 3, "strSql = new StringBuilder();");
                AppendFormatLine(code, 3, "strSql.Append(\"SELECT COUNT(1) FROM {0}\");", table.Name);
                AppendFormatLine(code, 3, "strSql.Append(\" WHERE {0}\");", GetConditonOfMySql(table));
            }

            GetConditionParms(table, style, code);

            AppendFormatLine(code, 2, "}");
        }

        private static void GetCount(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 获取数量");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "public int GetCount()");
            AppendFormatLine(code, 2, "{");

            switch (style.CmdType)
            {
                case System.Data.CommandType.StoredProcedure:
                    AppendFormatLine(code, 3, "object obj = {0}.ExecuteScalar(CommandType.StoredProcedure, \"sp_{1}_GetCount\", null);",
                         style.DBHelperName, table.Name);
                    break;
                case System.Data.CommandType.Text:
                default:
                    AppendFormatLine(code, 3, "object obj = {0}.ExecuteScalar(CommandType.Text, \"SELECT count(*) FROM {1}\", null);",
                        style.DBHelperName, table.Name);
                    break;
            }

            AppendFormatLine(code, 3, "return int.Parse(obj.ToString());");
            AppendFormatLine(code, 2, "}");
        }

        private static void GetList(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 获取泛型数据列表");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "public List<{0}.{1}> GetList()", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 2, "{");

            switch (style.CmdType)
            {
                case CommandType.StoredProcedure:
                    AppendFormatLine(code, 3, "using (DbDataReader dr = {0}.ExecuteReader(CommandType.StoredProcedure, \"sp_{1}_GetAllList\", null))",
                       style.DBHelperName, table.Name);
                    break;
                case CommandType.Text:
                case CommandType.TableDirect:
                default:
                    AppendFormatLine(code, 3, "StringBuilder strSql = new StringBuilder(\"SELECT * FROM {0}\");", table.Name);
                    AppendFormatLine(code, 3, "using (DbDataReader dr = {0}.ExecuteReader(CommandType.Text, strSql.ToString(), null))",
                        style.DBHelperName);
                    break;
            }
            AppendFormatLine(code, 3, "{");
            AppendFormatLine(code, 4, "List<{0}.{1}> lst = GetList(dr);", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 4, "return lst;");
            AppendFormatLine(code, 3, "}");
            AppendFormatLine(code, 2, "}");
        }

        private static void GetPageList(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 分页获取泛型数据列表");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "public PageList<{0}.{1}> GetPageList(PageInfo pi)", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 2, "{");

            AppendFormatLine(code, 3, "pi.RecordCount = GetCount();");
            AppendFormatLine(code, 3, "pi.Compute();");
            code.AppendLine();
            AppendFormatLine(code, 3, "PageList<{0}.{1}> pl = new PageList<{0}.{1}>(pi);", style.ModelNameSpace, table.Name);
            if (style.PageStyle == Model.CodeStyle.PageStyles.Sql)
            {
                StringBuilder fldSort = new StringBuilder();
                foreach (Model.Field field in table.ConditionRows)
                {
                    fldSort.Append(field.FieldName + ",");
                }
                fldSort = fldSort.Remove(fldSort.ToString().LastIndexOf(','), 1);

                AppendFormatLine(code, 3, "using (DbDataReader dr = {0}.GetPageList(\"{1}\", \"{2}\", null, pi.FirstIndex, pi.PageSize))",
                    style.DBHelperName, table.Name, fldSort.ToString());
                AppendFormatLine(code, 3, "{");
                AppendFormatLine(code, 4, "pl.List = GetList(dr);");
                AppendFormatLine(code, 3, "}");
            }
            else
            {
                switch (style.CmdType)
                {
                    case CommandType.StoredProcedure:
                        AppendFormatLine(code, 3, "using (DbDataReader dr = {0}.ExecuteReader(CommandType.StoredProcedure, \"sp_{1}_GetAllList\", null))",
                          style.DBHelperName, table.Name);
                        break;
                    case CommandType.Text:
                    case CommandType.TableDirect:
                    default:
                        AppendFormatLine(code, 3, "using (DbDataReader dr = {0}.ExecuteReader(CommandType.Text, \"SELECT * FROM {1}\", null))",
                           style.DBHelperName, table.Name);
                        break;
                }
                AppendFormatLine(code, 3, "{");
                AppendFormatLine(code, 4, "pl.List = GetPageList(dr, pi.FirstIndex, pi.PageSize);");
                AppendFormatLine(code, 3, "}");
            }
            code.AppendLine();
            AppendFormatLine(code, 3, "return pl;");
            AppendFormatLine(code, 2, "}");
        }

        private static void GetModelPrivate(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 由一行数据得到一个实体");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "private {0}.{1} GetModel(DbDataReader dr)", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 2, "{");
            AppendFormatLine(code, 3, "{0}.{1} model = new {0}.{1}();", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 3, "PrepareModel(model, dr);", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 3, "return model;");
            AppendFormatLine(code, 2, "}");
        }

        private static void PrepareModel(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 由一行数据得到一个实体");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "internal static void PrepareModel({0}.{1} model, DbDataReader dr)", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 2, "{");
            foreach (Model.Field model in table.Fields)
            {
                if (string.IsNullOrEmpty(model.DBUtilityConvertMethod))
                    AppendFormatLine(code, 3, "model.{0} = dr[\"{0}\"];", model.FieldName);
                else
                    AppendFormatLine(code, 3, "model.{0} = {1}(dr[\"{0}\"]);", model.FieldName, model.DBUtilityConvertMethod);
            }
            AppendFormatLine(code, 2, "}");
        }

        private static void GetListPrivate(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 由DbDataReader得到泛型数据列表");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "private List<{0}.{1}> GetList(DbDataReader dr)", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 2, "{");
            AppendFormatLine(code, 3, "List<{0}.{1}> lst = new List<{0}.{1}>();", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 3, "while (dr.Read())");
            AppendFormatLine(code, 3, "{");
            AppendFormatLine(code, 4, "lst.Add(GetModel(dr));");
            AppendFormatLine(code, 3, "}");
            AppendFormatLine(code, 3, "return lst;");
            AppendFormatLine(code, 2, "}");
        }

        private static void GetPageListPrivate(Model.Database db, Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// 由DbDataReader得到分页泛型数据列表");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "private List<{0}.{1}> GetPageList(DbDataReader dr, int first, int count)", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 2, "{");
            AppendFormatLine(code, 3, "List<{0}.{1}> lst = new List<{0}.{1}>();", style.ModelNameSpace, table.Name);
            code.AppendLine();
            AppendFormatLine(code, 3, "for (int i = 0; i < first; i++)", style.ModelNameSpace, table.Name);
            AppendFormatLine(code, 3, "{");
            AppendFormatLine(code, 4, "if (!dr.Read())");
            AppendFormatLine(code, 4, "{");
            AppendFormatLine(code, 5, "return lst;");
            AppendFormatLine(code, 4, "}");
            AppendFormatLine(code, 3, "}");
            code.AppendLine();
            AppendFormatLine(code, 3, "int resultsFetched = 0;");
            AppendFormatLine(code, 3, "while (resultsFetched < count && dr.Read())");
            AppendFormatLine(code, 3, "{");
            AppendFormatLine(code, 4, "lst.Add(GetModel(dr));");
            AppendFormatLine(code, 4, "resultsFetched++;");
            AppendFormatLine(code, 3, "}");
            code.AppendLine();
            AppendFormatLine(code, 3, "return lst;");
            AppendFormatLine(code, 2, "}");
        }

        #region 私有辅助
        /// <summary>
        /// 增加一个DbParameter
        /// </summary>
        private static void AddInDbParameter(StringBuilder code, Model.Database db, Model.Field field, Model.CodeStyle style)
        {
            AppendFormatLine(code, 4, "{0}.CreateInDbParameter(\"{1}\", DbType.{2}, model.{3}),",
                style.DBHelperName, GetSqlStoreProcedureParameter(field), field.DbType, field.FieldName);
        }

        private static void GetConditionParms(Model.Table table, Model.CodeStyle style, StringBuilder code)
        {
            AppendFormatLine(code, 3, "cmdParms = new DbParameter[]{");
            foreach (Model.Field field in table.ConditionRows)
            {
                AppendFormatLine(code, 4, "{0}.CreateInDbParameter(\"{1}\", DbType.{2}, {3}),", style.DBHelperName, GetSqlStoreProcedureParameter(field), field.DbType.ToString(), field.FieldName);
            }
            code.Remove(code.Length - 3, 3); //删除最后一个逗号和换行
            code.Append("};");
            code.AppendLine();
        }
        #endregion
    }
}
