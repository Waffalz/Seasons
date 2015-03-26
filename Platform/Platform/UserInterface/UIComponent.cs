using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.GameFlow;

namespace Platform.UserInterface
{
    public class UIComponent : IComparable
    {

        protected List<UIComponent> contents;

        UIBorder border;

        public Rectangle bounds;
        public bool visible;
        public Texture2D texture;
        public Rectangle sourceRect;
        public Color color;

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
        }

        public virtual void Update(GameTime gameTime)
        {
            if (visible){
                foreach (UIComponent comp in contents){
                    comp.Update(gameTime);
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visible) {
                spriteBatch.Draw(texture, bounds, sourceRect, color);
                foreach(UIComponent comp in contents){
                    comp.Draw(gameTime, spriteBatch);
                }
            }

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
    }

    enum UIBorder { None, Scroll}
}
