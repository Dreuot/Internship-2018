using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.Models;

namespace Tanks.Contollers
{
    class GameController
    {
        private Settings settings;
        private List<Tank> tanks;
        private List<Bullet> bullets;
        private List<Bullet> enemyBullets;
        private List<Apple> apples;
        private Player player;
        private DateTime lastUpdate;
        Random r = new Random();

        private int score;
        public int Score
        {
            get
            {
                return score;
            }
            private set
            {
                score = value;
                OnScoreChange(score);
            }
        }

        public event Action<int> OnScoreChange;

        public Player Player => player;

        public GameController(Settings s)
        {
            settings = s;
            tanks = new List<Tank>(s.EnemyCount);
            apples = new List<Apple>(settings.AppleCount);
            bullets = new List<Bullet>();
            enemyBullets = new List<Bullet>();

            player = new Player() { Position = new PointF(0, 0), Direction = Direction.Up, Speed = settings.Speed};
            lastUpdate = DateTime.Now;
            OnScoreChange += (x) => { };

            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < settings.EnemyCount; i++)
            {
                Tank tank = new Tank();
                tank.Position = RandomPosition(new Size(tank.Width, tank.Height));
                tank.Speed = settings.Speed;
                tank.Direction = Direction.Up;

                tanks.Add(tank);
            }

            for (int i = 0; i < settings.AppleCount; i++)
            {
                Apple apple = new Apple();
                apple.Position = RandomPosition(new Size(apple.Width, apple.Height));
                apples.Add(apple);
            }

            player = new Player() { Position = new PointF(0, 0), Direction = Direction.Up, Speed = settings.Speed };
            Score = 0;
        }

        private void AddApple()
        {
            if (apples.Count < settings.AppleCount)
                apples.Add(new Apple( position: RandomPosition(new Size(30, 30))));
        }

        public void Update()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
                for (int j = 0; j < tanks.Count; j++)
                {
                    if (bullets[i].Collides(tanks[j]))
                    {
                        tanks.Remove(tanks[j]);
                        j--;
                        bullets.Remove(bullets[i]);
                        i--;
                        break;
                    }
                }
            }

            foreach (var b in enemyBullets)
            {
                b.Update();
            }

            foreach (var tank in tanks)
            {
                tank.Update();
                Bullet b = tank.Shoot();
                if (b != null)
                    enemyBullets.Add(b);
            }

            for (int i = 0; i < apples.Count; i++)
            {
                if(apples[i].Collides(player))
                {
                    Score += 100;
                    apples.Remove(apples[i]);
                }
            }

            player.Update();

            AddApple();

            CheckPlayerBound();
            CheckObjectBound();
            ChangeDirecion();
        }

        private void CheckPlayerBound()
        {
            PointF p = Player.Position;
            if (p.X < 0)
            {
                p.X = 0;
            }

            if (p.X + Player.Width > settings.Width)
            {
                p.X = settings.Width - Player.Width;
            }

            if (p.Y < 0)
            {
                p.Y = 0;
            }

            if (p.Y + Player.Height > settings.Height)
            {
                p.Y = settings.Height - Player.Height;
            }

            Player.Position = p;
        }

        private void CheckBulletBound()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                PointF p = bullets[i].Position;
                if ((p.X < 0) || (p.X + bullets[i].Width > settings.Width) || (p.Y < 0) || (p.Y + bullets[i].Height > settings.Height))
                {
                    bullets.Remove(bullets[i]);
                    i--;
                }
            }
        }

        private void CheckObjectBound()
        {
            foreach (var tank in tanks)
            {
                PointF p = tank.Position;
                if (p.X < 0)
                {
                    p.X = 0;
                    tank.Direction = Direction.Right;
                }

                if (p.X + tank.Width > settings.Width)
                { 
                    p.X = settings.Width - tank.Width;
                    tank.Direction = Direction.Left;
                }

                if (p.Y < 0)
                {
                    p.Y = 0;
                    tank.Direction = Direction.Down;
                }

                if (p.Y + tank.Height > settings.Height)
                {
                    p.Y = settings.Height - tank.Height;
                    tank.Direction = Direction.Up;
                }

                tank.Position = p;
            }
        }

        private void ChangeDirecion()
        {
            foreach (var tank in tanks)
            {
                TimeSpan dt = DateTime.Now - tank.LastTurn;

                if (r.Next(0, 100) > 90 && dt.Seconds > 1)
                    tank.Direction = (Direction)r.Next(1, 5);
            }
        }

        public void PlayerShoot()
        {
            bullets.Add(Player.Shoot());
        }

        private PointF RandomPosition(Size size)
        {
            int x = r.Next(0, settings.Width - size.Width);
            int y = r.Next(0, settings.Height - size.Height);

            return new PointF(x, y);
        }

        public Bitmap Render(Bitmap image)
        {
            Graphics g = Graphics.FromImage(image);
            g.FillRectangle(Brushes.Black, new Rectangle(0, 0, image.Width, image.Height));

            foreach (var tank in tanks)
                tank.Render(g);

            foreach (var apple in apples)
                apple.Render(g);

            foreach (var bullet in bullets)
                bullet.Render(g);

            foreach (var e_bullet in enemyBullets)
                e_bullet.Render(g);

            player.Render(g);

            return image;
        }
    }
}
