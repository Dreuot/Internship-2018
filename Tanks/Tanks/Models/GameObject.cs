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
        private int speed;

        public Bitmap Sprite { get; }
        public int Width => Sprite.Width;
        public int Height => Sprite.Height;
        public double Speed { get; set; }
        public Direction Direction { get; set; }
        public PointF Position { get; set; }

        private DateTime lastUpdate { get; set; }

        public GameObject(string sprite, int speed = 0, PointF position = default, Direction direction = Direction.None)
        {
            Sprite = new Bitmap(sprite);
            Speed = speed;
            Position = position;
        }

        public void Render(Graphics g)
        {
            g.DrawImage(Sprite, Position);
        }

        public bool Collides(GameObject obj)
        {
            bool collide = false;


            return collide;
        }

        public void Update()
        {
            TimeSpan dt = DateTime.Now - lastUpdate;
            PointF p = Position;
            switch (Direction)
            {
                case Direction.Up:
                    p.Y -= speed * dt.Seconds;
                    break;
                case Direction.Down:
                    p.Y += speed * dt.Seconds;
                    break;
                case Direction.Left:
                    p.X -= speed * dt.Seconds;
                    break;
                case Direction.Right:
                    p.X += speed * dt.Seconds;
                    break;
                default:
                    break;
            }

            Position = p;
            lastUpdate = DateTime.Now;
        }
    }
}
