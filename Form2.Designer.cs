using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace bot
{
    partial class Form2
    {
        private IContainer components = null;
        private Button button1;
        private CheckBox checkBox1;
        private Label label1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.checkBox1 = new CheckBox();
            this.button1 = new Button();
            this.label1 = new Label();

            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(157, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(80, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;

            // 
            // button1
            // 
            this.button1.Location = new Point(347, 276);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(233, 146);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";

            // 
            // Form2
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(label1);
            this.Controls.Add(button1);
            this.Controls.Add(checkBox1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new EventHandler(Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Windows Form Designer generated code

        private void Form2_Load(object sender, EventArgs e)
        {
            // Можно добавить логику при загрузке формы
        }

        #endregion

        private CheckBox checkBox1;
        private Button button1;
        private Label label1;
    }
}
