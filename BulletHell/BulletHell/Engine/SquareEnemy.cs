using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Engine
{
    class SquareEnemy : Enemy
    {
        public SquareEnemy()
            : base(Util.Texture)
        {
            Width = 32;
            Height = 32;

            XOffset = 0;
            YOffset = 0;
        }

        public override void Update(float elapsed)
        {
            //base.Update(elapsed);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawRect = DrawRectangle;

            spriteBatch.Draw(Texture, new Rectangle((int)position.X - XOffset, (int)position.Y - YOffset, Width, Height), Color);

            spriteBatch.Draw(Util.Texture, drawRect, Color.Blue * 0.5f);
        }
    }
}
