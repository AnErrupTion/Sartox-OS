using System;
using SartoxOS.Utils;
using SartoxOS.GUI;
using System.IO;
using SartoxOS.Applications;
using Cosmos.Core;
using SartoxOS.Errors;
using Sys = Cosmos.System;
using SartoxOS.Login;
using System.Collections.Generic;
using SartoxOS.Settings;

namespace SartoxOS.Commands
{
    public static class CmdMan
    {
        public static void Init()
        {
            try
            {
                ColorConsole.WriteLine(ConsoleColor.Cyan, "Classic Shell v1.1");
                commands: ColorConsole.Write(ConsoleColor.Cyan, Reference.UserAccount.GetUsername());
                ColorConsole.Write(ConsoleColor.White, Reference.UserAccess);
                ColorConsole.Write(ConsoleColor.Yellow, Reference.CurrentDir);
                ColorConsole.Write(ConsoleColor.White, " => ");
                string cmd = Console.ReadLine();

                if (cmd == Reference.Commands[0])
                {
                    string all = string.Empty;
                    foreach (string comnd in Reference.Commands)
                        all += $"{comnd} - ";
                    ColorConsole.WriteLine(ConsoleColor.White, all.Remove(all.Length - 3));
                    goto commands;
                }
                else if (cmd == Reference.Commands[1]) SimpleGui.Init();
                else if (cmd == Reference.Commands[2])
                {
                    ColorConsole.WriteLine(ConsoleColor.White, $"{UnixTime.Now()} (day/month/year hour:minute:second)");
                    goto commands;
                }
                else if (cmd == Reference.Commands[3])
                {
                    ColorConsole.WriteLine(ConsoleColor.White, $"{MemoryManager.UsedMemory()} MB / {MemoryManager.TotalMemory()} MB (used / total)");
                    goto commands;
                }
                else if (cmd == Reference.Commands[4])
                {
                    Textpad.Run();
                    Console.ReadKey();
                    goto commands;
                }
                else if (cmd.StartsWith($"{Reference.Commands[4]} "))
                {
                    Textpad.Run(cmd.Split(" ")[1]);
                    Console.ReadKey();
                    goto commands;
                }
                else if (cmd.StartsWith($"{Reference.Commands[5]} "))
                {
                    string lang = cmd.Split(" ")[1];
                    string setName = "keyboard_layout";
                    if (lang == "fr")
                    {
                        Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.FR_Standard());
                        SettingsMan.Add(setName, "fr");
                    }
                    else if (lang == "us")
                    {
                        Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.US_Standard());
                        SettingsMan.Add(setName, "en");
                    }
                    else if (lang == "de")
                    {
                        Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.DE_Standard());
                        SettingsMan.Add(setName, "de");
                    }
                    else ColorConsole.WriteLine(ConsoleColor.Red, "Unknown keyboard layout.");
                    goto commands;
                }
                else if (cmd.StartsWith($"{Reference.Commands[6]} "))
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
                else if (cmd == Reference.Commands[7])
                {
                    List<string[]> fad = new List<string[]>
                    {
                        Directory.GetDirectories(Reference.CurrentDir),
                        Directory.GetFiles(Reference.CurrentDir)
                    };

                    for (int i = 0; i < fad.Count; i++)
                    {
                        string[] list = fad[i];
                        for (int y = 0; y < list.Length; y++)
                            ColorConsole.WriteLine(ConsoleColor.Cyan, list[y]);
                    }
                    goto commands;
                }
                else if (cmd == Reference.Commands[8])
                {
                    Console.Clear();
                    LoginMan.Init();
                }
                else if (cmd == Reference.Commands[9])
                {
                    Console.Clear();
                    goto commands;
                }
                else if (cmd == Reference.Commands[10])
                {
                    ColorConsole.WriteLine(ConsoleColor.White, $"Sartox OS ver {Reference.Version}, kernel ver {Reference.KernelVersion} made by ShiningLea.\n\nSartox OS is an operating system made in C# with the COSMOS kit. The goal here is to provide a minimal but working operating system for any purpose. Currently made by only one developer, it's highly maintained and will continue to be as long as the dev wants to (oh wait, I'm literally writing this message...)");
                    goto commands;
                }
                else if (cmd == Reference.Commands[11]) Power.Shutdown();
                else if (cmd.StartsWith($"{Reference.Commands[12]} "))
                {
                    Directory.CreateDirectory(cmd.Split(" ")[1]);
                    goto commands;
                }
                else if (cmd.StartsWith($"{Reference.Commands[13]} "))
                {
                    File.Create(cmd.Split(" ")[1]);
                    goto commands;
                }
                else if (cmd.StartsWith($"{Reference.Commands[14]} "))
                {
                    File.WriteAllText(cmd.Split(" ")[1], string.Empty);
                    goto commands;
                }
                else if (cmd == Reference.Commands[14])
                {
                    ColorConsole.WriteLine(ConsoleColor.White, "Touch what?");
                    goto commands;
                }
                else if (cmd.StartsWith($"{Reference.Commands[15]} "))
                {
                    File.WriteAllText(cmd.Split(" ")[1], cmd.Split(" ")[2]);
                    goto commands;
                }
                else if (cmd.StartsWith($"{Reference.Commands[16]} "))
                {
                    string file = cmd.Split(" ")[1];
                    if (File.Exists(file)) File.Delete(file);
                    else ColorConsole.WriteLine(ConsoleColor.Red, "The specified file doesn't exist.");
                    goto commands;
                }
                else if (cmd.StartsWith($"{Reference.Commands[17]} "))
                {
                    string dir = cmd.Split(" ")[1];
                    if (Directory.Exists(dir)) Directory.Delete(dir, true);
                    else ColorConsole.WriteLine(ConsoleColor.Red, "The specified directory doesn't exist.");
                    goto commands;
                }
                else if (cmd.StartsWith($"{Reference.Commands[18]} "))
                {
                    string move = cmd.Split(" ")[1];
                    if (Directory.Exists(move) || File.Exists(move)) Directory.Move(move, cmd.Split(" ")[2]);
                    else ColorConsole.WriteLine(ConsoleColor.Red, "The source directory/file doesn't exist.");
                    goto commands;
                }
                else if (cmd.StartsWith($"{Reference.Commands[19]} "))
                {
                    File.Copy(cmd.Split(" ")[1], cmd.Split(" ")[2], true);
                    goto commands;
                }
                else if (cmd == Reference.Commands[20]) Power.Restart();
                else if (cmd == Reference.Commands[21]) throw new Exception("Crash initialized by user.");
                else if (cmd == Reference.Commands[22]) SartoxShell.Init();
                else
                {
                    ColorConsole.WriteLine(ConsoleColor.Red, "Invalid command.");
                    goto commands;
                }
            }
            catch (Exception e)
            {
                Global.mDebugger.Send("ERROR : " + e.Message + e.HResult.ToString());
                ErrorScreen.Init(e.Message, e.HResult);
            }
        }
    }
}
