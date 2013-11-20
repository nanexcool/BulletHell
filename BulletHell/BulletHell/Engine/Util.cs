using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Engine
{
    public static class Util
    {
        public static Texture2D Texture;
        public static SpriteFont Font;

        public static Random random;

        public static void Initialize(Game game)
        {
            Texture = new Texture2D(game.GraphicsDevice, 1, 1);
            Texture.SetData<Color>(new Color[] { Color.White });

            Font = game.Content.Load<SpriteFont>("Font");

            random = new Random();
        }

        public static float NextFloat()
        {
            return (float)random.NextDouble();
        }
    }
}
