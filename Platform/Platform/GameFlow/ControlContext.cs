using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.UserInterface;

namespace Platform.GameFlow
{
    class ControlContext : GameContext
    {
        public UIComponent gui;

        public UIComponent GUI
        {
            get { return gui; }
            set { gui = value; }
        }

        public ControlContext()
        {
            gui = new UIComponent();
            gui.bounds = new Rectangle(0, 0, Game1.CurrentGame.Window.ClientBounds.Width, Game1.CurrentGame.Window.ClientBounds.Height);
            gui.texture = Game1.CurrentGame.Textures["MenuBack"];
            gui.sourceRect = gui.texture.Bounds;
            gui.color = Color.White;

            UIButton backButton = new UIButton(new Rectangle(0, 0, 100, 50), delegate()
            {
                Game1.CurrentGame.GameMode = new MainMenuContext();
            }, "Back");
            gui.Add(backButton);
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
