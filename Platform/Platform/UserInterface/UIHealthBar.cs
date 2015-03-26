using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.GameFlow;

namespace Platform.UserInterface
{
    class UIHealthBar: UIStatusBar
    {

        Rectangle end1SrcRect;
        Rectangle end2SrcRect;

        public UIHealthBar()
            : base()
        {
            texture = Game1.CurrentGame.Textures["HealthBar"];
            end1SrcRect = new Rectangle(0, 0, 200, 100);
            end2SrcRect = new Rectangle(300, 0, 200, 100);
            mSrcRect = new Rectangle(200, 0, 100, 100);
            vSrcRect = new Rectangle(0, 100, 600, 100);
            mColor = Color.White;
            vColor = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visible) {
                int endLength = (int)(((float)end1SrcRect.Width / end1SrcRect.Height) * bounds.Height);

                Rectangle middleBounds = new Rectangle(bounds.X + endLength, bounds.Y, bounds.Width - endLength * 2, bounds.Height);

                //draw actual value
                spriteBatch.Draw(texture,
                    new Rectangle(middleBounds.X - (int)((float)endLength / 6 * 1), middleBounds.Y, (int)(val / maxVal * (middleBounds.Width + (int)((float)endLength / 6 * 2))), middleBounds.Height),
                    new Rectangle(vSrcRect.X, vSrcRect.Y, (int)(val / maxVal * vSrcRect.Width), vSrcRect.Height), vColor);

                //draw left end of bar frame
                spriteBatch.Draw(texture, new Rectangle(bounds.X, bounds.Y, endLength, bounds.Height), end1SrcRect, mColor);

                //draw middle of bar frame
                spriteBatch.Draw(texture, middleBounds, mSrcRect, mColor);

                //draw right end of bar frame
                spriteBatch.Draw(texture, new Rectangle(bounds.X + bounds.Width - endLength, bounds.Y, endLength, bounds.Height), end2SrcRect, mColor);

                foreach (UIComponent comp in contents) {
                    comp.Draw(gameTime, spriteBatch);
                }
            }
        }

    }
}
