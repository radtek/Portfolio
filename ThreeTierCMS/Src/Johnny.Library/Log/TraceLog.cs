using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Library.Log
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
            return "\r\n" + System.DateTime.Now.ToLocalTime() + ": " + msg;
        }

        public static string SetMessageLn(string module, string msg)
        {
            return "\r\n" + System.DateTime.Now.ToLocalTime() + ": " + module + msg;
        }
    }
}
