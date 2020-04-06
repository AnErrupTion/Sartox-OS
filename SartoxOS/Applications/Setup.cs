using System;
using SartoxOS.Utils;
using SartoxOS.Accounts;
using System.IO;
using System.Text;

namespace SartoxOS.Applications
{
    public static class Setup
    {
        public static bool Init()
        {
            Console.Clear();
            dosetup: ColorConsole.Write(ConsoleColor.White, "Install Sartox OS? (y/n) ");
            string answer = Console.ReadLine();
            bool setup;

            if (answer == "y")
            {
                setup = true;

                ColorConsole.WriteLine(ConsoleColor.White, "Create your user account.");
                ColorConsole.Write(ConsoleColor.White, "User => ");
                string user = Console.ReadLine();
                ColorConsole.Write(ConsoleColor.White, "Password => ");
                string pass = Console.ReadLine();

                ColorConsole.WriteLine(ConsoleColor.White, "=> Creating user account...");
                Reference.UserAccount = new Acc(user, pass);
                Reference.UserAccount.Create();

                ColorConsole.WriteLine(ConsoleColor.White, "=> Finishing installation...");
                File.WriteAllText(Reference.RootPath + "Installed.txt", "Sartox OS is installed... no, there is no easter egg here.", Encoding.ASCII);
            }
            else if (answer == "n") setup = false;
            else
            {
                ColorConsole.WriteLine(ConsoleColor.Red, "Invalid answer.");
                goto dosetup;
            }

            return setup;
        }
    }
}
