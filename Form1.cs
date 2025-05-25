using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using OpenCvSharp;

namespace bot
{
    public partial class Form1 : Form
    {
        private GlobalKeyboardHook _globalKeyboardHook;
        private static bool _botRunning = true;

        public Form1()
        {
            InitializeComponent();
            SetupStopKey();
        }

        private void SetupStopKey()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.HookedKeys.Add(Keys.F12);
            _globalKeyboardHook.KeyUp += ToggleBot;
            _globalKeyboardHook.Hook();
        }

        private void ToggleBot(object sender, KeyEventArgs e)
        {
            _botRunning = !_botRunning;
            Console.WriteLine(_botRunning ? "Бот возобновил работу" : "Бот остановлен");
            e.Handled = true;
        }

        private async void StartBotButton_Click(object sender, EventArgs e)
        {
            this.Text = "Бот запущен";
            await Task.Run(() => BotLoop());
        }

        private void BotLoop()
        {
            var processes = Process.GetProcessesByName("rf_online.bin");

            if (processes.Length == 0) return;

            var gameProcess = processes[0];
            var template = new Bitmap(@"..\..\..\Images\templateOfHealthBar.png");

            while (true)
            {
                if (!_botRunning) continue;

                try
                {
                    var img1 = WorkWithImages.BringProcessToFrontAndCaptureGDIWindow(gameProcess);
                    Thread.Sleep(500);
                    var img2 = WorkWithImages.BringProcessToFrontAndCaptureGDIWindow(gameProcess);

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
            this.Width = 300;
            this.Height = 150;
            this.Text = "RF-Bot";

            var button = new Button { Text = "Запустить бота", Dock = DockStyle.Fill };
            button.Click += StartBotButton_Click;

            this.Controls.Add(button);
        }
    }
}
