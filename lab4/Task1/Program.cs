using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Task1
{
    public static class GdiMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
    }
    
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var rnd = new Random();
            var graphics = Graphics.FromHdc(GdiMethods.GetWindowDC(GdiMethods.GetDesktopWindow()));
            const ushort width = 5;
            const ushort height = 5;
            const byte pw = 100;
            for (int x = 0, y = 0, xc = pw, yc = pw;;
                x += (x < 0 || x > 1920 - width) ? xc = -xc : xc, y += (y < 0 || y > 1080 - height) ? yc = -yc : yc)
            {
                graphics.DrawRectangle(
                    new Pen(Color.FromArgb(255, Color.FromArgb(rnd.Next())), pw),
                    x,
                    y,
                    width,
                    height
                );
            }
        }
    }
}