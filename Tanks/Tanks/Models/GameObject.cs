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
        private Bitmap l_s;
        private Bitmap r_s;
        private Bitmap u_s;
        private Bitmap d_s;


        private Direction direction;
        private DateTime lastUpdate { get; set; }

        public Bitmap Sprite { get; protected set; }
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
                RotateSprite(value);
            }
        }

        public PointF Position { get; set; }
        public DateTime LastUpdate => lastUpdate;
        public DateTime LastTurn { get; private set; }

        public GameObject(string sprite, int speed = 0, PointF position = default, Direction direction = Direction.None)
        {
            SetSprites(sprite);
            Sprite = u_s;
            Speed = speed;
            Position = position;
            lastUpdate = DateTime.Now;
        }

        public GameObject(Bitmap sprite, int speed = 0, PointF position = default, Direction direction = Direction.None)
        {
            SetSprites(sprite);
            Sprite = u_s;
            Speed = speed;
            Position = position;
            lastUpdate = DateTime.Now;
        }

        private void SetSprites(string sprite)
        {
            u_s = u_s ?? new Bitmap(sprite);
            if (d_s == null)
            {
                d_s = new Bitmap(u_s);
                d_s.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }

            if (l_s == null)
            {
                l_s = new Bitmap(u_s);
                l_s.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }

            if (r_s == null)
            {
                r_s = new Bitmap(u_s);
                r_s.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
        }

        private void SetSprites(Bitmap image)
        {
            u_s = u_s ?? image;
            if (d_s == null)
            {
                d_s = new Bitmap(u_s);
                d_s.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }

            if (l_s == null)
            {
                l_s = new Bitmap(u_s);
                l_s.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }

            if (r_s == null)
            {
                r_s = new Bitmap(u_s);
                r_s.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
        }

        public void Render(Graphics g)
        {
            g.DrawImage(Sprite, Position.X, Position.Y, Width, Height);
            //g.DrawRectangle(Pens.Pink, new Rectangle((int)Position.X, (int)Position.Y, Width, Height));
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

        public void Update(TimeSpan dt)
        {
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

        private void RotateSprite(Direction direction)
        {
            switch (Direction)
            {
                case Direction.Up:
                    Sprite = u_s;
                    break;
                case Direction.Down:
                    Sprite = d_s;
                    break;
                case Direction.Left:
                    Sprite = l_s;
                    break;
                case Direction.Right:
                    Sprite = r_s;
                    break;
                default:
                    Sprite = u_s;
                    break;
            }
        }
    }
}
