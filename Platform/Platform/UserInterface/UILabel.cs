using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Platform.GameFlow;

namespace Platform.UserInterface
{
    class UILabel:UIComponent
    {
        public SpriteFont font;
        public string text;
        public Color textColor;

        public UILabel():base()
        {
            texture = Game1.CurrentGame.Textures["Square"];
            font = Game1.CurrentGame.Fonts["ButtonFont"];
            text = "";
            textColor = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visible)
            {
                spriteBatch.Draw(texture, bounds, sourceRect, color);
                spriteBatch.DrawString(font, text, new Vector2(bounds.X, bounds.Y), textColor);

                foreach (UIComponent comp in contents)
                {
                    comp.Draw(gameTime, spriteBatch);
                }
            }

        }


    }
}
