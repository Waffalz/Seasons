using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Platform.GameFlow;
using Platform.Control;
using Platform.Mobs;
using Platform.World;

namespace Platform.Characters
{
    class AutumnCharacter : Player
    {
        float maxBoostTime;
        float boostTime;
        float boostAccel;

        public AutumnCharacter()
            : base()
        {
            maxBoostTime = 3;
            boostTime = maxBoostTime;
            boostAccel = 400;
            airControl = 1;

            controls.Add("Aerial Boost Start", new OnceAction(this, 0,
                delegate() { return Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.W) || Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.Space); },
                delegate() { return Game1.CurrentGame.OldKeyboardInput.IsKeyDown(Keys.W) || Game1.CurrentGame.OldKeyboardInput.IsKeyDown(Keys.Space); },
                delegate(GameTime gameTime) {
                    if (!onGround) {
                        controls["Aerial Boost"].Active = true;
                        
                    }
                }));
            controls.Add("Aerial Boost", new ContinuousAction(this, 0,
                delegate() { return Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.W) || Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.Space); },
                delegate(GameTime gameTime) {
                    if (boostTime > 0) {
                        float timePassed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                        walkVelocity.Y += boostAccel * timePassed;
                        boostTime -= timePassed;
                    }
                }));

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (onGround) {
                controls["Aerial Boost"].Active = false;
                boostTime = maxBoostTime;
                
            }
        }
    }
}
