using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace bot
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread.Sleep(2000);

            var process = Process.GetCurrentProcess();
            WorkWithProcess.BringProcessWindowToFront(process);
            var windowHandle = process.MainWindowHandle;

            // Захватываем окно игры
            Bitmap screenCapture = CaptureScreen.CaptureWindow(windowHandle);
            Bitmap template = new Bitmap(@"..\..\..\Images\templateOfHealthBar.png");

            if (WorkWithImages.IsImageMatchWithTemplate(screenCapture, template))
            {
                MessageBox.Show("Шаблон найден!");
            }
            else
            {
                MessageBox.Show("Шаблон НЕ найден.");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var gkh = new GlobalKeyboardHook();
            gkh.HookedKeys.Add(Keys.A);
            gkh.HookedKeys.Add(Keys.B);
            gkh.KeyUp += Gkh_KeyUp;
            gkh.Hook();
        }

        private void Gkh_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Клавиша нажата в Form2");
            e.Handled = true;
        }
    }
}
