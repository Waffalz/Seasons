using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Platform.GameFlow;

namespace Platform.UserInterface
{
    class UIBorderedButton:UIButton
    {
        public const int DEFAULT_BORDER = 40;

        int borderSize;


        public UIBorderedButton()
            : base()
        {
            borderSize = DEFAULT_BORDER;
            texture = Game1.CurrentGame.Textures["ScrollBorder"];
        }
        public UIBorderedButton(Rectangle pos, ButtonAction todo)
            : base(pos, todo)
        {
            borderSize = DEFAULT_BORDER;
            texture = Game1.CurrentGame.Textures["ScrollBorder"];
        }
        public UIBorderedButton(Rectangle pos, Color co, ButtonAction todo)
            : base(pos, co, todo)
        {
            borderSize = DEFAULT_BORDER;
            texture = Game1.CurrentGame.Textures["ScrollBorder"];
        }
        public UIBorderedButton(Rectangle pos, Color co, ButtonAction todo, String textu)
            : base(pos, co, todo, textu)
        {
            borderSize = DEFAULT_BORDER;
            texture = Game1.CurrentGame.Textures["ScrollBorder"];
        }
        public UIBorderedButton(Rectangle pos, ButtonAction todo, String textu)
            : base(pos, todo, textu)
        {
            borderSize = DEFAULT_BORDER;
            texture = Game1.CurrentGame.Textures["ScrollBorder"];
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visible) {
                //top left border
                spriteBatch.Draw(texture, new Rectangle(bounds.X,bounds.Y, borderSize, borderSize), new Rectangle(0, 0, 50, 50), currentColor);

                //top right border
                spriteBatch.Draw(texture, new Rectangle(bounds.X + bounds.Width - borderSize, bounds.Y, borderSize, borderSize),
                    new Rectangle(100, 0, 50, 50), currentColor);

                //bot left border
                spriteBatch.Draw(texture, new Rectangle(bounds.X, bounds.Y + bounds.Height - borderSize, borderSize, borderSize),
                    new Rectangle(0, 100, 50, 50), currentColor);

                //bot right border
                spriteBatch.Draw(texture, new Rectangle(bounds.X + bounds.Width - borderSize, bounds.Y + bounds.Height - borderSize, borderSize, borderSize),
                    new Rectangle(100, 100, 50, 50), currentColor);

                //top border
                spriteBatch.Draw(texture, new Rectangle(bounds.X + borderSize, bounds.Y, bounds.Width - 2 * borderSize, borderSize),
                    new Rectangle(50, 0, 50, 50), currentColor);

                //left border
                spriteBatch.Draw(texture, new Rectangle(bounds.X, bounds.Y + borderSize, borderSize, bounds.Height - 2 * borderSize),
                    new Rectangle(0, 50, 50, 50), currentColor);

                //right border
                spriteBatch.Draw(texture, new Rectangle(bounds.X + bounds.Width - borderSize, bounds.Y + borderSize, borderSize, bounds.Height - 2 * borderSize),
                    new Rectangle(100, 50, 50, 50), currentColor);

                //bot border
                spriteBatch.Draw(texture, new Rectangle(bounds.X + borderSize, bounds.Y + bounds.Height - borderSize, bounds.Width - 2 * borderSize, borderSize),
                    new Rectangle(50, 100, 50, 50), currentColor);

                //center
                spriteBatch.Draw(texture, new Rectangle(bounds.X + borderSize, bounds.Y + borderSize,
                    bounds.Width - 2 * borderSize, bounds.Height - 2 * borderSize),
                    new Rectangle(50, 50, 50, 50), currentColor);

                spriteBatch.DrawString(font, text, new Vector2(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2) - font.MeasureString(text) / 2, textColor);

                foreach (UIComponent comp in contents) {
                    comp.Draw(gameTime, spriteBatch);
                }
            }
        }


    }
}
