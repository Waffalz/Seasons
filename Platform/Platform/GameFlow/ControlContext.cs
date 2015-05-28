using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.userinterface;
using Microsoft.Xna.Framework.Media;

namespace Platform.gameflow
{
    class ControlContext : GameContext
    {
        public UIComponent gui;

        SpriteFont font;

        public UIComponent GUI
        {
            get { return gui; }
            set { gui = value; }
        }

        public ControlContext(GameContext lastContext)
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
            MediaPlayer.Volume = 1.0f;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gui.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "W - Jump", new Vector2(485, 250), Color.Black);
            spriteBatch.DrawString(font, "A/D - Move Left/Right", new Vector2(420, 285), Color.Black);
            spriteBatch.DrawString(font, "Left Click - attack", new Vector2(430, 320), Color.Black);
        }
    }
}
