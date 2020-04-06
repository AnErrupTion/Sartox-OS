using System;
using SartoxOS.Utils;
using SartoxOS.GUI;
using System.IO;
using SartoxOS.Applications;
using Cosmos.Core;
using SartoxOS.Errors;

namespace SartoxOS.Commands
{
    public static class CmdMan
    {
        public static void Init()
        {
            try
            {
                commands: ColorConsole.Write(ConsoleColor.Cyan, Reference.UserAccount.GetUsername());
                ColorConsole.Write(ConsoleColor.White, Reference.UserAccess);
                ColorConsole.Write(ConsoleColor.Yellow, Reference.CurrentDir);
                ColorConsole.Write(ConsoleColor.White, " => ");
                string cmd = Console.ReadLine();

                if (cmd == "help")
                {
                    ColorConsole.WriteLine(ConsoleColor.White, "help - Shows all commands (this one).\ntime - Shows the current date and time.\ngui - Shows a nice GUI.\nstop - Shuts down kernel.\nreboot - Reboots kernel.\ngetmem - Gets the amount of used and total memory.\ntextpad [file] - The official text editor (read-only) for Sartox OS where [file] is the file to read. If not set, the user will be asked for the file to read.\ncd - Changes current directory.\nls - Lists all files and directories of the current directory.\ncls - Clears the console.\nabout - Shows useful informations about Sartox OS.\nmkdir <directory> - Creates a directory where <directory> is the name of the directory.\nmk <file> - Creates a file where <file> is the name of the file.\nrm <file> - Removes the specified file.\nrmdir <directory> - Removes the specified directory. It's recursive.\nmove <source file/directory> <dest file/directory> - Moves a file or directory from the (source) to the (dest)ination.\ncopy <source file> <dest file> - Copies a file from the (source) to the (dest)ination. It overwrites the destination file (if it exists).");
                    goto commands;
                }
                else if (cmd == "gui") SimpleGui.Init();
                else if (cmd == "time")
                {
                    ColorConsole.WriteLine(ConsoleColor.White, DateTime.Now.ToString());
                    goto commands;
                }
                else if (cmd == "getmem")
                {
                    ColorConsole.WriteLine(ConsoleColor.White, (CPU.GetEndOfKernel() + 1024) / 1048576 + " MB / " + CPU.GetAmountOfRAM() + " MB (used/total)");
                    goto commands;
                }
                else if (cmd == "textpad")
                {
                    Textpad.Run();
                    Console.ReadKey();
                    goto commands;
                }
                else if (cmd.StartsWith("textpad "))
                {
                    Textpad.Run(cmd.Split(" ")[1]);
                    Console.ReadKey();
                    goto commands;
                }
                else if (cmd.StartsWith("cd "))
                {
                    string newDir = cmd.Split(" ")[1];
                    if (Directory.Exists(newDir))
                    {
                        if (newDir.Contains(Reference.RootPath)) Reference.CurrentDir = newDir;
                        else Reference.CurrentDir += newDir;
                    }
                    else ColorConsole.WriteLine(ConsoleColor.Red, "Directory not found.");
                    goto commands;
                }
                else if (cmd == "ls")
                {
                    string[] fad = Directory.GetDirectories(Reference.CurrentDir);
                    for (int i = 0; i < fad.Length; i++)
                        ColorConsole.WriteLine(ConsoleColor.Cyan, fad[i]);

                    fad = Directory.GetFiles(Reference.CurrentDir);
                    for (int i = 0; i < fad.Length; i++)
                        ColorConsole.WriteLine(ConsoleColor.Cyan, fad[i]);
                    goto commands;
                }
                else if (cmd == "cls")
                {
                    Console.Clear();
                    goto commands;
                }
                else if (cmd == "about")
                {
                    ColorConsole.WriteLine(ConsoleColor.White, "Sartox OS v" + Reference.Version + " made by ShiningLea.\n\nSartox OS is an operating system made in C# with the COSMOS kit. The goal here is to provide a minimal but working operating system for any purpose. Currently made by only one developer, it's highly maintained and will continue to be as long as the dev wants to (oh wait, I'm literally writing this message...)");
                    goto commands;
                }
                else if (cmd == "stop") Power.Shutdown();
                else if (cmd.StartsWith("mkdir ")) Directory.CreateDirectory(cmd.Split(" ")[1]);
                else if (cmd.StartsWith("mk ")) File.Create(cmd.Split(" ")[1]);
                else if (cmd.StartsWith("rm "))
                {
                    string file = cmd.Split(" ")[1];
                    if (File.Exists(file)) File.Delete(file);
                    else ColorConsole.WriteLine(ConsoleColor.Red, "The specified file doesn't exist.");
                }
                else if (cmd.StartsWith("rmdir "))
                {
                    string dir = cmd.Split(" ")[1];
                    if (Directory.Exists(dir)) Directory.Delete(dir, true);
                    else ColorConsole.WriteLine(ConsoleColor.Red, "The specified directory doesn't exist.");
                }
                else if (cmd.StartsWith("move "))
                {
                    string move = cmd.Split(" ")[1];
                    if (Directory.Exists(move) || File.Exists(move)) Directory.Move(move, cmd.Split(" ")[2]);
                    else ColorConsole.WriteLine(ConsoleColor.Red, "The source directory/file doesn't exist.");
                }
                else if (cmd.StartsWith("copy ")) File.Copy(cmd.Split(" ")[1], cmd.Split(" ")[2], true);
                else if (cmd == "reboot") Power.Restart();
                else
                {
                    ColorConsole.WriteLine(ConsoleColor.Red, "Invalid command.");
                    goto commands;
                }
            }
            catch (Exception e)
            {
                Reference.Debugger.Send("ERROR : " + e.Message);
                ErrorScreen.Init(e.Message);
            }
        }
    }
}
