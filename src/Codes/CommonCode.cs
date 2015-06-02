using System;
using System.Reflection;
using System.Text;

namespace Codes
{
    public class CommonCode : CodeHelper
    {
        /// <summary>
        /// C#��Ȩ��Ϣ
        /// </summary>
        /// <returns></returns>
        public static string GetCSharpCopyrightCode()
        {
            StringBuilder code = new StringBuilder();
            AppendFormatLine(code, 0, "//------------------------------------------------------------------------------");
            AppendFormatLine(code, 0, "// ������ʶ: Copyright (C) 2007-{0} Socansoft.com ��Ȩ����", DateTime.Today.Year);
            AppendFormatLine(code, 0, "// ��������: SocanCode���������� V{0} �Զ������� {1}", Assembly.GetEntryAssembly().GetName().Version, DateTime.Now);
            AppendFormatLine(code, 0, "//");
            AppendFormatLine(code, 0, "// ��������: ");
            AppendFormatLine(code, 0, "//");
            AppendFormatLine(code, 0, "// �޸ı�ʶ: ");
            AppendFormatLine(code, 0, "// �޸�����: ");
            AppendFormatLine(code, 0, "//------------------------------------------------------------------------------");
            code.AppendLine();
            return code.ToString();
        }

        /// <summary>
        /// Html��Ȩ��Ϣ
        /// </summary>
        /// <returns></returns>
        public static string GetHtmlCopyrightCode()
        {
            StringBuilder code = new StringBuilder();
            AppendFormatLine(code, 0, "<%-------------------------------------------------------------------------------");
            AppendFormatLine(code, 0, " ������ʶ: Copyright (C) 2007-{0} Socansoft.com ��Ȩ����", DateTime.Today.Year);
            AppendFormatLine(code, 0, " ��������: SocanCode���������� V{0} �Զ������� {1}", Assembly.GetEntryAssembly().GetName().Version, DateTime.Now);
            code.AppendLine();
            AppendFormatLine(code, 0, " ��������: ");
            code.AppendLine();
            AppendFormatLine(code, 0, " �޸ı�ʶ: ");
            AppendFormatLine(code, 0, " �޸�����: ");
            AppendFormatLine(code, 0, "--------------------------------------------------------------------------------%>");
            code.AppendLine();
            return code.ToString();
        }

        /// <summary>
        /// SQL��Ȩ��Ϣ
        /// </summary>
        /// <param name="table">��</param>
        /// <param name="function">��������</param>
        /// <returns></returns>
        public static string GetSQLCopyrightCode(string function)
        {
            StringBuilder code = new StringBuilder();
            AppendFormatLine(code, 0, "-----------------------------------------------");
            AppendFormatLine(code, 0, "--Copyright (C) 2007-{0} Socansoft.com ��Ȩ����", DateTime.Today.Year);
            AppendFormatLine(code, 0, "--SocanCode���������� V{0} �Զ������� {1}", Assembly.GetEntryAssembly().GetName().Version, DateTime.Now);
            AppendFormatLine(code, 0, "--˵����{0}", function);
            AppendFormatLine(code, 0, "--ʱ�䣺{0}", DateTime.Now);
            AppendFormatLine(code, 0, "-----------------------------------------------");
            return code.ToString();
        }
    }
}
