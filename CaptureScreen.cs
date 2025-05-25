using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace bot
{
    class CaptureScreen
    {
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);
        
        public static Bitmap CaptureWindow(IntPtr hWnd)
        {
            Rectangle rect = NativeMethodsForWindow.GetWindowRect(hWnd);
            int width = rect.Width;
            int height = rect.Height;

            using (Bitmap bmp = new Bitmap(width, height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                IntPtr hdc = g.GetHdc();
                PrintWindow(hWnd, hdc, 0);
                g.ReleaseHdc(hdc);
                return new Bitmap(bmp);
            }
        }
    }
}
