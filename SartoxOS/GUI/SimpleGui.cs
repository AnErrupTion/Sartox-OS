using System;
using Cosmos.HAL.Drivers.PCI.Video;
using SartoxOS.Utils;
using SartoxOS.GUI.Input;
using Cosmos.HAL;

namespace SartoxOS.GUI
{
    public static class SimpleGui
    {
        private static VMWareSVGAII driver;
        private static Mouse m;

        private static readonly ushort Width = 800;
        private static readonly ushort Height = 600;

        private static int Frames = 0;
        private static int FPS = 0;
        private static int deltaT = 0;

        private static string LastFPS;
        private static string LastRAM;

        public static void Init()
        {
            // Initializes the GUI
            ColorConsole.WriteLine(ConsoleColor.White, "=> Loading driver...");
            driver = new VMWareSVGAII();
            driver.SetMode(Width, Height);
            driver.Clear(0x0);

            deltaT = RTC.Second;

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

            bool running = true;
            if (running)
            {
                if (deltaT != RTC.Second)
                {
                    FPS = Frames;
                    Frames = 0;
                    deltaT = RTC.Second;
                }

                byte b = (byte)(((double)m.X / (double)Width) * 255);
                byte rg = (byte)(((double)m.Y / (double)Height) * 255);

                uint col = (uint)b + (uint)(rg << 8) + (uint)(rg << 16);

                string fps = "FPS : " + FPS;
                DrawUtils.DrawString(driver, LastFPS, fps, 50, 50, 0x255);
                LastFPS = fps;

                string ram = "RAM : " + MemoryManager.UsedMemory().ToString() + " MB / " + MemoryManager.TotalMemory().ToString() + " MB";
                DrawUtils.DrawString(driver, LastRAM, ram, 50, 80, 0x255);
                LastRAM = ram;

                for (ushort x = updateX; x < updateX + updateWidth; x++)
                    for (ushort y = updateY; y < updateY + updateHeight; y++)
                        driver.SetPixel(x, y, col);

                m.Draw(driver);
                driver.Update(0, 0, Width, Height);

                Frames++;
                DrawComponents();
            }
        }
    }
}
