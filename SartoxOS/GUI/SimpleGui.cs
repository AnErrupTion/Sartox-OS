using System;
using Cosmos.HAL.Drivers.PCI.Video;
using SartoxOS.Utils;
using Sys = Cosmos.System;
using SartoxOS.GUI.Input;

namespace SartoxOS.GUI
{
    public static class SimpleGui
    {
        private static VMWareSVGAII driver;
        private static Mouse m;

        private static readonly ushort Width = 800;
        private static readonly ushort Height = 600;

        public static void Init()
        {
            // Initializes the GUI
            ColorConsole.WriteLine(ConsoleColor.White, "=> Loading driver...");
            driver = new VMWareSVGAII();
            driver.SetMode(Width, Height);
            driver.Clear(0x255);

            // And now the mouse
            ColorConsole.WriteLine(ConsoleColor.White, "=> Loading mouse...");
            m = new Mouse(Width, Height);

            DrawComponents(); // Draws a simple mouse, rectangle and handles events
        }

        private static void DrawComponents()
        {
            ushort updateWidth = Math.Min((ushort)(Width / 2.56), Width);
            ushort updateHeight = Math.Min((ushort)(Height / 2.56), Height);
            ushort updateX = (ushort)((Width - updateWidth) / 2);
            ushort updateY = (ushort)((Height - updateHeight) / 2);

            bool OK = true;
            while (OK)
            {
                byte b = (byte)(((double)m.X / (double)Width) * 255);
                byte rg = (byte)(((double)m.Y / (double)Height) * 255);

                uint col = (uint)b + (uint)(rg << 8) + (uint)(rg << 16);

                if (Sys.MouseManager.MouseState == Sys.MouseState.Left && (m.X >= updateX && m.Y >= updateY))
                    col = 0x00000000;

                DrawForm(updateX, updateY, updateWidth, updateHeight, col);

                m.Draw(driver);
                driver.Update(0, 0, Width, Height);
            }
        }

        private static void DrawForm(uint xx, uint yy, uint width, uint height, uint col)
        {
            for (uint x = xx; x < xx + width; x++)
                for (uint y = yy; y < yy + height; y++)
                    driver.SetPixel(x, y, col);
        }
    }
}
