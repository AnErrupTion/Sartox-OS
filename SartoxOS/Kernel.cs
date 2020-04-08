using System;
using Sys = Cosmos.System;
using SartoxOS.Utils;
using SartoxOS.Commands;
using SartoxOS.Accounts;
using Cosmos.Core;
using Cosmos.System.FileSystem.VFS;
using System.Text;
using Cosmos.System.ExtendedASCII;
using SartoxOS.Applications;
using System.IO;

namespace SartoxOS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Registering Extended ASCII encoding...");
            Encoding.RegisterProvider(CosmosEncodingProvider.Instance);
            Console.InputEncoding = Encoding.GetEncoding(437);
            Console.OutputEncoding = Encoding.GetEncoding(437);

            ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Loading virtual FS...");
            VFSManager.RegisterVFS(Reference.FAT);
            if (Reference.FAT.GetVolumes().Count > 0) ColorConsole.WriteLine(ConsoleColor.Green, "Sucessfully loaded the virtual FS!");
            else ColorConsole.WriteLine(ConsoleColor.Red, "Uh-oh, couldn't load the virtual FS...");

            ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Initializing SSE...");
            Global.CPU.InitSSE();
            ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Initializing Float...");
            Global.CPU.InitFloat();

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
            ColorConsole.WriteLine(ConsoleColor.Green, "Welcome to Sartox OS v" + Reference.Version + "!");
        }

        protected override void Run()
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Login to your user account.");
            login: ColorConsole.Write(ConsoleColor.White, "User");
            ColorConsole.Write(ConsoleColor.Yellow, " => ");
            string user = Console.ReadLine();
            ColorConsole.Write(ConsoleColor.White, "Password");
            ColorConsole.Write(ConsoleColor.Yellow, " => ");
            string pass = Console.ReadLine();

            if (AccMan.Exist(user) && AccMan.GetPassword(user, true) == pass)
            {
                Reference.UserAccount = new Acc(user, pass);
                CmdMan.Init();
            }
            else
            {
                ColorConsole.WriteLine(ConsoleColor.Red, "Incorrect credentials.");
                goto login;
            }
        }
    }
}
