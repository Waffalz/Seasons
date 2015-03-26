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
    public class MainMenuContext: GameContext
    {
        public UIComponent gui;

        public UIComponent GUI
        {
            get { return gui; }
            set { gui = value; }
        }

        public MainMenuContext()
        {
            gui = new UIComponent();
            gui.texture = Game1.CurrentGame.Textures["MenuBack"];
            gui.sourceRect = gui.texture.Bounds;
            gui.bounds = new Rectangle(0, 0, Game1.CurrentGame.Window.ClientBounds.Width, Game1.CurrentGame.Window.ClientBounds.Height);
            gui.color = Color.White;

            UIBorderedButton startButton = new UIBorderedButton(new Rectangle(0, 300, 300, 100), delegate()
            {
                Game1.CurrentGame.GameMode = new CharSelectContext();
            }, "New Game");
            startButton.bounds.X = (Game1.CurrentGame.Window.ClientBounds.Width - startButton.bounds.Width) / 2;
            gui.Add(startButton);

            UIButton exitButton = new UIButton(new Rectangle(0, 480, 300, 60), delegate()
            {
                Game1.CurrentGame.Exit();
            }, "Exit");
            exitButton.bounds.X = (Game1.CurrentGame.Window.ClientBounds.Width - startButton.bounds.Width) / 2;
            gui.Add(exitButton);

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
