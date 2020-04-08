using Cosmos.System.FileSystem;
using SartoxOS.Accounts;

namespace SartoxOS.Utils
{
    public static class Reference
    {
        public static string Version = "0.0.2";

        public static string RootPath = @"0:\";
        public static string CurrentDir = RootPath;
        public static string UserAccess = "#"; // Means the user has root (or admin) permissions

        public static bool Installed;

        public static CosmosVFS FAT = new CosmosVFS();
        public static Acc UserAccount = null;
    }
}
