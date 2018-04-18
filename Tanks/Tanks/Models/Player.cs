using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Player : GameObject, IShooter
    {
        private Shooter shooter;

        public Player(string sprite, int speed = 0, Point position = default, Direction direction = Direction.None) : base(sprite, speed, position, direction)
        {
            shooter = new Shooter();
        }

        public Bullet Shoot()
        {
             return shooter.Shoot((int)(Speed * 1.5), Position, Direction);
        }
    }
}
