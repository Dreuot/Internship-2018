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

namespace Tanks
{
    public partial class GameForm : Form
    {
        private Settings settings;
        private Graphics g;

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
    }
}
