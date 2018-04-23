using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tanks.Models;
using Tanks.Contollers;
using System.Threading;

namespace Tanks
{
    public partial class GameForm : Form
    {
        private Settings settings;
        private Graphics g;
        private GameController gc;

        public GameForm()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            ParamsForm param = new ParamsForm();
            param.FormClosing += (s, ev) =>
            {
                if (param.StartGame)
                {
                    settings = param.Settings;
                    this.Width = settings.Width;
                    this.Height = settings.Height;

                    this.Show();
                }
                else
                {
                    this.Close();
                }

                Settings sett = new Settings();
                sett.Width = pictureBox1.Width;
                sett.Height = pictureBox1.Height;
                sett.AppleCount = settings.AppleCount;
                sett.EnemyCount = settings.EnemyCount;
                sett.Speed = settings.Speed;

                gc = new GameController(sett);
                pictureBox1.Image = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
                this.KeyPreview = true;
                gc.OnScoreChange += (score) => this.label1.Text = score.ToString();
                gc.OnGameOver += () =>
                {
                    timer1.Stop();
                    if(MessageBox.Show("Вы проиграли!\nПовторить?", "Игра закончена", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        gc.Reset();
                        timer1.Start();
                    }
                };

                StartGame();
            };

            param.Show();
        }

        private void GameForm_Shown(object sender, EventArgs e)
        {
            Hide();
        }

        private void FillField()
        {
            g.FillRectangle(Brushes.Black, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
        }

        private void StartGame()
        {
            timer1.Tick += Render;
            timer1.Interval = 1000 / 100;
            timer1.Start();
        }

        private void Render(object sender, EventArgs e)
        {
            gc.Update();
            pictureBox1.Image = gc.Render((Bitmap)pictureBox1.Image);
        }

        private void GameForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyCatch(e);
        }

        private void KeyCatch(KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                case 'ц':
                    gc.Player.Direction = Direction.Up;
                    break;
                case 'ф':
                case 'a':
                    gc.Player.Direction = Direction.Left;
                    break;
                case 'ы':
                case 's':
                    gc.Player.Direction = Direction.Down;
                    break;
                case 'в':
                case 'd':
                    gc.Player.Direction = Direction.Right;
                    break;
                case 'r':
                case ' ':
                    gc.PlayerShoot();
                    break;
                default:
                    break;
            }
        }
    }
}
