﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Apple : GameObject
    {
        public Apple(PointF position = default) : base(@"Assets/apple.png", 0, position, Direction.None)
        {
        }
    }
}
