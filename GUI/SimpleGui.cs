using System;
using Cosmos.HAL.Drivers.PCI.Video;
using SartoxOS.Utils;
using SartoxOS.GUI.Input;
using Cosmos.HAL;
using Cosmos.System;
using SartoxOS.Commands;
using Power = SartoxOS.Utils.Power;

namespace SartoxOS.GUI
{
    public static class SimpleGui
    {
        private static int Frames = 0;
        private static int FPS = 0;
        private static int deltaT = 0;

        private static string LastFPS;
        private static string LastRAM;

        public static void Init()
        {
            // Initializes the GUI...
            ColorConsole.WriteLine(ConsoleColor.White, "=> Loading driver...");
            Reference.Driver = new VMWareSVGAII();
            Reference.Driver.SetMode(Reference.Width, Reference.Height);
            //driver.Fill(0, 0, Width, Height, 0x6619135); // Equivalent to driver.Clear(0x6619135);

            DrawButton(Reference.Driver, 50, 110, 10, 0x255, "Restart");
            DrawButton(Reference.Driver, 50, 140, 115, 0x255, "Back to console");
            /*DrawUtils.DrawRectangle(driver, 50, 140, 30, 90, 0x255);
            DrawUtils.DrawString(driver, string.Empty, "Back to Console (BETA)", 50, 140, 0x16777215);*/

            deltaT = RTC.Second;

            // ...and now the mouse
            ColorConsole.WriteLine(ConsoleColor.White, "=> Loading mouse...");
            Reference.Mouse = new Mouse(Reference.Width, Reference.Height);

            DrawComponents(); // Draws a simple mouse, rectangle and handles events
        }

        private static void DrawButton(VMWareSVGAII driver, int x, int y, int widthAdd, uint col, string text)
        {
            DrawUtils.DrawRectangle(driver, x, y, y + widthAdd, x - 30, col);
            DrawUtils.DrawString(driver, string.Empty, text, x, y, col);
        }

        private static void DrawComponents()
        {
            ushort updateWidth = Math.Min((ushort)(Reference.Width / 2.56), Reference.Width);
            ushort updateHeight = Math.Min((ushort)(Reference.Height / 2.56), Reference.Height);
            ushort updateX = (ushort)((Reference.Width - updateWidth) / 2);
            ushort updateY = (ushort)((Reference.Height - updateHeight) / 2);

            bool running = true;
            while (running)
            {
                if (deltaT != RTC.Second)
                {
                    FPS = Frames;
                    Frames = 0;
                    deltaT = RTC.Second;
                }

                byte b = (byte)((double)Reference.Mouse.X / (double)Reference.Width * 255);
                byte rg = (byte)((double)Reference.Mouse.Y / (double)Reference.Height * 255);

                uint col = b + (uint)(rg << 8) + (uint)(rg << 16);

                string fps = $"FPS : {FPS}";
                DrawUtils.DrawString(Reference.Driver, LastFPS, fps, 50, 50, 0x16777215);
                LastFPS = fps;

                string ram = $"Memory : {MemoryManager.UsedMemory()} MB / {MemoryManager.TotalMemory()} MB";
                DrawUtils.DrawString(Reference.Driver, LastRAM, ram, 50, 80, 0x16777215);
                LastRAM = ram;

                if (CheckClick(50, 110, 120, 20))
                    Power.Restart();

                if (CheckClick(50, 140, 255, 20))
                {
                    running = false;
                    Reference.Driver.Clear(0);
                    Reference.Driver.Update(0, 0, Reference.Width, Reference.Height);
                    Reference.Mouse = null;
                    Reference.Driver = null;
                    CmdMan.Init();
                }

                for (ushort x = updateX; x < updateX + updateWidth; x++)
                    for (ushort y = updateY; y < updateY + updateHeight; y++)
                        Reference.Driver.SetPixel(x, y, col);

                Reference.Mouse.Draw(Reference.Driver);
                Reference.Driver.Update(0, 0, Reference.Width, Reference.Height);

                Frames++;
            }
        }

        private static bool CheckClick(int x, int y, int width, int height)
        {
            if (Reference.Mouse.X <= x + width && Reference.Mouse.X >= x)
                if (Reference.Mouse.Y <= y + height && Reference.Mouse.Y >= y)
                    if (MouseManager.MouseState == MouseState.Left)
                        return true;
            return false;
        }
    }
}
