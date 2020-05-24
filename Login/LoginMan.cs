using System;
using SartoxOS.Utils;
using SartoxOS.Commands;
using SartoxOS.Accounts;
using System.Text;

namespace SartoxOS.Login
{
    public static class LoginMan
    {
        public static void Init()
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Login to your user account.");
            login: ColorConsole.Write(ConsoleColor.White, "User");
            ColorConsole.Write(ConsoleColor.Yellow, " => ");
            string user = Console.ReadLine();
            ColorConsole.Write(ConsoleColor.White, "Password");
            ColorConsole.Write(ConsoleColor.Yellow, " => ");
            string pass = Console.ReadLine();

            if (AccMan.Exist(user) && Encoding.ASCII.GetString(Convert.FromBase64String(AccMan.GetPassword(user))) == pass)
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
