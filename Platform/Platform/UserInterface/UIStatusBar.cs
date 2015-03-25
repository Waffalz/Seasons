using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Platform.GameFlow;

namespace Platform.UserInterface
{
    class UIStatusBar: UIComponent
    {

        protected float val;
        protected float maxVal;

        public Color vColor;
        public Color mColor;

        public Texture2D vTex;
        public Texture2D mTex;

        public Rectangle vSrcRect;
        public Rectangle mSrcRect;

        public float Value
        {
            get { return val; }
            set { val = value; }
        }

        public float MaxValue
        {
            get { return maxVal; }
            set { maxVal = value; }
        }

        public UIStatusBar()
            : base ()
        {
            bounds = new Rectangle(0,0,100,10);

            maxVal = 100;
            val = maxVal;

            vColor = Color.Green;
            mColor = Color.Red;

            vTex = Game1.CurrentGame.Textures["Square"];
            mTex = Game1.CurrentGame.Textures["Square"];

            vSrcRect = vTex.Bounds;
            mSrcRect = mTex.Bounds;

        }

        public UIStatusBar(int max)
            : base()
        {
            bounds = new Rectangle(0, 0, 100, 10);

            maxVal = max;
            val = maxVal;

            vColor = Color.Green;
            mColor = Color.Red;

            vTex = Game1.CurrentGame.Textures["Square"];
            mTex = Game1.CurrentGame.Textures["Square"];

            vSrcRect = vTex.Bounds;
            mSrcRect = mTex.Bounds;

        }

        public override void Update(GameTime gameTime)
        {
            if (visible){
                foreach (UIComponent comp in contents){
                    comp.Update(gameTime);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visible) {
                //draw maxBar
                spriteBatch.Draw(mTex, bounds, mSrcRect, mColor);

                //draw valuebar
                spriteBatch.Draw(
                    vTex,
                    new Rectangle(bounds.X, bounds.Y, (int)(bounds.Width * val / maxVal), bounds.Height),
                    new Rectangle(vSrcRect.X, vSrcRect.Y, (int)(vSrcRect.Width * val / maxVal), vSrcRect.Height),
                    vColor);

                foreach(UIComponent comp in contents){
                    comp.Draw(gameTime, spriteBatch);
                }
            }
           
        }


    }

}
