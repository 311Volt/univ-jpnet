using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF.App
{
    internal static class ConsoleUtil
    {
        public static void WriteLineColor(string str, System.ConsoleColor color)
        {
            WriteColor(str + "\n", color);
        }

        public static void WriteColor(string str, System.ConsoleColor color)
        {
            var oldCol = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ForegroundColor = oldCol;
        }

        public static void ErrorMessage(string str)
        {
            WriteColor("error: ", ConsoleColor.Red);
            Console.WriteLine(str);
        }
    }
}
