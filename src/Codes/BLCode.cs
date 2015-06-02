using System.Text;

namespace Codes
{
    public partial class BLCode : CodeHelper
    {
        /// <summary>
        /// 生成业务逻辑层.asmx
        /// </summary>
        public static string GetBLCode(Model.Database db, Model.Table table, Model.CodeStyle style)
        {
            StringBuilder code = new StringBuilder();
            AppendFormatLine(code, 0, "<%@ WebService Language=\"C#\" CodeBehind=\"{0}{1}.asmx.cs\" Class=\"{2}.{1}\" %>",
                style.AfterNamespaceDot, table.Name, style.BLNameSpace);
            return code.ToString();
        }

        /// <summary>
        /// 生成业务逻辑层.cs
        /// </summary>
        public static string GetBLCSCode(Model.Database db, Model.Table table, Model.CodeStyle style)
        {
            StringBuilder code = new StringBuilder(CommonCode.GetCSharpCopyrightCode());

            AppendFormatLine(code, 0, "using System;");


            if (style.SlnFrame == Model.CodeStyle.SlnFrames.Factory)
            {
                AppendFormatLine(code, 0, "using DALFactory;");
            }
            if (style.BlFrame == Model.CodeStyle.BLFrame.BLS)
            {
                AppendFormatLine(code, 0, "using System.Web.Services;");
                AppendFormatLine(code, 0, "using System.ComponentModel;");
            }

            AppendFormatLine(code, 0, "using System.Collections.Generic;");
            AppendFormatLine(code, 0, "using System.Text.RegularExpressions;");
            AppendFormatLine(code, 0, "using System.Web;");

            if (style.CacheFrame != Model.CodeStyle.CacheFrames.None)
                AppendFormatLine(code, 0, "using System.Web.Caching;");

            code.AppendLine();
            AppendFormatLine(code, 0, "namespace {0}", style.BLNameSpace);
            AppendFormatLine(code, 0, "{");
            AppendFormatLine(code, 1, "/// <summary>");
            AppendFormatLine(code, 1, "/// 业务逻辑类 {0}", table.Name);
            AppendFormatLine(code, 1, "/// </summary>");

            if (style.BlFrame == Model.CodeStyle.BLFrame.BLS)
            {
                AppendFormatLine(code, 1, "[WebService(Namespace = \"http://www.Socansoft.com/\")]");
                AppendFormatLine(code, 1, "[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]");
                AppendFormatLine(code, 1, "[ToolboxItem(false)]");
            }

            AppendFormatLine(code, 1, "public class {0} : BLHelper", table.Name);

            AppendFormatLine(code, 1, "{");

            if (style.SlnFrame == Model.CodeStyle.SlnFrames.Factory)
            {
                AppendFormatLine(code, 2, "private readonly {0}.I{1} dal = DataAccess.Create{2}{3}();",
                    style.IDALNameSpace, table.Name, style.AfterNamespace, table.Name);
            }
            else
            {
                AppendFormatLine(code, 2, "private readonly {0}.{1} dal = new {0}.{1}();",
                    style.DALNameSpace, table.Name);
            }

            AppendFormatLine(code, 2, "public {0}()", table.Name);
            if (style.CacheFrame != Model.CodeStyle.CacheFrames.None)
                AppendFormatLine(code, 3, ": base(\"{0}{1}_\")", style.AfterNamespaceLine, table.Name);

            AppendFormatLine(code, 2, "{ }");
            code.AppendLine();

            switch (style.CacheFrame)
            {
                case Model.CodeStyle.CacheFrames.None:
                    code.Append(ReadFromTemplate(Model.CreateStyle.CURRENT_PATH + "\\BL\\None.template", db, table, style));
                    break;
                case Model.CodeStyle.CacheFrames.ObjectCache:
                    code.Append(ReadFromTemplate(Model.CreateStyle.CURRENT_PATH + "\\BL\\ObjectCache.template", db, table, style));
                    break;
                case Model.CodeStyle.CacheFrames.AggregateDependency:
                    code.Append(ReadFromTemplate(Model.CreateStyle.CURRENT_PATH + "\\BL\\AggregateDependency.template", db, table, style));
                    break;
                case Model.CodeStyle.CacheFrames.ObjectCacheAndAggregateDependency:
                    code.Append(ReadFromTemplate(Model.CreateStyle.CURRENT_PATH + "\\BL\\ObjectCacheAndAggregateDependency.template", db, table, style));
                    break;
                default:
                    break;
            }

            AppendFormatLine(code, 1, "}");
            AppendFormatLine(code, 0, "}");

            return code.ToString();
        }

        /// <summary>
        /// BLHelper文件的代码
        /// </summary>
        public static string GetBLHelperCode(Model.CodeStyle style)
        {
            return ReadFromTemplate(Model.CreateStyle.CURRENT_PATH + "\\BL\\BLHelper.template", null, null, style);
        }
    }
}
