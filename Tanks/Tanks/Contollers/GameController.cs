﻿using System;
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
        private List<Apple> apples;
        private Player player;
        private DateTime lastUpdate;
        Random r = new Random();

        public GameController(Settings s)
        {
            settings = s;
            tanks = new List<Tank>(s.EnemyCount);
            apples = new List<Apple>(settings.AppleCount);

            player = new Player() { Position = new PointF(0, 0), Direction = Direction.Up, Speed = settings.Speed};
            lastUpdate = DateTime.Now;

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
        }

        private void AddApple()
        {
            if (apples.Count < settings.AppleCount)
                apples.Add(new Apple("apple.png", RandomPosition(new Size(30, 30))));
        }

        public void Update()
        {
            foreach (var tank in tanks)
                tank.Update();
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
                g.DrawImage(tank.Sprite, tank.Position);

            foreach (var apple in apples)
                g.DrawImage(apple.Sprite, apple.Position);

            g.DrawImage(player.Sprite, player.Position);

            return image;
        }
    }
}