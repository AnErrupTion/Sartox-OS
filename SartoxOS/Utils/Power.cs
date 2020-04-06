using System;
using Sys = Cosmos.System;

namespace SartoxOS.Utils
{
    public static class Power
    {
        public static void Shutdown()
        {
            Console.WriteLine("Shutting down computer...");
            Sys.Power.Shutdown();
        }

        public static void Restart()
        {
            Console.WriteLine("Rebooting computer...");
            Sys.Power.Reboot();
        }
    }
}
