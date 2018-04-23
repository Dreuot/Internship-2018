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
        private List<Wall> walls;
        private Player player;
        private DateTime lastUpdate;
        private DateTime lastShowInfo;
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
        public event Action OnGameOver;
        public event Action OnUpdate;

        public Player Player => player;

        public GameController(Settings s)
        {
            settings = s;
            OnScoreChange += (x) => { };
            OnGameOver += () => { };
            OnUpdate += () => { };

            Reset();
        }

        public void Reset()
        {
            tanks = new List<Tank>(settings.EnemyCount);
            apples = new List<Apple>(settings.AppleCount);
            bullets = new List<Bullet>();
            enemyBullets = new List<Bullet>();
            walls = Wall.CreateWalls(new Size(settings.Width, settings.Height));

            player = new Player() { Position = new PointF(0, 0), Direction = Direction.Up, Speed = settings.Speed };
            lastUpdate = DateTime.Now;

            for (int i = 0; i < settings.EnemyCount; i++)
            {
                Tank tank = new Tank();
                RandomPosition(tank);
                tank.Speed = settings.Speed;
                tank.Direction = Direction.Up;

                bool create = true;
                foreach (var wall in walls)
                {
                    if (tank.Collides(wall))
                        create = false;
                }

                if(create)
                    tanks.Add(tank);
            }

            for (int i = 0; i < settings.AppleCount; i++)
            {
                Apple apple = new Apple();
                RandomPosition(apple);

                apples.Add(apple);
            }

            player = new Player() { Position = new PointF(0, 0), Direction = Direction.Up, Speed = settings.Speed };
            Score = 0;
        }

        private void AddApple()
        {
            if (apples.Count < settings.AppleCount)
            {
                Apple apple = new Apple();
                RandomPosition(apple);
                apples.Add(apple);
            }
        }

        private void AddTanks()
        {
            if (tanks.Count < settings.EnemyCount)
            {
                Tank tank = new Tank(speed: settings.Speed, direction: (Direction)r.Next(1, 4));
                RandomPosition(tank);
                tanks.Add(tank);
            }

        }

        public string GameState()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(player.ToString());
            sb.Append("\n");
            foreach (var tank in tanks)
            {
                sb.Append(tank.ToString());
                sb.Append("\n");
            }

            foreach (var apple in apples)
            {
                sb.Append(apple.ToString());
                sb.Append("\n");
            }

            return sb.ToString();
        }

        private void RandomPosition(GameObject obj)
        {
            bool clear = true;
            Size size = obj.Sprite.Size;
            do
            {
                clear = true;
                int x = r.Next(0, settings.Width - size.Width);
                int y = r.Next(0, settings.Height - size.Height);
                obj.Position = new PointF(x, y);
                if (obj.Collides(player))
                    clear = false;

                foreach (var tank in tanks)
                {
                    if (obj == tank)
                        continue;

                    if (obj.Collides(tank))
                        clear = false;
                }

                foreach (var wall in walls)
                {
                    if (obj.Collides(wall))
                        clear = false;
                }

            } while (!clear);
        }

        private void CreateWalls()
        {
            Wall w = new Wall();
            Size ws = w.Sprite.Size;
        }

        public void Update()
        {
            var now = DateTime.Now;
            TimeSpan dt = now - lastUpdate;
            UpdateBullets(dt);

            UpdateTanks(dt);

            for (int i = 0; i < apples.Count; i++)
            {
                if (apples[i].Collides(player))
                {
                    Score += 100;
                    apples.Remove(apples[i]);
                }
            }

            UpdatePlayer(dt);

            AddApple();
            AddTanks();

            CheckPlayerBound();
            CheckTanksBound();
            ChangeDirecion();
            CheckBulletBound();

            if ((now - lastShowInfo).Milliseconds > 500)
            {
                OnUpdate();
                lastShowInfo = now;
            }

            lastUpdate = now;
        }

        private void UpdatePlayer(TimeSpan dt)
        {
            player?.Update(dt);
            if(player != null)
            {
                for (int i = 0; i < walls.Count; i++)
                {
                    if (player.Collides(walls[i]))
                    {
                        PointF p = new PointF();
                        p.X = player.Position.X;
                        p.Y = player.Position.Y;
                        switch (player.Direction)
                        {
                            case Direction.Up:
                                p.Y = 1 + walls[i].Position.Y + walls[i].Height;
                                break;
                            case Direction.Down:
                                p.Y = walls[i].Position.Y - player.Height - 1;
                                break;
                            case Direction.Left:
                                p.X = 1 + walls[i].Position.X + walls[i].Width;
                                break;
                            case Direction.Right:
                                p.X = walls[i].Position.X - player.Width - 1;
                                break;
                        }

                        player.Position = p;
                        break;
                    }
                }

                for (int i = 0; i < tanks.Count; i++)
                {
                    if (player.Collides(tanks[i]))
                    {
                        OnGameOver();
                        break;
                    }
                }
            }
        }

        private void UpdateTanks(TimeSpan dt)
        {
            Random r = new Random();
            foreach (var tank in tanks)
            {
                Bullet b = tank.Shoot();

                for (int i = 0; i < walls.Count; i++)
                {
                    if (tank.Collides(walls[i]))
                    {
                        PointF p = new PointF();
                        p.X = tank.Position.X;
                        p.Y = tank.Position.Y;
                        Direction dir;
                        switch (tank.Direction)
                        {
                            case Direction.Up:
                                p.Y += 2;
                                dir = (Direction)r.Next(3, 5);
                                break;
                            case Direction.Down:
                                p.Y -= 2;
                                dir = (Direction)r.Next(3, 5);
                                break;
                            case Direction.Left:
                                p.X += 2;
                                dir = (Direction)r.Next(1, 3);
                                break;
                            case Direction.Right:
                                p.X -= 2;
                                dir = (Direction)r.Next(1, 3);
                                break;
                            default:
                                dir = Direction.Up;
                                break;
                        }

                        tank.Position = p;
                        tank.Direction = dir;
                        break;
                    }
                }

                Action<Tank> Reverse = (Tank t) =>
                {
                    PointF p = tank.Position;
                    switch (t.Direction)
                    {
                        case Direction.Up:
                            p.Y += 2;
                            t.Direction = Direction.Down;
                            break;
                        case Direction.Down:
                            p.Y -= 2;
                            t.Direction = Direction.Up;
                            break;
                        case Direction.Left:
                            p.X += 2;
                            t.Direction = Direction.Right;
                            break;
                        case Direction.Right:
                            p.X -= 2;
                            t.Direction = Direction.Left;
                            break;
                    }

                    t.Position = p;
                };
        

                for (int i = 0; i < tanks.Count; i++)
                {
                    if (tank == tanks[i])
                        continue;

                    if(tank.Collides(tanks[i]))
                    {
                        Reverse(tank);
                    }
                }

                tank.Update(dt);
                if (b != null)
                    enemyBullets.Add(b);
            }
        }

        private void UpdateBullets(TimeSpan dt)
        {
            bool bulletRemoved = false;
            for (int i = 0; i < bullets.Count; i++)
            {
                bulletRemoved = false;
                bullets[i].Update(dt);
                for (int j = 0; j < walls.Count; j++)
                {
                    if (bullets[i].Collides(walls[j]))
                    {
                        bullets.Remove(bullets[i]);
                        bulletRemoved = true;
                        i--;
                        walls.Remove(walls[j]);
                        break;
                    }
                }

                if (bulletRemoved)
                    continue;

                for (int j = 0; j < tanks.Count; j++)
                {
                    if (bullets[i].Collides(tanks[j]))
                    {
                        tanks.Remove(tanks[j]);
                        Score += 500;
                        j--;
                        bullets.Remove(bullets[i]);
                        bulletRemoved = true;
                        i--;
                        break;
                    }
                }
            }

            for (int i = 0; i < enemyBullets.Count; i++)
            {
                bulletRemoved = false;
                for (int j = 0; j < walls.Count; j++)
                {
                    if (enemyBullets[i].Collides(walls[j]))
                    {
                        enemyBullets.Remove(enemyBullets[i]);
                        bulletRemoved = true;
                        i--;
                        walls.Remove(walls[j]);
                        break;
                    }
                }

                if (bulletRemoved)
                    continue;

                enemyBullets[i].Update(dt);
                if (enemyBullets[i].Collides(player))
                {
                    player.Speed = 0;
                    OnGameOver();
                }

            }
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

            for (int i = 0; i < enemyBullets.Count; i++)
            {
                PointF p = enemyBullets[i].Position;
                if ((p.X < 0) || (p.X + enemyBullets[i].Width > settings.Width) || (p.Y < 0) || (p.Y + enemyBullets[i].Height > settings.Height))
                {
                    enemyBullets.Remove(enemyBullets[i]);
                    i--;
                }
            }
        }

        private void CheckTanksBound()
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
            if((DateTime.Now - player.LastShoot).Milliseconds > 350)
            bullets.Add(Player.Shoot());
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

            foreach (var wall in walls)
                wall.Render(g);

            player.Render(g);

            return image;
        }
    }
}
