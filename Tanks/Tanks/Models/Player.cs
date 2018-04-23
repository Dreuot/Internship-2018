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
        public DateTime LastShoot { get; private set; }

        public Player(int speed = 0, PointF position = default, Direction direction = Direction.None) : base(@"Assets/player.png", speed, position, direction)
        {
            shooter = new Shooter();
        }

        public Bullet Shoot()
        {
            LastShoot = DateTime.Now;
            return shooter.Shoot((int)(Speed * 1.7), Position, Direction, Sprite.Size);
        }
    }
}
