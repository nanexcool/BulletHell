using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Engine
{
    class Level
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Tile[] Tiles { get; set; }

        public List<Entity> Entities { get; set; }

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
                    Tiles[x + y * Width] = new Tile();
                    //Tiles[x + y * Width].Color = Color.Black * Util.NextFloat();
                    if (x % Width == 0 || y % Height == 0 || x == Width -1 || y == Height - 1)
                    {
                        GetTile(x, y).Color = Color.Red;
                    }
                    else
                    {
                        GetTile(x, y).Color = Color.LawnGreen;
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
            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Update(elapsed);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Util.Texture, new Rectangle(0, 0, Width * Tile.Size, Height * Tile.Size), Color.White);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    spriteBatch.Draw(Util.Texture, new Rectangle(x * Tile.Size, y * Tile.Size, Tile.Size, Tile.Size), Tiles[x + y * Width].Color);
                }
            }

            foreach (Entity e in Entities)
            {
                e.Draw(spriteBatch);
            }
        }
    }
}
