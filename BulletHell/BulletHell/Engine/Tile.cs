﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Engine
{
    class Tile
    {
        public static int Size = 32;
        public Color Color { get; set; }
        public Color PreviousColor { get; set; }

        public void SwapColor(Color c)
        {
            PreviousColor = Color;
            Color = c;            
        }

        public bool IsSolid()
        {
            return Color == Color.Red ? true : false;
        }
    }
}