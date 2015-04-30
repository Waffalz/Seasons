using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.UserInterface;

namespace Platform.GameFlow
{
    class CreditsContext : GameContext
    {
        public UIComponent gui;

        SpriteFont font;

        public UIComponent GUI
        {
            get { return gui; }
            set { gui = value; }
        }

        public CreditsContext()
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

            font = Game1.CurrentGame.Fonts["ButtonFont"];
        }

        public override void Update(GameTime gameTime)
        {
            gui.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gui.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "Programmed by:", new Vector2(455, 250), Color.White);
            spriteBatch.DrawString(font, "Ethan Tran", new Vector2(470, 285), Color.White);
            spriteBatch.DrawString(font, "Max Poarch", new Vector2(470, 320), Color.White);
            spriteBatch.DrawString(font, "Caine Mongeau", new Vector2(455, 355), Color.White);
            spriteBatch.DrawString(font, "Michael Milton", new Vector2(455, 390), Color.White);
            spriteBatch.DrawString(font, "Art:", new Vector2(510, 440), Color.White);
            spriteBatch.DrawString(font, "Ethan Tran", new Vector2(470, 475), Color.White);
            spriteBatch.DrawString(font, "Music:", new Vector2(500, 525), Color.White);
            spriteBatch.DrawString(font, "Caine Mongeau", new Vector2(455, 560), Color.White);
        }
    }
}
