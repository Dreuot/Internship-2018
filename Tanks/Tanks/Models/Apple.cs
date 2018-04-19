using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Apple : GameObject
    {
        public Apple(string sprite = @"Assets/apple.png", PointF position = default) : base(sprite, 0, position, Direction.None)
        {
        }
    }
}
