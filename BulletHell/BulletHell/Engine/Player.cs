using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Engine
{
    class Player : Entity
    {
        public Player(Texture2D texture)
            : base(texture)
        {
            Width = 48;
            Height = 80;

            XOffset = (texture.Width - Width) / 2;
            YOffset = (texture.Height - Height) / 2;
        }
    }
}
