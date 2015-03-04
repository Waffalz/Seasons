using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.GameFlow;

namespace Platform.UserInterface
{
    public class UIComponent
    {

        public List<UIComponent> contents;

        public Rectangle bounds;
        public bool visible;
        public Texture2D texture;
        public Rectangle sourceRect;
        public Color color;

        public UIComponent()
        {
            contents = new List<UIComponent>();
            bounds = new Rectangle();
            visible = true;
            texture = Game1.CurrentGame.Textures["Square"];
            sourceRect = texture.Bounds;
            color = Color.Gold;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (visible)
            {
                foreach (UIComponent comp in contents)
                {
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
        }


    }
}
