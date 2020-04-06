using System;
using System.IO;
using SartoxOS.Utils;

namespace SartoxOS.Applications
{
    public static class Textpad
    {
        public static void Run(string file = null)
        {
            Console.Clear();

            string curDir = file;
            if (file == null)
            {
                lolfile: ColorConsole.Write(ConsoleColor.White, "File to read => ");
                string ftr = Console.ReadLine();
                curDir = Reference.CurrentDir;

                if (!ftr.Contains(Reference.RootPath)) curDir += @"\" + ftr;
                else curDir = ftr;

                if (!File.Exists(curDir))
                {
                    ColorConsole.WriteLine(ConsoleColor.Red, "File doesn't exist.");
                    goto lolfile;
                }
            }
            else
            {
                if (!file.Contains(Reference.RootPath)) curDir += @"\" + file;
            }

            string[] lines = File.ReadAllLines(curDir);
            Console.Clear();
            for (int i = 0; i < lines.Length; i++)
                ColorConsole.WriteLine(ConsoleColor.Cyan, lines[i]);
        }
    }
}
