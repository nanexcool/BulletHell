using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Engine
{
    public class Level
    {
        public Camera Camera { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public Tile[] Tiles { get; set; }

        public List<Entity> Entities { get; set; }

        private float enemyTimer = 0;

        public Level(int width, int height)
        {
            Width = width;
            Height = height;

            Tiles = new Tile[width * height];

            // Initialize all tiles
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Tiles[x + y * Width].Color = Color.White * Util.NextFloat();
                    Tiles[x + y * Width].Color = Color.SaddleBrown;
                    if (x % Width == 0 || y % Height == 0 || x == Width -1 || y == Height - 1)
                    {
                        Tiles[x + y * Width].Color = Color.Red;
                    }
                    else if (Util.NextDouble() < 0.05)
                    {
                        Tiles[x + y * Width].Color = Color.Red;
                    }
                }
            }

            Entities = new List<Entity>();
        }

        public void AddEntity(Entity e)
        {
            Entities.Add(e);
            e.Level = this;
        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
            {
                return Tiles[0];
            }
            return Tiles[x + y * Width];
        }

        public virtual void Update(float elapsed)
        {
            enemyTimer += elapsed;

            if (enemyTimer > 1 && Util.NextDouble() < elapsed)
            {
                enemyTimer = 0;
                Enemy e;
                if (Util.NextDouble() > 0.99)
                {
                    e = new Enemy(Util.OctoTexture);
                    e.Color = Color.Brown;
                    e.Position = new Vector2(200, 100);
                }
                else
                {
                    e = new SquareEnemy();
                    int x = 2 * Tile.Size + (e.Width / 2);
                    int y = 2 * Tile.Size + (e.Height / 2);
                    e.Position = new Vector2(x, y);
                }
                
                
                e.Target = Entities[0];
                AddEntity(e);
            }

            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Update(elapsed);
                
                if (Entities[i].CanRemove)
                {
                    Entities.RemoveAt(i);
                    i--;
                }
            }

            Camera.Update(elapsed);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Util.Texture, new Rectangle(0, 0, Width * Tile.Size, Height * Tile.Size), Color.Black);

            int x1 = (int)(Camera.X - Camera.Origin.X) / Tile.Size;
            int y1 = (int)(Camera.Y - Camera.Origin.Y) / Tile.Size;

            int x2 = (int)(Camera.X + Camera.Origin.X) / Tile.Size;
            int y2 = (int)(Camera.Y + Camera.Origin.Y) / Tile.Size;

            x1--;
            y1--;
            x2++;
            y2++;

            if (x1 < 0) x1 = 0;
            if (y1 < 0) y1 = 0;
            if (x2 > Width) x2 = Width;
            if (y2 > Height) y2 = Height;

            for (int y = y1; y < y2; y++)
            {
                for (int x = x1; x < x2; x++)
                {
                    spriteBatch.Draw(Util.Texture, new Rectangle(x * Tile.Size, y * Tile.Size, Tile.Size, Tile.Size), GetTile(x, y).Color);
                }
            }

            // Draw grid
            for (int y = y1; y < y2; y++)
            {
                for (int x = x1; x < x2; x++)
                {
                    spriteBatch.Draw(Util.Texture, new Rectangle(x * Tile.Size, y * Tile.Size, 1, 480), Color.Black);
                    spriteBatch.Draw(Util.Texture, new Rectangle(x * Tile.Size, y * Tile.Size, 800, 1), Color.Black);
                }
            }

            foreach (Entity e in Entities)
            {
                e.Draw(spriteBatch);
            }
        }
    }
}
