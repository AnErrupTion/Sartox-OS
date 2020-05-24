using Cosmos.HAL.Drivers.PCI.Video;

namespace SartoxOS.Utils
{
    public static class DrawUtils
    {
        public static void DrawLine(VMWareSVGAII driver, int x, int y, int x2, int y2, uint color)
        {
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = w;
            int shortest = h;
            if (!(longest > shortest))
            {
                longest = h;
                shortest = w;
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                driver.SetPixel((uint)x, (uint)y, color);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        public static void DrawRectangle(VMWareSVGAII driver, int x, int y, int w, int h, uint color)
        {
            DrawLine(driver, x, y, x, y + h, color);
            DrawLine(driver, x, y, x + w, y, color);
            DrawLine(driver, x, y + h, x + w, y + h, color);
            DrawLine(driver, x + w, y, x + w, y + h, color);
        }

        public static void FillRectangle(VMWareSVGAII driver, int x, int y, int w, int h, uint color)
        {
            DrawLine(driver, x, y, x, y + h, color);
            DrawLine(driver, x, y, x + w, y, color);
            DrawLine(driver, x, y + h, x + w, y + h, color);
            DrawLine(driver, x + w, y, x + w, y + h, color);

            for (int i = 0; i < h; i++) DrawLine(driver, x, y + i, x + w, y + i, color);
        }

        public static int DrawChar(VMWareSVGAII driver, char c, int x, int y, uint color, Font f)
        {
            int index = 0;
            for (int i = 0; i < f.Char.Count; i++)
                if (c == f.Char[i])
                {
                    index = i;
                    break;
                }

            byte width = f.Width[index];

            int z = 0;
            for (int p = y; p < y + f.Height[index]; p++)
                for (int i = x; i < x + width; i++)
                {
                    if (f.Data[index][z] == 1) driver.SetPixel((uint)i, (uint)p, color);
                    z++;
                }

            return width;
        }

        public static void DrawString(VMWareSVGAII driver, string pc, string c, int x, int y, uint color)
        {
            Font f = Fonts.Consolas14_cff;
            if (pc != c) DoChar(driver, pc, x, y, 0x0, f); // Clears the previous string.
            DoChar(driver, c, x, y, color, f); // Draws the new string.
        }

        private static void DoChar(VMWareSVGAII driver, string c, int x, int y, uint color, Font f)
        {
            int totalwidth = 0;
            for (int i = 0; i < c.Length; i++)
            {
                var ch = c[i];
                if (ch == ' ') totalwidth += f.Width[0];
                else if (ch == '\t') totalwidth += f.Width[0] * 4;
                else totalwidth += DrawChar(driver, ch, x + totalwidth, y, color, f);
            }
        }
    }
}
