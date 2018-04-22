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
        //private int speed;
        private Direction direction;
        private Bitmap upDirectionSprite;
        private DateTime lastUpdate { get; set; }

        public Bitmap Sprite { get; private set; }
        public int Width => Sprite.Width;
        public int Height => Sprite.Height;
        public double Speed { get; set; }

        public Direction Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
                LastTurn = DateTime.Now;
                Sprite = RotateSprite(value);
            }
        }

        public PointF Position { get; set; }
        public DateTime LastUpdate => lastUpdate;
        public DateTime LastTurn { get; private set; }

        public GameObject(string sprite, int speed = 0, PointF position = default, Direction direction = Direction.None)
        {
            Sprite = new Bitmap(sprite);
            upDirectionSprite = new Bitmap(sprite);
            Speed = speed;
            Position = position;
            lastUpdate = DateTime.Now;
        }

        public void Render(Graphics g)
        {
            g.DrawImage(Sprite, Position);
        }

        public bool Collides(GameObject obj)
        {
            return !((Position.X + Width <= obj.Position.X)
                || (Position.X > obj.Position.X + obj.Width)
                || (Position.Y + Height <= obj.Position.Y)
                || (Position.Y > obj.Position.Y + obj.Height));
        }

        public void Update()
        {
            TimeSpan dt = DateTime.Now - lastUpdate;
            PointF p = Position;
            switch (Direction)
            {
                case Direction.Up:
                    p.Y -= (float)Speed * dt.Milliseconds / 1000;
                    break;
                case Direction.Down:
                    p.Y += (float)Speed * dt.Milliseconds / 1000;
                    break;
                case Direction.Left:
                    p.X -= (float)Speed * dt.Milliseconds / 1000;
                    break;
                case Direction.Right:
                    p.X += (float)Speed * dt.Milliseconds / 1000;
                    break;
                default:
                    break;
            }

            Position = p;
            lastUpdate = DateTime.Now;
        }

        private Bitmap RotateSprite(Direction direction)
        {
            Bitmap newSprite = new Bitmap(upDirectionSprite);
            switch (Direction)
            {
                case Direction.Up:
                    break;
                case Direction.Down:
                    newSprite.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case Direction.Left:
                    newSprite.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                case Direction.Right:
                    newSprite.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                default:
                    break;
            }

            return newSprite;
        }
    }
}
