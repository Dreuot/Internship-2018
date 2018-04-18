using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    public class Settings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int EnemyCount { get; set; }
        public int Speed { get; set; }
        public int AppleCount { get; set; }

        public Settings(int width = 800, int height = 600, int enemyCount = 5, int speed = 50, int appleCount = 5)
        {
            Width = width;
            Height = height;
            EnemyCount = enemyCount;
            Speed = speed;
            AppleCount = appleCount;
        }
    }
}
