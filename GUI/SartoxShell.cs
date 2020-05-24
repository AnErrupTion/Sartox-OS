using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL.Drivers.PCI.Video;
using SartoxOS.GUI.Input;
using SartoxOS.Utils;
using Cosmos.System;

namespace SartoxOS.GUI
{
    public static class SartoxShell
    {
        private static string LastText;

        public static void Init()
        {
            ColorConsole.WriteLine(ConsoleColor.White, "=> Loading driver...");
            Reference.Driver = new VMWareSVGAII();
            Reference.Driver.SetMode(Reference.Width, Reference.Height);

            // In development
            bool running = true;
            int index = 0;
            int defY = 50;
            while (running)
            {
                KeyEvent e = KeyboardManager.ReadKey();
                string text = e.KeyChar.ToString();
                int x = 50 + index;
                //if (x >= Reference.Width) defY += 10;
                DrawUtils.DrawString(Reference.Driver, LastText, text, x, defY, 0x255);
                LastText = text;

                index += 10;
                Reference.Driver.Update(0, 0, Reference.Width, Reference.Height);
            }
        }
    }
}
