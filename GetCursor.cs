using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace bot
{
    class GetCursor
    {
        public static Bitmap CaptureCursor()
        {
            Bitmap bmp;
            IntPtr hicon;
            CURSORINFO ci = new CURSORINFO();
            ci.cbSize = Marshal.SizeOf(ci);
            if (GetCursorInfo(out ci))
            {
                if (ci.flags == CURSOR_SHOWING)
                {
                    hicon = CopyIcon(ci.hCursor);
                    if (GetIconInfo(hicon, out _))
                    {
                        Icon ic = Icon.FromHandle(hicon);
                        bmp = ic.ToBitmap();
                        return bmp;
                    }
                }
            }

            return null;
        }

        public static Boolean IsCursorRed()
        {
            var cursorScreenShot = GetCursor.CaptureCursor();

            if (cursorScreenShot.GetPixel(1, 2).R > 180)
            {
                return true;
            }
            return false;
        }


        #region Class Variables
        public const Int32 CURSOR_SHOWING = 0x00000001;

        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            public bool fIcon;         // Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies 
            public Int32 xHotspot;     // Specifies the x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot 
            public Int32 yHotspot;     // Specifies the y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot 
            public IntPtr hbmMask;     // (HBITMAP) Specifies the icon bitmask bitmap. If this structure defines a black and white icon, 
            public IntPtr hbmColor;    // (HBITMAP) Handle to the icon color bitmap. This member can be optional if this 
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public Int32 x;
            public Int32 y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CURSORINFO
        {
            public Int32 cbSize;        // Specifies the size, in bytes, of the structure. 
            public Int32 flags;         // Specifies the cursor state. This parameter can be one of the following values:
            public IntPtr hCursor;          // Handle to the cursor. 
            public POINT ptScreenPos;       // A POINT structure that receives the screen coordinates of the cursor. 
        }
        #endregion


        #region Class Functions
        [DllImport("user32.dll", EntryPoint = "GetCursorInfo")]
        public static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll", EntryPoint = "CopyIcon")]
        public static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport("user32.dll", EntryPoint = "GetIconInfo")]
        public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);
        #endregion
    }
}
