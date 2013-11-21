using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Engine
{
    public class Entity
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 acceleration;
        protected Vector2 oldPosition;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public int X { get { return (int)Math.Floor(position.X); } }
        public int Y { get { return (int)Math.Floor(position.Y); } }

        public int Width { get; set; }
        public int Height { get; set; }

        public int XOffset { get; set; }
        public int YOffset { get; set; }

        public Rectangle DrawRectangle
        {
            get
            {
                return new Rectangle(X, Y, Width, Height);
            }
        }

        public Texture2D Texture { get; set; }
        public Color Color { get; set; }

        public Level Level { get; set; }

        public Entity(Texture2D texture)
        {
            Texture = texture;
            Width = texture.Width;
            Height = texture.Height;
            Color = Color.White;
        }

        public virtual void Update(float elapsed)
        {
            if (velocity.X != 0 || velocity.Y != 0)
            {
                Move(elapsed);

                position = Vector2.Clamp(position, Vector2.Zero, new Vector2(Level.Width * Tile.Size - Width, Level.Height * Tile.Size - Height));
            }
        }

        private void Move(float elapsed)
        {
            oldPosition = position;
            
            position += velocity * elapsed;

            Rectangle drawRect = DrawRectangle;

            int x1 = X / Tile.Size;
            int x2 = (drawRect.Right - 1) / Tile.Size;
            int y1 = Y / Tile.Size;
            int y2 = (drawRect.Bottom - 1) / Tile.Size;

            Tile t;

            if (velocity.X < 0)
            {
                for (int y = y1; y <= y2; y++)
                {
                    if (Level.GetTile(x1, y).IsSolid())
                    {
                        //position.X = oldPosition.X;
                        position.X = x1 * Tile.Size + Tile.Size;
                        velocity.X = 0;
                        break;
                    }
                }
            }
            else if (velocity.X > 0)
            {
                for (int y = y1; y <= y2; y++)
                {
                    t = Level.GetTile(x2, y);
                    if (t.IsSolid())
                    {
                        //position.X = oldPosition.X;
                        position.X = x2 * Tile.Size - Width;
                        velocity.X = 0;
                        break;
                    }
                }
            }

            if (velocity.Y < 0)
            {
                for (int x = x1; x <= x2; x++)
                {
                    t = Level.GetTile(x, y1);
                    if (t.IsSolid())
                    {
                        position.Y = y1 * Tile.Size + Tile.Size;
                        //position.Y = oldPosition.Y;
                        velocity.Y = 0;
                        break;
                    }
                }
            }
            else if (velocity.Y > 0)
            {
                for (int x = x1; x <= x2; x++)
                {
                    t = Level.GetTile(x, y2);
                    if (t.IsSolid())
                    {
                        position.Y = y2 * Tile.Size - Height;
                        //position.Y = oldPosition.Y;
                        velocity.Y = 0;
                        break;
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawRect = DrawRectangle;
            
            spriteBatch.Draw(Texture, new Vector2(position.X - XOffset, position.Y - YOffset), Color);
            
            //spriteBatch.Draw(Texture, drawRect, Color);
            
            //spriteBatch.Draw(Util.Texture, drawRect, Color.Blue * 0.5f);

            //int x1 = X / Tile.Size;
            //int x2 = (drawRect.Right - 1) / Tile.Size;
            //int y1 = Y / Tile.Size;
            //int y2 = (drawRect.Bottom - 1) / Tile.Size;

            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine(string.Format("X:{0},{1} Y:{2},{3}", x1, x2, y1, y2));
            //sb.AppendLine(drawRect.ToString());
            //spriteBatch.DrawString(Util.Font, sb, new Vector2(drawRect.Left, drawRect.Bottom), Color.Blue);
        }
    }
}
