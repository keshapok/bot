using OpenCvSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bot
{
    public partial class Form1 : Form
    {
        private GlobalKeyboardHook _globalKeyboardHook;
        private static bool _botRunning = true;

        public Form1()
        {
            InitializeComponent();

            // Горячая клавиша F12 для паузы/запуска
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.HookedKeys.Add(Keys.F12);
            _globalKeyboardHook.KeyUp += ToggleBot;
            _globalKeyboardHook.Hook();
        }

        private void ToggleBot(object sender, KeyEventArgs e)
        {
            _botRunning = !_botRunning;
            Console.WriteLine(_botRunning ? "Бот возобновлён" : "Бот остановлен");
            e.Handled = true;
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Process[] processes = Process.GetProcessesByName("rf_online.bin");

                if (processes.Length == 0)
                {
                    MessageBox.Show("Процесс rf_online.bin не найден!");
                    return;
                }

                Process gameProcess = processes[0];
                var template = new Bitmap(@"..\..\..\Images\templateOfHealthBar.png");

                while (true)
                {
                    if (!_botRunning) continue;

                    try
                    {
                        var img1 = WorkWithImages.BringProcessToFrontAndCaptureGDIWindow(gameProcess);
                        Thread.Sleep(500);

                        var img2 = WorkWithImages.BringProcessToFrontAndCaptureGDIWindow(gameProcess);
                        var difference = WorkWithImages.GetDiffInTwoImages(img1, img2);

                        var contours = WorkWithImages.FindCountoursAtImage(difference);
                        var point = WorkWithImages.GetBiggestCountourCoordinates(contours);

                        var windowRect = NativeMethodsForWindow.GetAbsoluteClientRect(gameProcess.MainWindowHandle);

                        int x = point.X + windowRect.X;
                        int y = point.Y + windowRect.Y;

                        Input.SmoothMouseMove(x, y, 1);
                        Thread.Sleep(900);

                        if (GetCursor.IsCursorRed())
                        {
                            CharachterControl.TryToAttackMob();
                        }

                        // Замена Direct3DCapture на GDI-захват
                        var currentScreen = WorkWithImages.BringProcessToFrontAndCaptureGDIWindow(gameProcess);

                        if (WorkWithImages.IsImageMatchWithTemplate(currentScreen, template))
                        {
                            int counter = 0;
                            CharachterControl.AttackMobAndWait(1);
                            CharachterControl.PressKeyBoardButton(Keys.F1);

                            try
                            {
                                while (WorkWithImages.IsImageMatchWithTemplate(
                                        WorkWithImages.BringProcessToFrontAndCaptureGDIWindow(gameProcess), template))
                                {
                                    CharachterControl.AttackMobAndWait(1);
                                    Thread.Sleep(1010);
                                    counter++;

                                    if ((counter % 6) == 0)
                                    {
                                        CharachterControl.AttackMobAndWait(100);
                                        CharachterControl.PressKeyBoardButton(Keys.F2);
                                        CharachterControl.AttackMobAndWait(100);
                                    }

                                    if ((counter % 13) == 0)
                                    {
                                        CharachterControl.AttackMobAndWait(100);
                                        CharachterControl.PressKeyBoardButton(Keys.F1);
                                        CharachterControl.AttackMobAndWait(100);
                                    }
                                }
                            }
                            finally
                            {
                                CharachterControl.GetLoot();
                                CharachterControl.PressKeyBoardButton(Keys.Escape);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка: " + ex.Message);
                    }
                }
            });
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.Show();
        }
    }
}
