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
            dosetup: ColorConsole.Write(ConsoleColor.Yellow, "Install Sartox OS? (y/n) ");
            string answer = Console.ReadLine();
            bool setup;

            if (answer == "y")
            {
                setup = true;

                doformat: ColorConsole.WriteLine(ConsoleColor.Yellow, "Sartox OS will now format your hard drive. Agree? (y/n)");
                string format = Console.ReadLine();
                if (format == "y")
                {
                    Reference.FAT.Format(Reference.RootPath.Replace(@":\", string.Empty), "FAT32", true);
                }
                else
                {
                    ColorConsole.WriteLine(ConsoleColor.Red, "Invalid answer.");
                    goto doformat;
                }

                ColorConsole.WriteLine(ConsoleColor.Yellow, "Create your user account.");
                ColorConsole.Write(ConsoleColor.White, "User");
                ColorConsole.Write(ConsoleColor.Yellow, " => ");
                string user = Console.ReadLine();
                ColorConsole.Write(ConsoleColor.White, "Password");
                ColorConsole.Write(ConsoleColor.Yellow, " => ");
                string pass = Console.ReadLine();

                ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Creating user account...");
                Reference.UserAccount = new Acc(user, pass);
                Reference.UserAccount.Create();
                ColorConsole.WriteLine(ConsoleColor.Green, "=> Created user account.");

                ColorConsole.WriteLine(ConsoleColor.Yellow, "=> Finishing installation...");
                File.WriteAllText(Reference.RootPath + "Installed.txt", "Sartox OS is installed... no, there is no easter egg here.", Encoding.ASCII);

                Power.Restart();
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
