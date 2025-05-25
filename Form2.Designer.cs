namespace bot
{
    partial class Form1
    {
        private Button button1;
        private Button button2;

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.button2 = new Button();

            // Настройка кнопок
            this.button1.Text = "Start Bot";
            this.button1.Click += new EventHandler(this.Button1_Click);

            this.button2.Text = "Open Form2";
            this.button2.Click += new EventHandler(this.Button2_Click);

            this.Controls.Add(button1);
            this.Controls.Add(button2);
        }
    }
}
