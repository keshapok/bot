using System;
using System.Diagnostics;
using System.Threading;

namespace bot
{
    class WorkWithProcess
    {
        public static void BringProcessWindowToFront(Process proc)
        {
            if (proc == null) return;
            IntPtr handle = proc.MainWindowHandle;
            int i = 0;

            while (!NativeMethodsForWindow.IsWindowInForeground(handle))
            {
                if (i == 0) Thread.Sleep(1);

                if (NativeMethodsForWindow.IsIconic(handle))
                    NativeMethodsForWindow.ShowWindow(handle, NativeMethodsForWindow.WindowShowStyle.Restore);
                else
                    NativeMethodsForWindow.SetForegroundWindow(handle);

                Thread.Sleep(1);

                if (NativeMethodsForWindow.IsWindowInForeground(handle))
                {
                    Thread.Sleep(5);
                    return;
                }

                if (i > 620) throw new Exception("Не удалось активировать окно игры");
                i++;
            }
        }
    }
}
