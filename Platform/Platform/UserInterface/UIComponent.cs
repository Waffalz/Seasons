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
            foreach (UIComponent comp in contents) {
                comp.Update(gameTime);
            }

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (visible) {
                spriteBatch.Draw(texture, bounds, sourceRect, color);
                foreach(UIComponent comp in contents){
                    comp.Draw(spriteBatch);
                }
            }

        }

    }
}
