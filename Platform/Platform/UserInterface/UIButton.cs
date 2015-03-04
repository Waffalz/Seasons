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
    delegate void ButtonAction();

    class UIButton : UIComponent
    {
        public SpriteFont font;
        
        public Color currentColor;
        public string text;
        public Color textColor;
        public ButtonAction clickAction;

        public UIButton(): base(){
            texture = Game1.CurrentGame.Textures["Square"];
            font = Game1.CurrentGame.Fonts["ButtonFont"];
            bounds = new Rectangle();
            sourceRect = texture.Bounds;
            currentColor = color;
            text = "";
            textColor = Color.White;
            clickAction = null;
            visible = true;
        }
        public UIButton(Rectangle pos, ButtonAction todo)
        {
            texture = Game1.CurrentGame.Textures["Square"];
            font = Game1.CurrentGame.Fonts["ButtonFont"];
            bounds = pos;
            sourceRect = texture.Bounds;
            currentColor = color;
            text = "";
            textColor = Color.White;
            clickAction = todo;
            visible = true;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mos = Game1.CurrentGame.MouseInput;
            MouseState oMos = Game1.CurrentGame.OldMouseInput;
            if (visible) {
                if (bounds.Contains(new Point(mos.X, mos.Y))) {
                    currentColor = new Color(
                            color.R - 30,
                            color.G - 30,
                            color.B - 30);
                    if (mos.LeftButton != ButtonState.Pressed && oMos.LeftButton == ButtonState.Pressed) {
                        if (clickAction != null) {
                            clickAction();
                        }
                    }

                }
                else {
                    currentColor = color;
                }
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visible) {
                spriteBatch.Draw(texture, bounds, sourceRect, currentColor);
                spriteBatch.DrawString(font, text, new Vector2(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2) - font.MeasureString(text) / 2, textColor);
                foreach (UIComponent comp in contents)
                {
                    comp.Draw(gameTime, spriteBatch);
                }
            }

        }

    }
}
