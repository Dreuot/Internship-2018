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
        public Bullet Shoot(int speed, Point point, Direction direction)
        {
           Bullet bullet = new Bullet
           {
               Speed = (int)(speed * 1.5),
               Direction = direction
           };
           bullet.Position = new Point(point.X + bullet.Width, point.Y + bullet.Height);

           return bullet;
        }
    }
}
