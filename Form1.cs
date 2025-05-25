using System;
using System.Windows.Forms;

namespace bot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bot started!");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.Show();
        }
    }
}
