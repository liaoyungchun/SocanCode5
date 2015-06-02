using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CodeUtility
{
    public class FileStream
    {
        /// <summary>
        /// 写入文件
        /// </summary>
        public static void WriteFile(string path, string content)
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
            sw.Write(content);
            sw.Close();
        }

        /// <summary>
        /// 在文件尾追加内容，如果文件不存在，则创建
        /// </summary>
        /// <param name="context">追加内容</param>
        /// <param name="filePath">文件路径</param>
        public static void AppendFile(string context, string filePath)
        {
            System.IO.FileStream stream;
            System.IO.StreamReader reader;
            System.IO.StreamWriter writer;

            stream = new System.IO.FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            reader = new System.IO.StreamReader(stream);
            string old = reader.ReadToEnd();

            writer = new System.IO.StreamWriter(stream);
            writer.Write(context);
            writer.Flush();
            writer.Close();

            reader.Close();
            stream.Close();
        }

        /// <summary>
        /// 从指定路径中读取文件
        /// </summary>
        public static string ReadFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string str = sr.ReadToEnd();
            sr.Close();
            return str;
        }

        /// <summary>
        /// 运行命令行
        /// </summary>
        public static void RunCommand(string cmd)
        {
            string tempPath = Environment.GetEnvironmentVariable("TEMP");
            string fileName = tempPath + "\\" + Guid.NewGuid().ToString("N") + ".bat";
            CodeUtility.FileStream.WriteFile(fileName, cmd.ToString());
            Process.Start(fileName);
        }
    }
}
