using System;

namespace SartoxOS.Utils
{
    public static class ColorConsole
    {
        public static void WriteLine(ConsoleColor c, string msg)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = c;
            Console.WriteLine(msg);
            Console.ForegroundColor = color;
        }

        public static void Write(ConsoleColor c, string msg)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = c;
            Console.Write(msg);
            Console.ForegroundColor = color;
        }
    }
}
