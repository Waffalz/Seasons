using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Platform.gameflow;
using Platform.logger;

namespace Platform.userinterface
{
    delegate void ButtonAction();

    class UIButton : UIComponent
    {
        public SpriteFont font;
        
        public Color currentColor;
        public string text;
        public Color textColor;
        public ButtonAction clickAction;


        public UIButton()
            : base()
        {
            font = Game1.CurrentGame.Fonts["ButtonFont"];
            currentColor = color;
            text = "";
            textColor = Color.White;
            clickAction = null;
        }
        public UIButton(Rectangle pos, ButtonAction todo)
            : base()
        {
            font = Game1.CurrentGame.Fonts["ButtonFont"];
            bounds = pos;
            currentColor = color;
            text = "";
            textColor = Color.White;
            clickAction = todo;
        }
        public UIButton(Rectangle pos, Color co, ButtonAction todo)
            : base()
        {
            font = Game1.CurrentGame.Fonts["ButtonFont"];
            bounds = pos;
            color = co;
            currentColor = color;
            text = "";
            textColor = Color.White;
            clickAction = todo;
        }
        public UIButton(Rectangle pos, Color co, ButtonAction todo, String textu)
            : base()
        {
            font = Game1.CurrentGame.Fonts["ButtonFont"];
            bounds = pos;
            color = co;
            currentColor = color;
            text = textu;
            textColor = Color.White;
            clickAction = todo;
        }
        public UIButton(Rectangle pos, ButtonAction todo, String textu)
            : base()
        {
            font = Game1.CurrentGame.Fonts["ButtonFont"];
            bounds = pos;
            currentColor = color;
            text = textu;
            textColor = Color.White;
            clickAction = todo;
        }

        public override void Update(GameTime gameTime)
        {

            if (!visible) {
                return;
            }

            MouseState mos = Game1.CurrentGame.MouseInput;
            MouseState oMos = Game1.CurrentGame.OldMouseInput;
            
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

            } else {
                currentColor = color;
            }
            UpdateComponents(gameTime);
            

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visible) {
                DrawBackground(spriteBatch, currentColor);

                spriteBatch.DrawString(font, text, new Vector2(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2) - font.MeasureString(text) / 2, textColor);

                DrawComponents(gameTime, spriteBatch);
            }

        }

    }
}
