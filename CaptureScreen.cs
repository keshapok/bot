using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace bot
{
    class CaptureScreen
    {
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, uint nFlags);

        public static Bitmap CaptureWindow(IntPtr handle)
        {
            var rect = NativeMethodsForWindow.GetWindowRect(handle);
            int width = rect.Width;
            int height = rect.Height;

            using (Bitmap sourceImage = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            using (Graphics g = Graphics.FromImage(sourceImage))
            {
                IntPtr hdc = g.GetHdc();
                PrintWindow(handle, hdc, 0);
                g.ReleaseHdc(hdc);

                return new Bitmap(sourceImage);
            }
        }
    }
}
