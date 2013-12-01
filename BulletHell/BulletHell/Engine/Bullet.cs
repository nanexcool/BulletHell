using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Engine
{
    public class Bullet
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int X { get { return (int)Math.Floor(Position.X); } }
        public int Y { get { return (int)Math.Floor(Position.Y); } }

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(X, Y, Width, Height);
            }
        }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public Color Color { get; set; }

        public float Speed { get; set; }

        public bool IsActive { get; set; }

        public Bullet()
        {
            Width = 16;
            Height = 16;

            Speed = 500;

            Color = Color.Aqua;

            IsActive = true;
        }

        public void OnCollide(Entity e)
        {
            if (e is Enemy)
            {
                e.CanRemove = true;
                IsActive = false;
            }
        }

        public void Update(float elapsed)
        {
            Position += Velocity * Speed * elapsed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Util.Texture, CollisionBox, new Rectangle(0, 0, Width, Height), Color);
        }
    }
}
