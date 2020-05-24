using System.IO;
using SartoxOS.Utils;
using System;

namespace SartoxOS.Settings
{
    public static class SettingsMan
    {
        public static void Add(string name, string value)
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Adding setting '" + name + "'...");

            // Always check (for example if the file has been deleted).
            if (!File.Exists(Reference.Settings)) File.Create(Reference.Settings);

            File.AppendAllText(Reference.Settings, name + ":" + value);
            ColorConsole.WriteLine(ConsoleColor.Green, "Added setting '" + name + "' with value '" + value + "'.");
        }

        public static string Get(string name)
        {
            string value = string.Empty;
            if (!File.Exists(Reference.Settings)) return "Settings file doesn't exist";

            string[] lines = File.ReadAllLines(Reference.Settings);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains(name)) value = line.Split(":")[1];
            }

            return value;
        }
    }
}
