using Cosmos.HAL.Drivers.PCI.Video;
using Cosmos.System.FileSystem;
using SartoxOS.Accounts;
using SartoxOS.GUI.Input;

namespace SartoxOS.Utils
{
    public static class Reference
    {
        public static string Version = "0.0.4";
        public static string KernelVersion = "0.0.2-2";

        public static string[] Commands = {
            "help",
            "gui",
            "time",
            "getmem",
            "textpad",
            "keyblang",
            "cd",
            "ls",
            "logout",
            "cls",
            "about",
            "stop",
            "mkdir",
            "mk",
            "touch",
            "write",
            "rm",
            "rmdir",
            "move",
            "copy",
            "reboot",
            "crash",
            "sartoxshell"
        };

        public static string RootPath = @"0:\";
        public static string CurrentDir = RootPath;
        public static string UserAccess = "#"; // Means the user has root (or admin) permissions.

        public static string Settings = $"{RootPath}settings.dat";

        public static bool Installed;

        public static CosmosVFS FAT = new CosmosVFS();
        public static Acc UserAccount = null;

        public static VMWareSVGAII Driver;
        public static Mouse Mouse;

        public static readonly ushort Width = 800;
        public static readonly ushort Height = 600;
    }
}
