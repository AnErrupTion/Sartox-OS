using Cosmos.Debug.Kernel;
using Cosmos.System.FileSystem;
using SartoxOS.Accounts;
using Cosmos.Core;

namespace SartoxOS.Utils
{
    public static class Reference
    {
        public static string Version = "0.0.1";

        public static string RootPath = @"0:\";
        public static string CurrentDir = RootPath;
        public static string UserAccess = "#"; // Means the user has root (or admin) permissions

        public static bool Installed;

        public static Debugger Debugger = Global.mDebugger;
        public static CosmosVFS FAT = new CosmosVFS();
        public static Acc UserAccount = null;
    }
}
