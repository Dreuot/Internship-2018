using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Tank : GameObject, IShooter
    {
        private Shooter shooter;
        private DateTime lastShoot;

        public Tank(string sprite = @"Assets/tank.png", int speed = 0, PointF position = default, Direction direction = Direction.None) : base(sprite, speed, position, direction)
        {
            shooter = new Shooter();
            lastShoot = DateTime.Now;
        }

        public Bullet Shoot()
        {
            
            if ((DateTime.Now - lastShoot).Seconds > 2)
            {
                lastShoot = DateTime.Now;
                return shooter.Shoot((int)(Speed * 1.7), Position, Direction, Sprite.Size);
            }
            else
            {
                return null;
            }
        }
    }
}
