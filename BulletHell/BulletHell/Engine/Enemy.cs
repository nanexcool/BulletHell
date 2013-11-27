using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Engine
{
    class Enemy : Entity
    {
        public Entity Target { get; set; }

        public Enemy(Texture2D texture)
            : base(texture)
        {
            Width = 48;
            Height = 80;

            XOffset = (texture.Width - Width) / 2;
            YOffset = (texture.Height - Height) / 2;
        }

        public override void Update(float elapsed)
        {
            if (Target != null)
            {
                Vector2 target = Target.Position - position;
                target.Normalize();
                velocity = target * 100;

                if (!canMoveHorizontal)
                {
                    velocity.Y = velocity.X;
                }
                if (!canMoveVertical)
                {
                    velocity.X = velocity.Y;
                }
            }

            base.Update(elapsed);
        }
    }
}
