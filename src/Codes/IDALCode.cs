using System.Text;

namespace Codes
{
    public partial class IDALCode : CodeHelper
    {
        /// <summary>
        /// ����IDAL�ӿڲ����
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public static string GetIDALCode(Model.Table table, Model.CodeStyle style)
        {
            StringBuilder code = new StringBuilder(CommonCode.GetCSharpCopyrightCode());
            AppendFormatLine(code, 0, "using System;");
            AppendFormatLine(code, 0, "using System.Data;");
            AppendFormatLine(code, 0, "using System.Collections;");
            AppendFormatLine(code, 0, "using System.Collections.Generic;");
            code.AppendLine();
            AppendFormatLine(code, 0, "namespace {0}", style.IDALNameSpace);
            AppendFormatLine(code, 0, "{");
            AppendFormatLine(code, 1, "/// <summary>");
            AppendFormatLine(code, 1, "/// �ӿڲ� I{0}", table.Name);
            AppendFormatLine(code, 1, "/// </summary>");
            AppendFormatLine(code, 1, "public interface I{0}", table.Name);
            AppendFormatLine(code, 1, "{");

            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// ����һ������");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "int Add({0}.{1} model);", style.ModelNameSpace, table.Name);
            code.AppendLine();

            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// ����һ������");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "int Update({0}.{1} model);", style.ModelNameSpace, table.Name);
            code.AppendLine();

            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// ɾ��һ������");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "int Delete({0});", CodeHelper.GetArgumentsOfFunction(table));
            code.AppendLine();

            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// �Ƿ���ڸü�¼");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "bool Exists({0});", CodeHelper.GetArgumentsOfFunction(table));
            code.AppendLine();

            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// �õ�һ������ʵ��");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "{0}.{1} GetModel({2});", style.ModelNameSpace, table.Name, GetArgumentsOfFunction(table));
            code.AppendLine();

            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// ��ȡ��������");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "int GetCount();");
            code.AppendLine();

            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// ��ȡ���������б�");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "List<{0}.{1}> GetList();", style.ModelNameSpace, table.Name);
            code.AppendLine();

            AppendFormatLine(code, 2, "/// <summary>");
            AppendFormatLine(code, 2, "/// ��ҳ��ȡ���������б�");
            AppendFormatLine(code, 2, "/// </summary>");
            AppendFormatLine(code, 2, "PageList<{0}.{1}> GetPageList(PageInfo pi);",
                style.ModelNameSpace, table.Name);

            AppendFormatLine(code, 1, "}");
            AppendFormatLine(code, 0, "}");

            return code.ToString();
        }
    }
}
