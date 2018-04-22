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

        public Player(string sprite = @"Assets/player.png", int speed = 0, PointF position = default, Direction direction = Direction.None) : base(sprite, speed, position, direction)
        {
            shooter = new Shooter();
        }

        public Bullet Shoot()
        {
            return shooter.Shoot((int)(Speed * 1.7), Position, Direction, Sprite.Size);
            //Bullet bullet = new Bullet
            //{
            //    Speed = (int)(this.Speed * 1.5),
            //    Direction = this.Direction
            //};

            //PointF p = new PointF();
            //switch (Direction)
            //{
            //    case Direction.Up:
            //        p.X = Position.X + (Sprite.Size.Width / 2) - (bullet.Sprite.Size.Width / 2);
            //        p.Y = Position.Y - bullet.Sprite.Size.Height;
            //        break;
            //    case Direction.Down:
            //        p.X = Position.X + (Sprite.Size.Width / 2) - (bullet.Sprite.Size.Width / 2);
            //        p.Y = Position.Y + Sprite.Size.Height;
            //        break;
            //    case Direction.Left:
            //        p.X = Position.X - bullet.Sprite.Size.Width;
            //        p.Y = Position.Y + (Sprite.Size.Height / 2) - (bullet.Sprite.Size.Height / 2);
            //        break;
            //    case Direction.Right:
            //        p.X = Position.X + Sprite.Size.Width;
            //        p.Y = Position.Y + (Sprite.Size.Height / 2) - (bullet.Sprite.Size.Height / 2);
            //        break;
            //}
            //bullet.Position = p;

            //return bullet;
        }
    }
}
