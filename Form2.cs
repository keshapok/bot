using System;
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
            MessageBox.Show("Тестовая кнопка нажата");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var gkh = new GlobalKeyboardHook();
            gkh.HookedKeys.Add(Keys.A);
            gkh.KeyUp += (s, ev) =>
            {
                Console.WriteLine("A нажата");
                ev.Handled = true;
            };
        }
    }
}
