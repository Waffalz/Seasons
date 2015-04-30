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
using Platform.graphics;
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
            texture = Game1.CurrentGame.Textures["SpringAnim"];

            //basic attack
            controls.Add("Spring Basic Attack", new ContinuousAction(this, .75f,
                delegate() { return Game1.CurrentGame.MouseInput.LeftButton == ButtonState.Pressed; },
                delegate(GameTime gameTime) {

                    //create bullet seed
                    MouseState mouse = Game1.CurrentGame.MouseInput;
                    Vector2 dif = parent.Camera.PositionFromScreen(new Point(mouse.X,mouse.Y))-this.Position;
                    dif.Normalize();
                    SpringBasic seed = new SpringBasic(this, 10);
                    seed.Position = this.Position;
                    seed.Velocity = dif * BULLET_SPEED;
                    parent.AddEntity(seed);

                    //set animation stuff
                    AnimState = new AnimationState(1,1);
                    AnimState.Draw = delegate(GameTime gTime, SpriteBatch spriteBatch) {
                        //animation for drawing
                        if (texture != null) {
                            spriteBatch.Draw(texture, new Microsoft.Xna.Framework.Rectangle(
                                (int)((Position.X - Size.X / 2 - parent.Camera.Position.X) * parent.Camera.ZoomScale + parent.Camera.PointOnScreen.X),
                                (int)(-(Position.Y + Size.Y / 2 - parent.Camera.Position.Y) * parent.Camera.ZoomScale + parent.Camera.PointOnScreen.Y),
                                (int)(Size.X * parent.Camera.ZoomScale), (int)(Size.Y * parent.Camera.ZoomScale)),
                                sourceRect, color * ((float)(color.A) / 255));
                        }

                    };
                    AnimState.LastFrame = delegate(int frame) {
                        AnimState = null;
                    };
                }));

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            
        }


    }
}
