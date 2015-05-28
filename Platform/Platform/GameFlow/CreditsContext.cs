using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.userinterface;

namespace Platform.gameflow
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

        public CreditsContext(GameContext lastContext)
        {
            gui = new UIComponent();
            gui.bounds = new Rectangle(0, 0, Game1.CurrentGame.Window.ClientBounds.Width, Game1.CurrentGame.Window.ClientBounds.Height);
            gui.texture = Game1.CurrentGame.Textures["MenuBack"];
            gui.sourceRect = gui.texture.Bounds;
            gui.color = Color.White;

            UIButton backButton = new UIButton(new Rectangle(0, 0, 100, 50), delegate()
            {
                Game1.CurrentGame.GameMode = lastContext;
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
            spriteBatch.DrawString(font, "Programmed by:", new Vector2(455, 250), Color.Black);
            spriteBatch.DrawString(font, "Ethan Tran", new Vector2(470, 285), Color.Black);
            spriteBatch.DrawString(font, "Max Poarch", new Vector2(470, 320), Color.Black);
            spriteBatch.DrawString(font, "Caine Mongeau", new Vector2(455, 355), Color.Black);
            spriteBatch.DrawString(font, "Michael Milton", new Vector2(455, 390), Color.Black);
            spriteBatch.DrawString(font, "Art:", new Vector2(510, 440), Color.Black);
            spriteBatch.DrawString(font, "Ethan Tran", new Vector2(470, 475), Color.Black);
            spriteBatch.DrawString(font, "Music:", new Vector2(500, 525), Color.Black);
            spriteBatch.DrawString(font, "Caine Mongeau", new Vector2(455, 560), Color.Black);
        }
    }
}
