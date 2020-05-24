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

            lolfile: string curDir = Reference.CurrentDir;
            if (file == null)
            {
                ColorConsole.Write(ConsoleColor.Yellow, "File to read => ");
                string ftr = Console.ReadLine();

                if (!ftr.Contains(Reference.RootPath)) curDir += @"\" + ftr;
                else curDir = ftr;
            }
            else
            {
                if (!file.Contains(Reference.RootPath)) curDir += @"\" + file;
                else curDir = file;
            }

            if (!File.Exists(curDir))
            {
                ColorConsole.WriteLine(ConsoleColor.Red, "File doesn't exist.");
                if (file == null) goto lolfile; else goto end;
            }

            string[] lines = File.ReadAllLines(curDir);
            Console.Clear();
            for (int i = 0; i < lines.Length; i++)
                ColorConsole.WriteLine(ConsoleColor.Cyan, lines[i]);

            end:;
        }
    }
}
