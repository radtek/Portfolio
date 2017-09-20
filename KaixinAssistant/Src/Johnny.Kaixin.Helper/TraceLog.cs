using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Johnny.Kaixin.Helper
{    
    public class TraceLog
    {
        // Fields
        private static bool _newLine;

        // Methods
        public TraceLog()
        {
            _newLine = true;
        }

        public static void Error(string format, params object[] args)
        {
            string str = string.Format(format, args);
            DateTime time = DateTime.Now.ToLocalTime();
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            if (!_newLine)
            {
                Console.WriteLine();
            }
            Console.WriteLine(string.Format("[{0}]: {1}", time, str));
            Console.ForegroundColor = foregroundColor;
            _newLine = true;
        }



        public static void Print(string format, params object[] args)
        {
            string str = string.Format(format, args);
            DateTime time = DateTime.Now.ToLocalTime();
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Gray;
            if (_newLine)
            {
                Console.Write(string.Format("[{0}]: ", time));
                _newLine = false;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(str);
            Console.ForegroundColor = foregroundColor;
        }

        public static void PrintLn(string format, params object[] args)
        {
            Print(format, args);
            Console.WriteLine();
            _newLine = true;
        }

        public static string SetMessage(string msg)
        {
            return msg;
        }

        public static string SetMessage(string module, string msg)
        {
            return module + msg;
        }

        public static string SetMessageLn(string msg)
        {
            return "\r\n" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + msg;
        }

        public static string SetMessageLn(string module, string msg)
        {
            return "\r\n" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + module + msg;
        }

        public static void WriteLogToFile(string taskname, string log)
        {
            //taskname+date
            //主号花园20080414.txt
            if (log.IndexOf("分钟后重新启动任务") > -1)
                return;
            string folder = Path.Combine(Application.StartupPath, "Log");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            string file = Path.Combine(folder, "Task_" + taskname + ".txt");
            if (File.Exists(file))
            {
                FileInfo info = new FileInfo(file);
                if (info.LastWriteTime.Year != System.DateTime.Now.Year ||
                    info.LastWriteTime.Month != System.DateTime.Now.Month ||
                    info.LastWriteTime.Day != System.DateTime.Now.Day)
                {
                    //rename
                    File.Move(file, Path.Combine(info.DirectoryName, "Task_" + taskname + System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + ".txt"));
                }
            }
            StreamWriter sw = File.AppendText(file);
            sw.WriteLine(log);
            sw.Flush();
            sw.Close();
        }
    }
}
