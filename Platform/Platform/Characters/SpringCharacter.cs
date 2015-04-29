using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Platform.mobs;
using Platform.world;
using Platform.control;
using Platform.gameflow;
using Platform.projectiles;
using Platform.logger;

namespace Platform.characters
{
    class SpringCharacter : Player
    {
        const float BULLET_SPEED = 200f;


        public SpringCharacter()
            : base()
        {
            //set texture 

            //basic attack
            controls.Add("Spring Basic Attack", new ContinuousAction(this, .75f,
                delegate() { return Game1.CurrentGame.MouseInput.LeftButton == ButtonState.Pressed; },
                delegate(GameTime gameTime) {
                    MouseState mouse = Game1.CurrentGame.MouseInput;
                    Vector2 dif = parent.Camera.PositionFromScreen(new Point(mouse.X,mouse.Y))-this.Position;
                    dif.Normalize();
                    SpringBasic seed = new SpringBasic(this, 10);
                    seed.Position = this.Position;
                    seed.Velocity = dif * BULLET_SPEED;
                    parent.AddEntity(seed);

                }));

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            
        }


    }
}
