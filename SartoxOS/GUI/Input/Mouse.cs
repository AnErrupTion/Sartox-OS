using Sys = Cosmos.System;
using Cosmos.HAL.Drivers.PCI.Video;

namespace SartoxOS.GUI.Input
{
    public class Mouse
    {
        // Mouse X coord.
        public ushort X
        {
            get
            {
                return (ushort)Sys.MouseManager.X;
            }
        }

        // Mouse Y coord.
        public ushort Y
        {
            get
            {
                return (ushort)Sys.MouseManager.Y;
            }
        }

        // Screen width - sets max X coord.
        public uint ScreenWidth
        {
            get
            {
                return Sys.MouseManager.ScreenWidth;
            }
            set
            {
                Sys.MouseManager.ScreenWidth = value;
            }
        }

        // Screen height - sets max Y coord.
        public uint ScreenHeight
        {
            get
            {
                return Sys.MouseManager.ScreenHeight;
            }
            set
            {
                Sys.MouseManager.ScreenHeight = value;
            }
        }

        public Mouse(uint screenWidth, uint screenHeight)
        {
            // Initializes the mouse manager.
            Sys.MouseManager.ScreenWidth = screenWidth;
            Sys.MouseManager.ScreenHeight = screenHeight;
        }

        public int LastDrawX { get; private set; } = 0;

        public int LastDrawY { get; private set; } = 0;

        private readonly uint[][] LastDrawPosCols = new uint[20][]
            {
                new uint[20],new uint[20],new uint[20],new uint[20],new uint[20],new uint[20],new uint[20],new uint[20],new uint[20],new uint[20],
                new uint[20],new uint[20],new uint[20],new uint[20],new uint[20],new uint[20],new uint[20],new uint[20],new uint[20],new uint[20],
            };

        // Sets the colours for the mouse - 0 is black and can be used as transparent in draw method.
        // 20 x 20 pixels allowed for mouse design.
        private readonly uint[][] MouseCols = new uint[20][]
        {
            new uint[20] { 0x0000000011, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x00FFFFFFFF, 0x0000000011, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000011, 0x00FFFFFFFF, 0x00FFFFFFFF, 0x0000000011, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 },
            new uint[20] { 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000011, 0x0000000011, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000, 0x0000000000 }
        };

        // Draws the mouse
        public void Draw(VMWareSVGAII vgaDriver)
        {
            // If the mouse hasn't moved, don't reset.
            if (LastDrawX != X || LastDrawY != Y)
            {
                // Clears the old mouse draw location.
                DoDraw(LastDrawX, LastDrawY, vgaDriver, LastDrawPosCols, true);

                // Stores the existing colours where the mouse will now be.
                for (ushort x = 0; x < 20; x++)
                    for (ushort y = 0; y < 20; y++)
                        LastDrawPosCols[y][x] = vgaDriver.GetPixel((ushort)(X + x), (ushort)(Y + y));

                // Updates the last draw mouse position to the new position.
                LastDrawX = X;
                LastDrawY = Y;
            }

            // Draws the mouse in it's current position - ensure it's always on top.
            DoDraw(X, Y, vgaDriver, MouseCols, false);
        }

        // Actually does the draw.
        private void DoDraw(int X, int Y, VMWareSVGAII vgaDriver, uint[][] mouseCol, bool allowBlack)
        {
            for (ushort x = 0; x < 20; x++)
                for (ushort y = 0; y < 20; y++)
                    if (allowBlack || mouseCol[y][x] != 0)
                        vgaDriver.SetPixel((ushort)(X + x), (ushort)(Y + y), mouseCol[y][x]);
        }
    }
}
