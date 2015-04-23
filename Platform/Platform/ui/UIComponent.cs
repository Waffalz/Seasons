using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.gameflow;
using Platform.logger;

namespace Platform.userinterface
{
    public class UIComponent : IComparable
    {

        protected List<UIComponent> contents;

        protected UIBorder border;
        public UIBorder Border
        {
            get { return border; }
            set
            {
                border = value;
                
                switch (border)
                {
                    case UIBorder.Scroll:
                        texture = Game1.CurrentGame.Textures["ScrollBorder"];
                        break;
                    case UIBorder.None:

                        break;
                    default:
                        texture = Game1.CurrentGame.Textures["Square"];
                        break;
                }
                sourceRect = texture.Bounds;
            }
        }

        public Rectangle bounds;
        public bool visible;
        public Texture2D texture;
        public Rectangle sourceRect;
        public Color color;

        public int borderSize;

        public float depth;

        public UIComponent()
        {
            contents = new List<UIComponent>();
            bounds = new Rectangle();
            visible = true;
            texture = Game1.CurrentGame.Textures["Square"];
            sourceRect = texture.Bounds;
            color = Color.MediumSeaGreen;
            depth = (float)0.5;
            border = UIBorder.None;
            borderSize = 30;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!visible) {
                return;
            }
            UpdateComponents(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!visible) {
                return;
            }
            DrawBackground(spriteBatch, color);
            DrawComponents(gameTime, spriteBatch);
        }

        public void Add(UIComponent child)
        {
            contents.Add(child);
            contents.Sort();
        }

        public int CompareTo(object e)
        {
            if (e == null) {
                return 1;
            }
            UIComponent other = e as UIComponent;
            if (other != this) {
                return this.depth > other.depth ? 1 : this.depth < other.depth ? -1 : 0;
            } else {
                throw new ArgumentException("Object is not BackgroundObject");
            }
        }

        public void UpdateComponents(GameTime gameTime)
        {
            foreach (UIComponent comp in contents) {
                comp.Update(gameTime);
            }
        }

        public void DrawComponents(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (UIComponent comp in contents) {
                comp.Draw(gameTime, spriteBatch);
            }
        }

        public virtual void DrawBackground(SpriteBatch spriteBatch, Color col)
        {
            if (border == UIBorder.None) {
                spriteBatch.Draw(texture, bounds, sourceRect, col);
            } else {
                DrawBorder(spriteBatch, col);
            }
        }

        public void DrawBorder(SpriteBatch spriteBatch, Color col)
        {
            //top left border
            spriteBatch.Draw(texture, new Rectangle(bounds.X, bounds.Y, borderSize, borderSize), new Rectangle(0, 0, 50, 50), col);

            //top right border
            spriteBatch.Draw(texture, new Rectangle(bounds.X + bounds.Width - borderSize, bounds.Y, borderSize, borderSize),
                new Rectangle(100, 0, 50, 50), col);

            //bot left border
            spriteBatch.Draw(texture, new Rectangle(bounds.X, bounds.Y + bounds.Height - borderSize, borderSize, borderSize),
                new Rectangle(0, 100, 50, 50), col);

            //bot right border
            spriteBatch.Draw(texture, new Rectangle(bounds.X + bounds.Width - borderSize, bounds.Y + bounds.Height - borderSize, borderSize, borderSize),
                new Rectangle(100, 100, 50, 50), col);

            //top border
            spriteBatch.Draw(texture, new Rectangle(bounds.X + borderSize, bounds.Y, bounds.Width - 2 * borderSize, borderSize),
                new Rectangle(50, 0, 50, 50), col);

            //left border
            spriteBatch.Draw(texture, new Rectangle(bounds.X, bounds.Y + borderSize, borderSize, bounds.Height - 2 * borderSize),
                new Rectangle(0, 50, 50, 50), col);

            //right border
            spriteBatch.Draw(texture, new Rectangle(bounds.X + bounds.Width - borderSize, bounds.Y + borderSize, borderSize, bounds.Height - 2 * borderSize),
                new Rectangle(100, 50, 50, 50), col);

            //bot border
            spriteBatch.Draw(texture, new Rectangle(bounds.X + borderSize, bounds.Y + bounds.Height - borderSize, bounds.Width - 2 * borderSize, borderSize),
                new Rectangle(50, 100, 50, 50), col);

            //center
            spriteBatch.Draw(texture, new Rectangle(bounds.X + borderSize, bounds.Y + borderSize,
                bounds.Width - 2 * borderSize, bounds.Height - 2 * borderSize),
                new Rectangle(50, 50, 50, 50), col);
        }
    }

    public enum UIBorder { None, Scroll}
}
