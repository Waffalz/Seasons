using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


using Platform.World;
using Platform.Graphics;

namespace Platform.GameFlow
{
    class CombatGame : GameScreen
    {
        public const int MAX_SCROLL = 10, MIN_SCROLL = 1;

        Map world;

        public Map World
        {
            get { return world; }
            set { world = value; }
        }

        public CombatGame (){
            //TODO: initialize world

            


        }

        public override void Update(GameTime gameTime)
        {
            MouseState mus = Game1.Mouse;
            MouseState oMus = Game1.OldMouse;
            int scroll = mus.ScrollWheelValue - oMus.ScrollWheelValue;

            world.Tick(gameTime); //update stuff in the Map

            if (scroll < 0){
                world.Camera.ZoomScale = Math.Max(MIN_SCROLL, world.Camera.ZoomScale + scroll / 120);
            }
            if (scroll > 0){
                world.Camera.ZoomScale = Math.Min(MAX_SCROLL, world.Camera.ZoomScale + scroll / 120);
            }


        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            world.Camera.Draw(gameTime, spriteBatch);
        }

    }
}
