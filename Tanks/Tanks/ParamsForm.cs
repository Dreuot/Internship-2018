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
    public partial class ParamsForm : Form
    {
        public Settings Settings { get; private set; }
        public bool StartGame = false;

        public ParamsForm()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out int width);
            int.TryParse(textBox2.Text, out int height);
            int.TryParse(textBox3.Text, out int enemies);
            int.TryParse(textBox4.Text, out int apples);
            int.TryParse(textBox5.Text, out int speed);

            width = width == 0 ? 800 : width;
            height = height == 0 ? 600 : height;
            enemies = enemies == 0 ? 5 : enemies;
            apples = apples == 0 ? 5 : apples;
            speed = speed == 0 ? 50 : speed;

            Settings = new Settings(width, height, enemies, speed, apples);
            StartGame = true;
            this.Close();
        }

        private bool CorrentInput(KeyPressEventArgs e)
        {
            if ((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8)
                return false;

            return true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = CorrentInput(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = CorrentInput(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = CorrentInput(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = CorrentInput(e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = CorrentInput(e);
        }
    }
}
