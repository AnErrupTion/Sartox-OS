using System;
using System.IO;
using SartoxOS.Utils;
using System.Text;

namespace SartoxOS.Accounts
{
    public static class AccMan
    {
        public static string GetPassword(string name, bool decrypt = false)
        {
            string accPath = Reference.RootPath + @"Accs\" + name + ".txt";
            string password = string.Empty;

            if (!Directory.Exists(Reference.RootPath + "Accs")) ColorConsole.WriteLine(ConsoleColor.Red, "This account doesn't exist.");
            if (File.Exists(accPath))
            {
                ColorConsole.WriteLine(ConsoleColor.White, "Getting password...");
                password = File.ReadAllText(accPath).Split(":")[1];
                ColorConsole.WriteLine(ConsoleColor.Green, "Got password.");
            }
            else ColorConsole.WriteLine(ConsoleColor.Red, "This account doesn't exist.");

            if (decrypt) password = Encoding.ASCII.GetString(Convert.FromBase64String(password));
            return password;
        }

        public static bool Exist(string name)
        {
            string accPath = Reference.RootPath + @"Accs\" + name + ".txt";
            bool exists = false;

            if (!Directory.Exists(Reference.RootPath + "Accs")) ColorConsole.WriteLine(ConsoleColor.Red, "This account doesn't exist.");
            if (File.Exists(accPath)) exists = true;
            else ColorConsole.WriteLine(ConsoleColor.Red, "This account doesn't exist.");

            return exists;
        }
    }
}
