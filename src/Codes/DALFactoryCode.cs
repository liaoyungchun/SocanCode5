using System.Collections.Generic;
using System.Text;

namespace Codes
{
    public partial class DALFactoryCode : CodeHelper
    {
        /// <summary>
        /// 创建反射工厂类
        /// </summary>
        public static string GetDALFactoryCode(List<Model.Table> tables, Model.CodeStyle style)
        {
            StringBuilder code = new StringBuilder(CommonCode.GetCSharpCopyrightCode());
            AppendFormatLine(code, 0, "using System;");
            AppendFormatLine(code, 0, "using System.Configuration;");
            AppendFormatLine(code, 0, "using System.Collections.Generic;");
            AppendFormatLine(code, 0, "using System.Text;");
            AppendFormatLine(code, 0, "using System.Reflection;");
            code.AppendLine();
            AppendFormatLine(code, 0, "namespace DALFactory");
            AppendFormatLine(code, 0, "{");
            AppendFormatLine(code, 1, "public class DataAccess");
            AppendFormatLine(code, 1, "{");
            AppendFormatLine(code, 2, "private static readonly string path = ConfigurationManager.AppSettings[\"WebDAL\"];");
            foreach (Model.Table table in tables)
            {
                code.AppendLine();
                AppendFormatLine(code, 2, "public static {0}.I{1} Create{2}{3}()",
                    style.IDALNameSpace, table.Name, style.AfterNamespace, table.Name);
                AppendFormatLine(code, 2, "{");
                AppendFormatLine(code, 3, "string className = path + \".{0}{1}\";", style.AfterNamespaceDot, table.Name);
                AppendFormatLine(code, 3, "return ({0}.I{1})Assembly.Load(path).CreateInstance(className);", style.IDALNameSpace, table.Name);
                AppendFormatLine(code, 2, "}");
            }
            AppendFormatLine(code, 1, "}");
            AppendFormatLine(code, 0, "}");

            return code.ToString();
        }
    }
}
