using System;
using Sys = Cosmos.System;

namespace SartoxOS.Utils
{
    public static class Power
    {
        public static void Shutdown()
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Shutting down computer...");
            Sys.Power.Shutdown();
        }

        public static void Restart()
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Rebooting computer...");
            Sys.Power.Reboot();
        }
    }
}
