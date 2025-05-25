using System;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace bot
{
    class CharachterControl
    {
        public static void TryToAttackMob()
        {
            Click();
            RandomDelaySleep(100);
            PreventFromRunningFarAway();
        }

        static void Click()
        {
            Input.mouse_event(Input.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Input.mouse_event(Input.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        static void PreventFromRunningFarAway()
        {
            for (int i = 0; i < 3; i++)
            {
                Input.keybd_event((byte)Keys.S, 0, Input.KEYBOARDEVENTF_KEYDOWN, 0);
                Input.keybd_event((byte)Keys.S, 0, Input.KEYBOARDEVENTF_KEYUP, 0);
                SendKeys.SendWait("s");
                Thread.Sleep(50);
            }
        }

        public static void AttackMobAndWait(int delay)
        {
            SendKeys.SendWait("1");
            RandomDelaySleep(delay);
        }

        public static void GetLoot()
        {
            for (int j = 0; j < 50; j++)
            {
                Input.keybd_event((byte)Keys.X, 0, Input.KEYBOARDEVENTF_KEYDOWN, 0);
                Input.keybd_event((byte)Keys.X, 0, Input.KEYBOARDEVENTF_KEYUP, 0);
                Thread.Sleep(100);
            }
        }

        public static void PressKeyBoardButton(byte key)
        {
            Input.keybd_event(key, 0, Input.KEYBOARDEVENTF_KEYDOWN, 0);
            Input.keybd_event(key, 0, Input.KEYBOARDEVENTF_KEYUP, 0);
        }

        public static void PressKeyBoardButton(Keys key)
        {
            Input.keybd_event((byte)key, 0, Input.KEYBOARDEVENTF_KEYDOWN, 0);
            Input.keybd_event((byte)key, 0, Input.KEYBOARDEVENTF_KEYUP, 0);
        }

        static void RandomDelaySleep(float delayInMilliseconds)
        {
            if (delayInMilliseconds < 5) delayInMilliseconds = 5;

            float dispersion = 20;
            float percentsFromDelay = delayInMilliseconds / 100 * dispersion;

            var rand = new Random();
            int randomDelay = rand.Next(-Convert.ToInt32(percentsFromDelay), Convert.ToInt32(percentsFromDelay));

            Thread.Sleep(Convert.ToInt32(delayInMilliseconds) + randomDelay);
        }
    }
}
