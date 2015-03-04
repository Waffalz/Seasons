using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.UserInterface;
using Platform.Mobs;

namespace Platform.GameFlow
{
    public class MenuScreen: GameScreen
    {
        public UIComponent gui;

        public UIComponent GUI
        {
            get { return gui; }
            set { gui = value; }
        }

        public MenuScreen()
        {
            gui = new UIComponent();
            UIButton startButton = new UIButton(new Rectangle(500, 500, 100, 100), delegate() {
                Game1.CurrentGame.Player = new Player();
                Game1.CurrentGame.GameMode = new CombatGame();
            });
            startButton.text = "Start";
            gui.Add(startButton);
            gui.texture = Game1.CurrentGame.Textures["MenuBack"];
            gui.sourceRect = gui.texture.Bounds;
            gui.bounds = new Rectangle(0, 0, Game1.CurrentGame.Window.ClientBounds.Width, Game1.CurrentGame.Window.ClientBounds.Height);
            gui.color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            gui.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gui.Draw(gameTime, spriteBatch);
        }

    }
}
