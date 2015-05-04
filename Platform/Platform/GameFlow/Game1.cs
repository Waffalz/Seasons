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

using Platform.graphics;
using Platform.world;
using Platform.mobs;
using Platform.logger;

namespace Platform.gameflow {
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game {

		private const string className = "Game1";
		private const string classNamespace = "gameflow";

		private Player player;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private static Game1 currentGame;

		private GameContext gameMode;//the current context of the game, be it a menu screen, the actual game, etc.

		private Dictionary<string, Texture2D> textures;
		private Dictionary<string, SpriteFont> fonts;

		private Random rand;

		private KeyboardState kipz;
		private KeyboardState oKipz;
		private MouseState mus;
		private MouseState oMus;

        public List<Song> gameMusic;

		public static Game1 CurrentGame {
			get {
				return currentGame;
			}
		}
		public Random Rand {
			get {
				return rand;
			}
		}
		public Dictionary<string, Texture2D> Textures {
			get {
				return textures;
			}
		}
		public Dictionary<string, SpriteFont> Fonts {
			get {
				return fonts;
			}
		}
		public KeyboardState KeyboardInput {
			get {
				return kipz;
			}
		}
		public KeyboardState OldKeyboardInput {
			get {
				return oKipz;
			}
		}
		public MouseState MouseInput {
			get {
				return mus;
			}
		}
		public MouseState OldMouseInput {
			get {
				return oMus;
			}
		}
		public GameContext GameMode {
			get {
				return gameMode;
			}
			set {
				gameMode = value;
			}
		}
		public Player Player {
			get {
				return player;
			}
			set {
				player = value;
			}
		}

		public Game1() {
			graphics = new GraphicsDeviceManager( this );
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			graphics.PreferredBackBufferWidth = 1080;
			graphics.PreferredBackBufferHeight = 720;
			rand = new Random();
			currentGame = this;
			graphics.PreferMultiSampling = true;

			Logger.Info( className, classNamespace, "Game created" );
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
			// TODO: Add your initialization logic here

			//Pre-content load
			kipz = Keyboard.GetState();
			oKipz = kipz;
			mus = Mouse.GetState();
			oMus = mus;

			textures = new Dictionary<string, Texture2D>();
			fonts = new Dictionary<string, SpriteFont>();
			//call to LoadContent
            gameMusic = new List<Song>();

			base.Initialize();

			//TODO: Post content loading here

			//Debugging hardcode here
			gameMode = new MainMenuContext();

			Logger.Info( className, classNamespace, "Game init successful" );
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch( GraphicsDevice );

			// TODO: use this.Content to load your game content here

			textures.Add( "Blocks", Content.Load<Texture2D>( "Tiles/blocks" ) );
			//textures.Add( "NewBlocks", Content.Load<Texture2D>( "Tiles/newBlocks" ) );

			textures.Add( "Player", Content.Load<Texture2D>( "Entities/player" ) );

			textures.Add( "Square", Content.Load<Texture2D>( "Particles/square" ) );
			textures.Add( "CircleParticle", Content.Load<Texture2D>( "Particles/circleParticle" ) );

			textures.Add( "MenuBack", Content.Load<Texture2D>( "MenuItems/Menuback" ) );
			textures.Add( "HealthBar", Content.Load<Texture2D>( "GUITextures/healthBar" ) );
			//textures.Add("CombatHUD", Content.Load<Texture2D>("GUITextures/HUDBack"));
			
			textures.Add( "ScrollBorder", Content.Load<Texture2D>( "GUITextures/scrollBorder" ) );
            
            //Add player sprites
            textures.Add("SpringAnim", Content.Load<Texture2D>("Players/SpringAnim"));
            textures.Add("SummerAnim", Content.Load<Texture2D>("Players/SummerAnim"));
            textures.Add("AutumnAnim", Content.Load<Texture2D>("Players/AutumnAnim"));
            textures.Add("WinterAnim", Content.Load<Texture2D>("Players/WinterAnim"));

            //projectiles
            textures.Add("Icicle", Content.Load<Texture2D>("Projectiles/Icicle"));
			
			fonts.Add( "ButtonFont", Content.Load<SpriteFont>( "Fonts/buttonFont" ) );

            gameMusic.Add(Content.Load<Song>("Songs/Ah, The 808's"));
            gameMusic.Add(Content.Load<Song>("Songs/Description N-A"));
            gameMusic.Add(Content.Load<Song>("Songs/Ominosity"));
            gameMusic.Add(Content.Load<Song>("Songs/Questionable"));
            gameMusic.Add(Content.Load<Song>("Songs/The Key"));
            gameMusic.Add(Content.Load<Song>("Songs/Welcome to Summer"));

            ShuffleMusic();

            for (int i = 0; i < gameMusic.Count; i++)
            {
                MediaPlayer.Play(gameMusic[i]);
            }
            MediaPlayer.IsRepeating = true;

			Logger.Info( className, classNamespace, "Game content load successful" );
		}

        private void ShuffleMusic()
        {
            for (int i = 0; i < gameMusic.Count; i++)
            {
                int randIndex = rand.Next( gameMusic.Count );
                Song toReplace = gameMusic[ randIndex ];
                gameMusic[ i ] = toReplace;
                gameMusic[randIndex] = gameMusic[i];
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
			// TODO: Unload any non ContentManager content here
			Logger.Info( className, classNamespace, "Game content unload successful" );
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update( GameTime gameTime ) {
			// Allows the game to exit
			if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed )
				this.Exit();

			// TODO: Add your update logic here
			kipz = Keyboard.GetState();
			mus = Microsoft.Xna.Framework.Input.Mouse.GetState();


			gameMode.Update( gameTime );

			oKipz = kipz;
			oMus = mus;

			base.Update( gameTime );
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw( GameTime gameTime ) {
			GraphicsDevice.Clear( Color.Gray );

			// TODO: Add your drawing code here
			spriteBatch.Begin();

			gameMode.Draw( gameTime, spriteBatch );//draw for game screen

			spriteBatch.End();
			base.Draw( gameTime );
		}
	}
}