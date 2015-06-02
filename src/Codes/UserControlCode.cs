using System.Text;

namespace Codes
{
    public partial class UserControlCode : CodeHelper
    {
        /// <summary>
        /// 生成Web用户控件后台代码
        /// </summary>
        public static string GetWebUserControlCsCode(Model.Table table, Model.CodeStyle style)
        {
            return ReadFromTemplate(Model.CreateStyle.CURRENT_PATH + "\\Web\\Cs.Template", null, table, style);
        }

        /// <summary>
        /// 生成Web用户控件Designer后台代码
        /// </summary>
        public static string GetWebUserControlDesignerCsCode(Model.Table table, Model.CodeStyle style)
        {
            return ReadFromTemplate(Model.CreateStyle.CURRENT_PATH + "\\Web\\DesignerCs.Template", null, table, style);
        }

        /// <summary>
        /// 生成Web用户控件
        /// </summary>
        public static string GetUserControlCode(Model.Table table, Model.CodeStyle style)
        {
            int width = table.Fields.Count > 1 ? 100 / (table.Fields.Count - 1) : 100;

            StringBuilder code = new StringBuilder(CommonCode.GetHtmlCopyrightCode());
            AppendFormatLine(code, 0, "<%@ Control Language=\"C#\" AutoEventWireup=\"true\" Codebehind=\"{0}{1}Grid.ascx.cs\"",
                style.AfterNamespace, table.Name);
            AppendFormatLine(code, 1, "Inherits=\"Controls_{0}{1}Grid\" %>", style.AfterNamespace, table.Name);
            AppendFormatLine(code, 0, "<table id=\"{0}Grid\" class=\"data\" style=\"width: 100%;\">", table.Name);
            AppendFormatLine(code, 1, "<tr class=\"title\">");
            int i = 0;
            foreach (Model.Field field in table.Fields)
            {
                if (field != table.ConditionRows[0])
                {
                    AppendFormatLine(code, 2, "<td style=\"width: {0}%;\">", width);

                    if (i == 0)
                        AppendFormatLine(code, 3, "<input id=\"chkChooseAll{0}\" type=\"checkbox\" onclick=\"chooseAll('{0}Grid','chkChooseAll{0}')\" />",
                            table.Name);
                    AppendFormatLine(code, 3, "{0}", field.FieldDescn);
                    AppendFormatLine(code, 2, "</td>");
                    i++;
                }
            }
            AppendFormatLine(code, 1, "</tr>");
            AppendFormatLine(code, 1, "<asp:Repeater ID=\"grd{0}\" runat=\"server\" OnItemCommand=\"grd{0}_ItemCommand\">", table.Name);
            AppendFormatLine(code, 2, "<ItemTemplate>");
            AppendFormatLine(code, 3, "<tr>");

            i = 0;
            foreach (Model.Field field in table.Fields)
            {
                if (field != table.ConditionRows[0])
                {
                    AppendFormatLine(code, 4, "<td>");

                    if (i == 0)
                    {
                        AppendFormatLine(code, 5, "<asp:CheckBox ID=\"chkChoose\" runat=\"server\" /><asp:HiddenField ID=\"hf{0}\"", table.Name);
                        AppendFormatLine(code, 6, "runat=\"server\" Value='<%# Eval(\"{0}\") %>' />", table.ConditionRows[0].FieldName);
                    }
                    AppendFormatLine(code, 5, "<span title=\"<%# Eval(\"{0}\") %>\">", field.FieldName);
                    AppendFormatLine(code, 6, "<%# Eval(\"{0}\") %></span>", field.FieldName);
                    AppendFormatLine(code, 4, "</td>");
                    i++;
                }
            }
            AppendFormatLine(code, 3, "</tr>");
            AppendFormatLine(code, 2, "</ItemTemplate>");
            AppendFormatLine(code, 1, "</asp:Repeater>");
            AppendFormatLine(code, 0, "</table>");

            return code.ToString();
        }
    }
}
