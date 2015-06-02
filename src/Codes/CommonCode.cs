using System;
using System.Reflection;
using System.Text;

namespace Codes
{
    public class CommonCode : CodeHelper
    {
        /// <summary>
        /// C#版权信息
        /// </summary>
        /// <returns></returns>
        public static string GetCSharpCopyrightCode()
        {
            StringBuilder code = new StringBuilder();
            AppendFormatLine(code, 0, "//------------------------------------------------------------------------------");
            AppendFormatLine(code, 0, "// 创建标识: Copyright (C) 2007-{0} Socansoft.com 版权所有", DateTime.Today.Year);
            AppendFormatLine(code, 0, "// 创建描述: SocanCode代码生成器 V{0} 自动创建于 {1}", Assembly.GetEntryAssembly().GetName().Version, DateTime.Now);
            AppendFormatLine(code, 0, "//");
            AppendFormatLine(code, 0, "// 功能描述: ");
            AppendFormatLine(code, 0, "//");
            AppendFormatLine(code, 0, "// 修改标识: ");
            AppendFormatLine(code, 0, "// 修改描述: ");
            AppendFormatLine(code, 0, "//------------------------------------------------------------------------------");
            code.AppendLine();
            return code.ToString();
        }

        /// <summary>
        /// Html版权信息
        /// </summary>
        /// <returns></returns>
        public static string GetHtmlCopyrightCode()
        {
            StringBuilder code = new StringBuilder();
            AppendFormatLine(code, 0, "<%-------------------------------------------------------------------------------");
            AppendFormatLine(code, 0, " 创建标识: Copyright (C) 2007-{0} Socansoft.com 版权所有", DateTime.Today.Year);
            AppendFormatLine(code, 0, " 创建描述: SocanCode代码生成器 V{0} 自动创建于 {1}", Assembly.GetEntryAssembly().GetName().Version, DateTime.Now);
            code.AppendLine();
            AppendFormatLine(code, 0, " 功能描述: ");
            code.AppendLine();
            AppendFormatLine(code, 0, " 修改标识: ");
            AppendFormatLine(code, 0, " 修改描述: ");
            AppendFormatLine(code, 0, "--------------------------------------------------------------------------------%>");
            code.AppendLine();
            return code.ToString();
        }

        /// <summary>
        /// SQL版权信息
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="function">功能描述</param>
        /// <returns></returns>
        public static string GetSQLCopyrightCode(string function)
        {
            StringBuilder code = new StringBuilder();
            AppendFormatLine(code, 0, "-----------------------------------------------");
            AppendFormatLine(code, 0, "--Copyright (C) 2007-{0} Socansoft.com 版权所有", DateTime.Today.Year);
            AppendFormatLine(code, 0, "--SocanCode代码生成器 V{0} 自动创建于 {1}", Assembly.GetEntryAssembly().GetName().Version, DateTime.Now);
            AppendFormatLine(code, 0, "--说明：{0}", function);
            AppendFormatLine(code, 0, "--时间：{0}", DateTime.Now);
            AppendFormatLine(code, 0, "-----------------------------------------------");
            return code.ToString();
        }
    }
}
