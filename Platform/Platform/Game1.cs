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

using Platform.Graphics;
using Platform.World;
using Platform.Mobs;

namespace Platform
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Map mappu;

        public static Dictionary<string, Texture2D> tileSheets;
        public static Dictionary<string, Texture2D> entSheets;
        public static Dictionary<string, Texture2D> particleSheets;

        public static Random rand;

        public static KeyboardState kipz;
        public static KeyboardState oKipz;
        public static MouseState mus;
        public static MouseState oMus;

        int maxScroll = 10, minScroll = 1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            rand = new Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            oKipz = Keyboard.GetState();
            oMus = Mouse.GetState();

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

            tileSheets = new Dictionary<string, Texture2D>();
            tileSheets.Add("Blocks", Content.Load<Texture2D>("Tiles/Blocks"));
            tileSheets.Add("Platforms", Content.Load<Texture2D>("Tiles/Platforms"));

            entSheets = new Dictionary<string, Texture2D>();
            entSheets.Add("Player", Content.Load<Texture2D>("Entities/player"));

            particleSheets = new Dictionary<string, Texture2D>();
            particleSheets.Add("DefaultParticle", Content.Load<Texture2D>("Particles/Square"));


            mappu = Map.LoadMap2(@"Content/Maps/Level02.txt");
            mappu.Cam.PointOnScreen = new Point(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            mappu.BackList.Sort();

            for (int i = 0; i < 40; i++)
            {
                BackgroundObject boi = new BackgroundObject();
                boi.Position = new Vector2(rand.Next(1, 200), rand.Next(1, 100));
                boi.Size = new Vector2(rand.Next(1, 10));

                boi.Col = new Color((byte)rand.Next(1, 255), (byte)rand.Next(1, 255), (byte)rand.Next(1, 255));

                boi.Depth = (float)rand.Next(1, 100) / 100;
                boi.Image = particleSheets["DefaultParticle"];
                mappu.BackList.Add(boi);

            }


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
            // Allows the game to exit
			KeyboardState kbState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kbState.IsKeyDown( Keys.Escape ) )
                this.Exit();

            // TODO: Add your update logic here
            kipz = Keyboard.GetState();
            mus = Mouse.GetState();
            float timePassed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            int scroll = mus.ScrollWheelValue - oMus.ScrollWheelValue;

            
            

            mappu.Tick(gameTime); //update stuff in the Map

            

            oKipz = kipz;
            oMus = mus;

            if (scroll < 0){
                mappu.Cam.ZoomScale = Math.Max(minScroll, mappu.Cam.ZoomScale + scroll/120);
            }
            if (scroll > 0) {
                mappu.Cam.ZoomScale = Math.Min(maxScroll, mappu.Cam.ZoomScale + scroll/120);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            //Draw entities
            mappu.Cam.Draw(gameTime, spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
