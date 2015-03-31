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

        public Map CombatWorld
        {
            get { return world; }
            set { world = value; }
        }
        bool paused;

        UIComponent gameHUD;

        public UIComponent GameHUD
        {
            get { return gameHUD; }
        }

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
            manaBar.bounds = new Rectangle(50, 70, 350, 40);
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
