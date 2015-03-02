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

        private KeyboardState oKipz;
        private MouseState oMus;

        int maxScroll = 10, minScroll = 1;

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
            KeyboardState kipz = Keyboard.GetState();
            MouseState mus = Mouse.GetState();
            float timePassed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            int scroll = mus.ScrollWheelValue - oMus.ScrollWheelValue;

            
            if (mappu.Player != null){//if a player exists
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
                    for (int i = 0; i < 30; i++ )//particle effects
                    {
                        Particle poi = new Particle((float)2,(float)2);
                        poi.Color = Color.SkyBlue;
                        poi.Position = new Vector2(p.Position.X, p.Position.Y-p.Size.Y/2);
                        double rAngle = MathHelper.ToRadians(rand.Next(0, 360));
                        double speed = rand.Next(20, 40);
                        poi.Velocity = new Vector2((float)Math.Round(Math.Cos(rAngle) * speed), Math.Abs((float)Math.Round(Math.Sin(rAngle) * speed)));
                        poi.ColorSpeed = new Vector3(rand.Next(-10,10), rand.Next(-10,10), rand.Next(-10,10));
                        mappu.AddParticle(poi);

                    }
                    p.OnGround = false;
                    p.Velocity = new Vector2(p.Velocity.X, p.JumpSpeed);
                }

                if (p.WalkVelocity.X != 0 && p.OnGround == true){ //fancy particles effects when running
                    for (int i = 0; i < 5; i++) {
                        Particle poi = new Particle((float)2, (float)1);
                        poi.Color = Color.SkyBlue;
                        poi.Position = new Vector2(p.Position.X, p.Position.Y - p.Size.Y / 2);
                        double rAngle = MathHelper.ToRadians(rand.Next(0, 360));
                        float speed = rand.Next(20, 40);
                        poi.Velocity = new Vector2((float)Math.Cos(rAngle), Math.Abs((float)(Math.Sin(rAngle)))) * speed;
                        poi.ColorSpeed = new Vector3(rand.Next(-10, 10), rand.Next(-10, 10), rand.Next(-10, 10));
                        mappu.AddParticle(poi);

                    }
                }
                if (mus.LeftButton == ButtonState.Pressed) {//left click firing
                    if (p.AttackTime > p.MaxAttack) {
                        p.Attack(mappu.Cam.PositionFromScreen(new Point(mus.X, mus.Y)));
                        
                        p.AttackTime = 0;

                    }
                }
                mappu.Cam.ZoomScale += (mus.ScrollWheelValue - oMus.ScrollWheelValue)/120;
            }

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
