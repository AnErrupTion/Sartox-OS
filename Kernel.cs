using System;
using Sys = Cosmos.System;
using SartoxOS.Utils;
using SartoxOS.Accounts;
using Cosmos.Core;
using Cosmos.System.FileSystem.VFS;
using System.Text;
using Cosmos.System.ExtendedASCII;
using SartoxOS.Applications;
using System.IO;
using SartoxOS.Login;
using SartoxOS.Settings;

namespace SartoxOS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Encoding.RegisterProvider(CosmosEncodingProvider.Instance);
            Console.InputEncoding = Encoding.GetEncoding(437);
            Console.OutputEncoding = Encoding.GetEncoding(437);

            ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Loading virtual FS...");
            VFSManager.RegisterVFS(Reference.FAT);

            if (Reference.FAT.GetVolumes().Count != 0) ColorConsole.WriteLine(ConsoleColor.Green, "Sucessfully loaded the virtual FS!");
            else ColorConsole.WriteLine(ConsoleColor.Red, "Uh-oh, couldn't load the virtual FS...");

            ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Initializing SSE...");
            Global.CPU.InitSSE();
            ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Initializing Float...");
            Global.CPU.InitFloat();

            ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Initializing settings...");
            if (!File.Exists(Reference.Settings)) File.Create(Reference.Settings);
            ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Loading settings...");

            string layout = SettingsMan.Get("keyboard_layout");
            if (layout == "fr") Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.FR_Standard());
            else if (layout == "en") Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.US_Standard());
            else if (layout == "de") Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.DE_Standard());

            ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Creating live account...");
            Acc acc = new Acc("Sartox", "123");
            acc.Create();

            Reference.Installed = File.Exists(Reference.RootPath + "Installed.txt");
            if (!Reference.Installed)
            {
                ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Loading setup...");
                Setup.Init();
            }

            Console.Clear();
            ColorConsole.WriteLine(ConsoleColor.Green, $"Welcome to Sartox OS v{Reference.Version}!");
        }

        protected override void Run()
        {
            LoginMan.Init();
        }
    }
}
