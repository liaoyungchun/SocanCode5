using System.Collections.Generic;
using System.Text;

namespace Codes
{
    public class CacheDependencyFactoryCode : CodeHelper
    {
        public static string GetDependencyAccessCode(List<Model.Table> tables, Model.CodeStyle style)
        {
            StringBuilder code = new StringBuilder();
            AppendFormatLine(code, 0, "using System;");
            AppendFormatLine(code, 0, "using System.Collections.Generic;");
            AppendFormatLine(code, 0, "using System.Text;");
            AppendFormatLine(code, 0, "using System.Web.Caching;");
            AppendFormatLine(code, 0, "using System.Configuration;");
            AppendFormatLine(code, 0, "using System.Reflection;");
            code.AppendLine();

            AppendFormatLine(code, 0, "namespace CacheDependencyFactory");
            AppendFormatLine(code, 0, "{");
            AppendFormatLine(code, 1, "public class DependencyAccess");
            AppendFormatLine(code, 1, "{");
            foreach (Model.Table table in tables)
            {
                AppendFormatLine(code, 2, "public static ICacheDependency.ISocansoftCacheDependency Create{0}{1}Dependency()",
                    style.AfterNamespace, table.Name);
                AppendFormatLine(code, 2, "{");
                AppendFormatLine(code, 3, "return LoadInstance(\"{0}{1}\");", style.AfterNamespaceDot, table.Name);
                AppendFormatLine(code, 2, "}");
                code.AppendLine();
            }
            AppendFormatLine(code, 2, "private static ICacheDependency.ISocansoftCacheDependency LoadInstance(string className)");
            AppendFormatLine(code, 2, "{");
            AppendFormatLine(code, 3, "string path = ConfigurationManager.AppSettings[\"CacheDependencyAssembly\"];");
            AppendFormatLine(code, 3, "string fullQualifiedClass = path + \".\" + className;");
            AppendFormatLine(code, 3, "return (ICacheDependency.ISocansoftCacheDependency)Assembly.Load(path).CreateInstance(fullQualifiedClass);");
            AppendFormatLine(code, 2, "}");
            AppendFormatLine(code, 1, "}");
            AppendFormatLine(code, 0, "}");
            return CommonCode.GetCSharpCopyrightCode() + code.ToString();
        }

        public static string GetDependencyFacadeCode(List<Model.Table> tables, Model.CodeStyle style)
        {
            StringBuilder code = new StringBuilder();
            AppendFormatLine(code, 0, "using System;");
            AppendFormatLine(code, 0, "using System.Collections.Generic;");
            AppendFormatLine(code, 0, "using System.Text;");
            AppendFormatLine(code, 0, "using System.Configuration;");
            AppendFormatLine(code, 0, "using System.Web.Caching;");
            code.AppendLine();

            AppendFormatLine(code, 0, "namespace CacheDependencyFactory");
            AppendFormatLine(code, 0, "{");
            AppendFormatLine(code, 1, "public class DependencyFacade");
            AppendFormatLine(code, 1, "{");
            AppendFormatLine(code, 2, "private static readonly string path = ConfigurationManager.AppSettings[\"CacheDependencyAssembly\"];");
            foreach (Model.Table table in tables)
            {
                code.AppendLine();
                AppendFormatLine(code, 2, "public static AggregateCacheDependency Get{0}{1}CacheDependency()",
                    style.AfterNamespace, table.Name);
                AppendFormatLine(code, 2, "{");
                AppendFormatLine(code, 3, "if (!string.IsNullOrEmpty(path))");
                AppendFormatLine(code, 4, "return DependencyAccess.Create{0}{1}Dependency().GetDependency();",
                    style.AfterNamespace, table.Name);
                AppendFormatLine(code, 3, "else");
                AppendFormatLine(code, 4, "return null;");
                AppendFormatLine(code, 2, "}");
            }
            AppendFormatLine(code, 1, "}");
            AppendFormatLine(code, 0, "}");
            code.AppendLine();
            code.AppendLine();
            return CommonCode.GetCSharpCopyrightCode() + code.ToString();
        }
    }
}
