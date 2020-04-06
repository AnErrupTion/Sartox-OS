using System;
using System.Text;
using System.IO;
using SartoxOS.Utils;

namespace SartoxOS.Accounts
{
    public class Acc
    {
        public string Name;
        public string Password;

        public Acc(string Name, string Password)
        {
            this.Name = Name;
            this.Password = Convert.ToBase64String(Encoding.ASCII.GetBytes(Password));
        }

        public string GetUsername()
        {
            return Name;
        }
        
        public void Create()
        {
            string accPath = Reference.RootPath + @"Accs\" + Name + ".txt";
            if (!Directory.Exists(Reference.RootPath + "Accs")) Directory.CreateDirectory(Reference.RootPath + "Accs");
            if (!File.Exists(accPath))
            {
                ColorConsole.WriteLine(ConsoleColor.White, "Creating account...");
                File.WriteAllText(accPath, Name + ":" + Password);
                ColorConsole.WriteLine(ConsoleColor.Green, "Account created.");
            }
            else ColorConsole.WriteLine(ConsoleColor.Red, "This account already exists.");
        }
    }
}
