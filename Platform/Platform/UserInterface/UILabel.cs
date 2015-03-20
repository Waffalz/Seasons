using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Platform.GameFlow;

namespace Platform.UserInterface
{

    public enum HorizontalTextAlignment { Left, Center, Right }
    public enum VerticalTextAlignment { Top, Center, Bottom }

    class UILabel:UIComponent
    {
        public SpriteFont font;
        public string text;
        public Color textColor;
        public HorizontalTextAlignment hAlign;
        public VerticalTextAlignment vAlign;

        public UILabel():base()
        {
            texture = Game1.CurrentGame.Textures["Square"];
            font = Game1.CurrentGame.Fonts["ButtonFont"];
            text = "";
            textColor = Color.White;
            hAlign = HorizontalTextAlignment.Left;
            vAlign = VerticalTextAlignment.Top;
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
                Vector2 textPos = new Vector2();
                
                switch (hAlign) {
                    case HorizontalTextAlignment.Left:
                        textPos.X = this.bounds.X;
                        break;
                    case HorizontalTextAlignment.Center:
                        textPos.X = this.bounds.X + (this.bounds.Width - font.MeasureString(text).X)/2;
                        break;
                    case HorizontalTextAlignment.Right:
                        textPos.X = this.bounds.X + this.bounds.Width - font.MeasureString(text).X;
                        break;
                    default:
                        textPos.X = this.bounds.X;
                        break;
                }

                switch (vAlign) {
                    case VerticalTextAlignment.Top:
                        textPos.Y = this.bounds.Y;
                        break;
                    case VerticalTextAlignment.Center:
                        textPos.Y = this.bounds.Y + (this.bounds.Height - font.MeasureString(text).Y) / 2;
                        break;
                    case VerticalTextAlignment.Bottom:
                        textPos.Y = this.bounds.Y + this.bounds.Width - font.MeasureString(text).Y;
                        break;
                    default:
                        textPos.Y = this.bounds.Y;
                        break;
                }

                spriteBatch.DrawString(font, text, textPos, textColor);

                foreach (UIComponent comp in contents)
                {
                    comp.Draw(gameTime, spriteBatch);
                }
            }

        }


    }


}
