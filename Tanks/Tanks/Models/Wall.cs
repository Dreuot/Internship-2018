using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Wall : GameObject
    {
        public Wall(PointF position = default) : base(@"Assets/wall.png", 0, position, Direction.None)
        {
        }

        public static List<Wall> CreateWalls (Size size)
        {
            Wall w = new Wall(new PointF(0, 0));
            int[][,] patterns = new int[8][,];
            patterns[0] = new int[4, 3]
            {
                { 0, 1, 0 },
                { 0, 1, 0 },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };
            patterns[1] = new int[4, 3]
            {
                { 0, 0, 1 },
                { 0, 0, 1 },
                { 0, 0, 0 },
                { 1, 1, 0 },
            };
            patterns[2] = new int[4, 3]
            {
                 { 0, 0, 0 },
                 { 0, 0, 0 },
                 { 1, 1, 0 },
                 { 0, 1, 0 },
            };
            patterns[3] = new int[4, 3]
            {
                 { 0, 1, 0 },
                 { 0, 1, 0 },
                 { 0, 1, 1 },
                 { 0, 0, 0 }
            };
            patterns[4] = new int[4, 3]
            {
                 { 0, 1, 0 },
                 { 0, 1, 0 },
                 { 0, 1, 0 },
                 { 0, 0, 0 }
            };
            patterns[5] = new int[4, 3]
            {
                 { 0, 0, 0 },
                 { 0, 1, 0 },
                 { 0, 1, 0 },
                 { 0, 1, 0 }
            };
            patterns[6] = new int[4, 3]
            {
                 { 0, 0, 0 },
                 { 0, 0, 0 },
                 { 1, 1, 1 },
                 { 0, 0, 0 }
            };
            patterns[7] = new int[4, 3]
            {
                { 0, 0, 0 },
                { 0, 1, 0 },
                { 0, 1, 0 },
                { 1, 1, 1 }
            };

            int l = patterns.GetLength(0);
            int x_block = w.Width * 6;
            int y_block = w.Height * 8;
            int x_count = size.Width / x_block + 1;
            int y_count = size.Height / y_block + 1;
            Random r = new Random();
            List<Wall> walls = new List<Wall>();

            for (int i = 0; i < x_count; i++)
            {
                for (int j = 0; j < y_count; j++)
                {
                    int[,] pattern = patterns[r.Next(0, l)];
                    for (int p_y = 0; p_y < 4; p_y++)
                    {
                        for (int p_x = 0; p_x < 3; p_x++)
                        {
                            if(pattern[p_y, p_x] == 1)
                            {
                                Wall wall = new Wall();
                                float x = i * x_block + (w.Width * p_x * 2);
                                float y = j * y_block + (w.Height * p_y * 2);
                                walls.AddRange(CreateFour(new PointF(x, y)));
                            }
                        }
                    }
                }
            }

            return walls;
        }

        private static List<Wall> CreateFour(PointF p)
        {
            List<Wall> res = new List<Wall>();
            Wall w = new Wall(p);
            res.Add(w);

            PointF point = new PointF();
            point.X = p.X + w.Width;
            point.Y = p.Y;
            res.Add(new Wall(point));

            point = new PointF();
            point.X = p.X;
            point.Y = p.Y + w.Height;
            res.Add(new Wall(point));

            point = new PointF();
            point.X = p.X + w.Width;
            point.Y = p.Y + w.Height;
            res.Add(new Wall(point));

            return res;
        }
    }
}
