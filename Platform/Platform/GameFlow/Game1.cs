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

namespace Platform.GameFlow
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private static GameScreen gameMode;//the current context of the game, be it a menu screen, the actual game, etc.

        public static Dictionary<string, Texture2D> textures;

        public static Random Rand;

        private static KeyboardState kipz;
        private static KeyboardState oKipz;
        private static MouseState mus;
        private static MouseState oMus;

        public static Dictionary<string, Texture2D> Textures
        {
            get { return textures; }
        }
        public static KeyboardState KeyboardInput
        {
            get { return kipz; }
        }
        public static KeyboardState OldKeyboardInput
        {
            get { return oKipz; }
        }
        public static MouseState MouseInput{
            get { return mus; }
        }
        public static MouseState OldMouseInput
        {
            get { return oMus; }
        }
        public static GameScreen GameMode
        {
            get { return gameMode; }
            set { gameMode = value; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            Rand = new Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            //Pre-content load
            kipz = Keyboard.GetState();
            oKipz = kipz;
            mus = Mouse.GetState();
            oMus = mus;

            textures = new Dictionary<string, Texture2D>();
            
            //call to LoadContent
            base.Initialize();

            //TODO: Post content loading here

            //Debugging hardcode here
            gameMode = new CombatGame();
            if (gameMode is CombatGame){
                CombatGame cGame = (CombatGame)gameMode;
                cGame.World = Map.LoadMap2(@"Content/maps/Level02.txt");
                cGame.World.Camera.PointOnScreen = new Point(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
                for (int i = 0; i < 40; i++) {
                    BackgroundObject boi = new BackgroundObject();
                    boi.Position = new Vector2(Game1.Rand.Next(-50, 250), Game1.Rand.Next(-50, 250));
                    boi.Size = new Vector2(Game1.Rand.Next(10, 50));

                    boi.Col = new Color((byte)Game1.Rand.Next(1, 255), (byte)Game1.Rand.Next(1, 255), (byte)Game1.Rand.Next(1, 255));

                    boi.Depth = (float)Game1.Rand.Next(1, 100) / 100;
                    boi.Image = Game1.Textures["DefaultParticle"];
                    cGame.World.BackList.Add(boi);

                }
                cGame.World.BackList.Sort();
            }


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

            textures.Add("Blocks", Content.Load<Texture2D>("tiles/Blocks"));
            textures.Add("Platforms", Content.Load<Texture2D>("tiles/Platforms"));
            textures.Add("Player", Content.Load<Texture2D>("entities/player"));
            textures.Add("DefaultParticle", Content.Load<Texture2D>("particles/Square"));
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            kipz = Keyboard.GetState();
            mus = Microsoft.Xna.Framework.Input.Mouse.GetState();


            gameMode.Update(gameTime);

            oKipz = kipz;
            oMus = mus;

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

            gameMode.Draw(gameTime,spriteBatch);//draw for game screen

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
