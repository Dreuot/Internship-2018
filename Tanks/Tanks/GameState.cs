using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    public partial class GameState : Form
    {
        public GameState()
        {
            InitializeComponent();
        }

        public void Update(string state)
        {
            richTextBox1.Text = "";
            richTextBox1.Text = state;
        }
    }
}
