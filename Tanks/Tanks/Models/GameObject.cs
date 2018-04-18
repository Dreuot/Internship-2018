using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    abstract class GameObject
    {
        public Bitmap Sprite { get; }
        public int Width => Sprite.Width;
        public int Height => Sprite.Height;
        public double Speed { get; set; }
        public Direction Direction { get; set; }
        public Point Position { get; set; }

        private DateTime lastUpdate { get; set; }

        public GameObject(string sprite, int speed = 0, Point position = default, Direction direction = Direction.None)
        {
            Sprite = new Bitmap(sprite);
            Speed = speed;
            Position = position;
        }

        public void Render(Graphics g)
        {
            g.DrawImage(Sprite, Position);
        }

        public void Update()
        {
            lastUpdate = DateTime.Now;
        }
    }
}
