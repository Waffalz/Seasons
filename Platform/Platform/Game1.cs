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
    /// This keeps track of what screen we are showing in the game: the main menu, the character select screen, and the game
    /// </summary>
    enum GameState
    {
        MainMenu,
        CharacterSelect,
        Playing
    }
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

        private KeyboardState oKipz;
        private MouseState oMus;

        int maxScroll = 10, minScroll = 1;

        GameState CurrentGameState = GameState.MainMenu;
        // Screen Adjustments
        int screenWidth = 800, screenHeight = 600;
        Texture2D menuTexture;

        Button start;
        Button pause;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            // TODO: Add your initialization logic here

            base.Initialize();
            rand = new Random();

            mappu = Map.LoadMap2(@"Content/maps/Level02.txt");
            mappu.Cam.PointOnScreen = new Point(Window.ClientBounds.Width/2, Window.ClientBounds.Height/2);

            oKipz = Keyboard.GetState();
            oMus = Mouse.GetState();
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
            tileSheets.Add("Blocks", Content.Load<Texture2D>("tiles/Blocks"));
            tileSheets.Add("Platforms", Content.Load<Texture2D>("tiles/Platforms"));

            entSheets = new Dictionary<string, Texture2D>();
            entSheets.Add("Player", Content.Load<Texture2D>("entities/player"));

            particleSheets = new Dictionary<string, Texture2D>();
            particleSheets.Add("DefaultParticle", Content.Load<Texture2D>("particles/Square"));

            // Screen stuff
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            start = new Button(Content.Load<Texture2D>("menuItems/Button"), graphics.GraphicsDevice);
            start.setPosition(new Vector2(325, 450));

            menuTexture = Content.Load<Texture2D>("menuItems/Seasons_Menu");
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
            MouseState mus = Mouse.GetState();
            KeyboardState kipz = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            start.Update(mus);
            switch (CurrentGameState)
            {
                //Main menu
                case GameState.MainMenu:
                    //Starting the game by clicking the start button
                    if (start.isClicked == true)
                    {
                        CurrentGameState = GameState.Playing;
                        start.color = new Color(255, 255, 255, 255);
                    }
                    break;

                //character select menu
                case GameState.CharacterSelect:
                    //Return to main menu by pressing escape
                    if (kipz.IsKeyDown(Keys.Escape))
                        CurrentGameState = GameState.MainMenu;
                    break;
                
                //Logic for playing the game
                case GameState.Playing:
                    float timePassed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    int scroll = mus.ScrollWheelValue - oMus.ScrollWheelValue;


                    if (mappu.Player != null)
                    {//if a player exists
                        Player p = mappu.Player;
                        if (kipz.IsKeyDown(Keys.D)) //running right
                        {
                            p.WalkVelocity += new Vector2(p.WalkSpeed, 0);
                        }
                        if (kipz.IsKeyDown(Keys.A)) //running left
                        {
                            p.WalkVelocity += new Vector2(-p.WalkSpeed, 0);

                        }

                        if ((kipz.IsKeyDown(Keys.Space) || kipz.IsKeyDown(Keys.W)) && p.OnGround)//jumping
                        {
                            p.OnGround = false;
                            p.Velocity = new Vector2(p.Velocity.X, p.JumpSpeed);
                        }

                        if (mus.LeftButton == ButtonState.Pressed)
                        {//left click firing
                            if (p.AttackTime > p.MaxAttack)
                            {
                                p.Attack(mappu.Cam.PositionFromScreen(new Point(mus.X, mus.Y)));

                                p.AttackTime = 0;
                            }
                        }
                        mappu.Cam.ZoomScale += (mus.ScrollWheelValue - oMus.ScrollWheelValue) / 120;
                    }

                    mappu.Tick(gameTime); //update stuff in the Map

                    if (scroll < 0)
                    {
                        mappu.Cam.ZoomScale = Math.Max(minScroll, mappu.Cam.ZoomScale + scroll / 120);
                    }
                    if (scroll > 0)
                    {
                        mappu.Cam.ZoomScale = Math.Min(maxScroll, mappu.Cam.ZoomScale + scroll / 120);
                    }

                    //Return to main menu by pressing escape
                    if (kipz.IsKeyDown(Keys.Escape))
                        CurrentGameState = GameState.MainMenu;
                    break;
            }
            oKipz = kipz;
            oMus = mus;

            //You can quit the game from anywhere by pressing Ctrl+Q
            if ((kipz.IsKeyDown(Keys.LeftControl) || kipz.IsKeyDown(Keys.RightControl)) && kipz.IsKeyDown(Keys.Q))
                this.Exit();

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

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(menuTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    start.Draw(spriteBatch);
                    break;
                case GameState.CharacterSelect:

                    break;
                case GameState.Playing:
                    //Draw entities
                    mappu.Cam.Draw(gameTime, spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
