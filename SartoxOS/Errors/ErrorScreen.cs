using System;
using SartoxOS.Utils;

namespace SartoxOS.Errors
{
    public static class ErrorScreen
    {
        public static void Init(string msg, int result)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            ColorConsole.WriteLine(ConsoleColor.White, "Oh no, an error has occured...");
            ColorConsole.WriteLine(ConsoleColor.White, "The system has been paused to prevent damage to your computer.");
            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.White, "Error : " + msg);
            ColorConsole.WriteLine(ConsoleColor.White, "HResult : " + result.ToString());
            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.White, "If you see this screen, it means you've encountered an error across Sartox OS. Please report this error at the developer ASAP so it can be fixed.\nThe system must reboot now.");

            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.White, "Press any key to reboot...");
            Console.ReadKey();
            Power.Restart();
        }
    }
}
