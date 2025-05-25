using System;
using System.Drawing;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using OpenCvSharp;
using bot.Properties;

namespace bot
{
    public partial class Form1 : Form
    {
        private GlobalKeyboardHook _globalKeyboardHook;
        private static bool _botRunning = true;

        public Form1()
        {
            InitializeComponent();
            HookStopKey();
        }

        private void HookStopKey()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.HookedKeys.Add(Keys.F12);
            _globalKeyboardHook.KeyUp += (sender, args) =>
            {
                _botRunning = !_botRunning;
                Console.WriteLine(_botRunning ? "Бот возобновил работу" : "Бот остановлен");
                args.Handled = true;
            };
            _globalKeyboardHook.Hook();
        }

        private async void StartBotButton_Click(object sender, EventArgs e)
        {
            this.Text = "Бот запущен";
            await Task.Run(() => BotLoop());
        }

        private void BotLoop()
        {
            Process[] processes = Process.GetProcessesByName("rf_online.bin");

            if (processes.Length == 0) return;

            var gameProcess = processes[0];
            var template = new Bitmap(@"..\..\..\Images\templateOfHealthBar.png");

            while (true)
            {
                if (!_botRunning) continue;

                try
                {
                    // Захват экрана
                    var img1 = WorkWithImages.BringProcessToFrontAndCaptureGDIWindow(gameProcess);
                    Thread.Sleep(500);
                    var img2 = WorkWithImages.BringProcessToFrontAndCaptureGDIWindow(gameProcess);

                    // Поиск изменений
                    var diff = WorkWithImages.GetDiffInTwoImages(img1, img2);
                    var contours = WorkWithImages.FindCountoursAtImage(diff);
                    var point = WorkWithImages.GetBiggestCountourCoordinates(contours);

                    var windowRect = NativeMethodsForWindow.GetAbsoluteClientRect(gameProcess.MainWindowHandle);

                    int x = point.X + windowRect.X;
                    int y = point.Y + windowRect.Y;

                    Input.SmoothMouseMove(x, y, 1);
                    Thread.Sleep(900);

                    if (GetCursor.IsCursorRed())
                        CharachterControl.TryToAttackMob();

                    if (WorkWithImages.IsImageMatchWithTemplate(CaptureScreen.CaptureWindow(gameProcess.MainWindowHandle), template))
                    {
                        CharachterControl.AttackMobAndWait(1);
                        CharachterControl.PressKeyBoardButton(Keys.F1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Простая кнопка без дизайнеров
            var button = new Button
            {
                Text = "Запустить бота",
                Width = 200,
                Height = 40,
                Location = new System.Drawing.Point(50, 50)
            };
            button.Click += StartBotButton_Click;

            this.Controls.Add(button);
            this.Width = 300;
            this.Height = 150;
            this.Text = "RF-Bot";

            this.ResumeLayout(false);
        }
    }
}
