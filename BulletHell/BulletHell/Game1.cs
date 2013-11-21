using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using BulletHell.Engine;

namespace BulletHell
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState oldKeyboardState, keyboardState;
        MouseState oldmouseState, mouseState;

        Level level;
        Player player;
        Camera camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Util.Initialize(this);

            player = new Player(Content.Load<Texture2D>("Octocat"));
            
            level = new Level(50, 50);
            level.AddEntity(player);

            player.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - player.Width / 2,
                GraphicsDevice.Viewport.Height / 2 - player.Height / 2);

            camera = new Camera(this);
            camera.Focus = player;
            camera.Bounds = new Rectangle(0, 0, level.Width * Tile.Size, level.Height * Tile.Size);

            level.Camera = camera;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            oldmouseState = mouseState;
            mouseState =Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            player.Velocity = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                camera.Shake(2, 2);
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                player.Velocity = new Vector2(-300, player.Velocity.Y);
            }
            else if (keyboardState.IsKeyDown(Keys.E))
            {
                player.Velocity = new Vector2(300, player.Velocity.Y);
            }
            if (keyboardState.IsKeyDown(Keys.OemComma))
            {
                player.Velocity = new Vector2(player.Velocity.X, -300);
            }
            else if (keyboardState.IsKeyDown(Keys.O))
            {
                player.Velocity = new Vector2(player.Velocity.X, 300);
            }
            
            level.Update(elapsed);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, camera.Transform);
            level.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
