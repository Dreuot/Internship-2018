using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Bullet : GameObject
    {
        public Bullet(string sprite = @"Assets/Bullet.png", int speed = 0, PointF position = default, Direction direction = Direction.None) : base(sprite, speed, position, direction)
        {
        }
    }
}
