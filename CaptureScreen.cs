using System.Drawing;
using System.Runtime.InteropServices;

namespace bot
{
    class CaptureScreen
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

        public static Bitmap CaptureWindow(IntPtr handle)
        {
            Rectangle rect = NativeMethodsForWindow.GetWindowRect(handle);
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
