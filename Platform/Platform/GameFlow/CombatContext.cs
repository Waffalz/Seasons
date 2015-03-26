using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Platform.World;
using Platform.Graphics;
using Platform.UserInterface;

namespace Platform.GameFlow
{
    public class CombatContext : GameContext
    {
        public const int MAX_SCROLL = 10, MIN_SCROLL = 1;

        Map world;
        bool paused;

        UIComponent gameHUD;
        UIHealthBar healthBar;
        UIHealthBar manaBar;

        UIComponent pauseMenu;

        public Map World
        {
            get { return world; }
            set { world = value; }
        }
        public bool Paused
        {
            get { return paused; }
            set { paused = value; }
        }

        public CombatContext (){
            //TODO: initialize world

            Rectangle window = Game1.CurrentGame.Window.ClientBounds;

            //create Hud for in game stuff

            gameHUD = new UIComponent();
            //UIComponent topLeft = new UIComponent();
            //topLeft.bounds = new Rectangle(0, 0, 450, 150);
            //topLeft.color = Color.LightGray;
            //topLeft.texture = Game1.CurrentGame.Textures["CombatHUD"];
            //topLeft.sourceRect = topLeft.texture.Bounds;
            //gameHUD.Add(topLeft);
            healthBar = new UIHealthBar();
            healthBar.bounds = new Rectangle(50, 10, 500, 60);
            healthBar.mColor = Color.White;
            healthBar.vColor = Color.Green;
            healthBar.depth = 1;
            gameHUD.Add(healthBar);
            manaBar = new UIHealthBar();
            manaBar.bounds = new Rectangle(80, 70, 350, 40);
            manaBar.vColor = Color.SkyBlue; ;
            manaBar.mColor = Color.White;
            manaBar.depth = 1;
            gameHUD.Add(manaBar);

            //create UI for game when it's paused
            pauseMenu = new UIComponent();
            pauseMenu.bounds = new Rectangle(0, 0, window.Width, window.Height);
            pauseMenu.color = new Color(0, 0, 0, 100);

            //Pause menu button for going back to the main menu
            UIButton menuButton = new UIButton(new Rectangle(10, 10, 200, 100), delegate() { Game1.CurrentGame.GameMode = new MainMenuContext(); });
            menuButton.text = "Main menu";
            pauseMenu.Add(menuButton);
            
            //pause menu button for quitting the game
            UIButton quitButton = new UIButton(new Rectangle(10, window.Height - 110, 200, 100), delegate() { Game1.CurrentGame.Exit(); });
            quitButton.text = "Quit game";
            pauseMenu.Add(quitButton);

            gameHUD.visible = true;
            pauseMenu.visible = false;

            world = Map.LoadMap2(@"Content/maps/Level02.txt");
            world.Camera.PointOnScreen = new Point(Game1.CurrentGame.Window.ClientBounds.Width / 2, Game1.CurrentGame.Window.ClientBounds.Height / 2);
            
            for (int i = 0; i < 300; i++) {

                BackgroundObject boi = new BackgroundObject();
                int ro = Game1.CurrentGame.Rand.Next(0, 25);
                boi.Depth = (float)Game1.CurrentGame.Rand.Next(1, 100) / 100;
                boi.Position = new Vector2(Game1.CurrentGame.Rand.Next(-100, 500), Game1.CurrentGame.Rand.Next(-100, 500))*(2-boi.Depth);
                boi.Size = new Vector2(Game1.CurrentGame.Rand.Next(5, 10))*(boi.Depth/2 + (float).5);
                boi.Col = Color.White;
                boi.Image = Game1.CurrentGame.Textures["Blocks"];
                boi.SrcRect = new Rectangle(
                                (ro % Tile.VARS) * Tile.TILE_TEX_WIDTH,
                                (ro / Tile.VARS) * Tile.TILE_TEX_WIDTH,
                                Tile.TILE_TEX_WIDTH, Tile.TILE_TEX_WIDTH);
                world.BackList.Add(boi);

            }
            world.BackList.Sort();


        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState kipz = Game1.CurrentGame.KeyboardInput;
            KeyboardState oKipz = Game1.CurrentGame.OldKeyboardInput;
            MouseState mus = Game1.CurrentGame.MouseInput;
            MouseState oMus = Game1.CurrentGame.OldMouseInput;
            int scroll = mus.ScrollWheelValue - oMus.ScrollWheelValue;

            if (!paused)
            {
                world.Tick(gameTime); //update stuff in the Map
                gameHUD.visible = true;
                pauseMenu.visible = false;
                gameHUD.Update(gameTime);
                
            }
            else
            {
                pauseMenu.visible = true;
                pauseMenu.Update(gameTime);
            }

            if (kipz.IsKeyDown(Keys.Escape) && !oKipz.IsKeyDown(Keys.Escape)){
                paused = !paused;
            }

            if (scroll < 0){
                world.Camera.ZoomScale = Math.Max(MIN_SCROLL, world.Camera.ZoomScale + scroll / 120);
            }
            if (scroll > 0){
                world.Camera.ZoomScale = Math.Min(MAX_SCROLL, world.Camera.ZoomScale + scroll / 120);
            }

            healthBar.MaxValue = Game1.CurrentGame.Player.MaxHealth;
            healthBar.Value = Game1.CurrentGame.Player.Health;

            manaBar.MaxValue = Game1.CurrentGame.Player.MaxMana;
            manaBar.Value = Game1.CurrentGame.Player.Mana;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            world.Camera.Draw(gameTime, spriteBatch);
            gameHUD.Draw(gameTime, spriteBatch);
            pauseMenu.Draw(gameTime, spriteBatch);


        }

    }
}
