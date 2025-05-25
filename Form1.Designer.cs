using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace bot
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Button button1;
        private Button button2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // button1
            this.button1.Location = new System.Drawing.Point(12, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start Bot";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);

            // button2
            this.button2.Location = new System.Drawing.Point(585, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(203, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "OpenTestForm";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 88);
            this.Controls.Add(button2);
            this.Controls.Add(button1);
            this.Name = "Form1";
            this.Text = "RF Bot";
            this.ResumeLayout(false);
        }
    }
}
