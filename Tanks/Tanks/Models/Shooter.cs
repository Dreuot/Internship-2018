using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Shooter
    {
        public Bullet Shoot(int speed, PointF point, Direction direction, Size size)
        {
            Bullet bullet = new Bullet
            {
                Speed = (int)(speed * 1.5),
                Direction = direction
            };

            PointF p = new PointF();
            switch (direction)
            {
                case Direction.Up:
                    p.X = point.X + (size.Width / 2) - (bullet.Sprite.Size.Width / 2);
                    p.Y = point.Y - bullet.Sprite.Size.Height;
                    break;
                case Direction.Down:
                    p.X = point.X + (size.Width / 2) - (bullet.Sprite.Size.Width / 2);
                    p.Y = point.Y + size.Height;
                    break;
                case Direction.Left:
                    p.X = point.X - bullet.Sprite.Size.Width;
                    p.Y = point.Y + (size.Height / 2) - (bullet.Sprite.Size.Height / 2);
                    break;
                case Direction.Right:
                    p.X = point.X + size.Width;
                    p.Y = point.Y + (size.Height / 2) - (bullet.Sprite.Size.Height / 2);
                    break;
            }
            bullet.Position = p;

            return bullet;
        }
    }
}
