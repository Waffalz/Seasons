using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Platform.GameFlow;
using Platform.Graphics;
using Platform.World;
using Platform.Control;

namespace Platform.Mobs
{
    class Player : Mob
    {
        float spread;
        float shotspeed;

        private Dictionary<string,GameAction> controls;

        public float ShotSpeed
        {
            get { return shotspeed; }
            set { shotspeed = value; }
        }
        public float Spread
        {
            get { return spread; }
            set { spread = value; }
        }

        public Player():base()
        {
            Size = new Vector2(10,10);
            texture = Game1.Textures["Player"];
            SourceRect = texture.Bounds;
            WalkSpeed = 50;
            JumpSpeed = 100;
            WalkVelocity = new Vector2();
            OnGround = false;

            spread = 10;
            shotspeed = 200;

            controls = new Dictionary<string, GameAction>();

            controls.Add("Move Left", new ContinuousAction(this, 0,
                delegate() { return Game1.KeyboardInput.IsKeyDown(Keys.D); },
                delegate() { walkVelocity += new Vector2(walkSpeed, 0); }));//add run left control
            controls.Add("Move Right", new ContinuousAction(this, 0,
                delegate() { return Game1.KeyboardInput.IsKeyDown(Keys.A); },
                delegate() { walkVelocity += new Vector2(-walkSpeed, 0); }));//add run right control
            controls.Add("Basic Attack", new ContinuousAction(this, (float).5,
                delegate() { return Game1.MouseInput.LeftButton == ButtonState.Pressed; },
                delegate() { BasicAttack(spread); }));
            controls.Add("Jump W", new ContinuousAction(this, 0,
                delegate() { return Game1.KeyboardInput.IsKeyDown(Keys.W);},
                delegate() {
                    if (onGround) {
                        onGround = false;
                        velocity = new Vector2(velocity.X, jumpSpeed);
                    }
                }));
            controls.Add("Jump Space", new ContinuousAction(this, 0,
                delegate() { return Game1.KeyboardInput.IsKeyDown(Keys.Space); },
                delegate() {
                    if (onGround) {
                        onGround = false;
                        velocity = new Vector2(velocity.X, jumpSpeed);
                    }
                }));
            controls.Add("Shatgann", new OnceAction(this, 1,
                delegate() { return (Game1.MouseInput.RightButton == ButtonState.Pressed); },
                delegate() { return (Game1.OldMouseInput.RightButton == ButtonState.Pressed); },
                delegate() {
                    for (int p = 0; p < 10; p++) {
                        BasicAttack(spread*3);
                    }
                }));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float gTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            foreach (KeyValuePair<string, GameAction> a in controls) {
                a.Value.Update(gameTime);
            }
            /*
            for (int i = 0; i < 30; i++){//particle effects
                
                Particle poi = new Particle((float)2, (float)2);
                poi.Color = Color.SkyBlue;
                poi.Position = new Vector2(position.X, position.Y - size.Y / 2);
                double rAngle = MathHelper.ToRadians(Game1.Rand.Next(0, 360));
                double speed = Game1.Rand.Next(20, 40);
                poi.Velocity = new Vector2((float)Math.Round(Math.Cos(rAngle) * speed), Math.Abs((float)Math.Round(Math.Sin(rAngle) * speed)));
                poi.ColorSpeed = new Vector3(Game1.Rand.Next(-10, 10), Game1.Rand.Next(-10, 10), Game1.Rand.Next(-10, 10));
                parent.AddParticle(poi);

            }
            */
            parent.Camera.ZoomScale += (Game1.MouseInput.ScrollWheelValue - Game1.OldMouseInput.ScrollWheelValue) / 120;
            

        }

        public void BasicAttack(float spread){
        
            Vector2 target = parent.Camera.PositionFromScreen(new Point(Game1.MouseInput.X, Game1.MouseInput.Y));
            Vector2 dif = target - position;
            
            double ang = (float)Math.Atan2((double)dif.Y, (double)dif.X) + MathHelper.ToRadians(Game1.Rand.Next((int)(-spread / 2), (int)(spread / 2)));//calculate spread

            dif.Normalize();

            Ball b = new Ball();
            b.Creator = this;
            b.Position = position;
            b.Velocity = new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang)) * shotspeed;
            b.Size = new Vector2(5);
            b.LifeLeft = 5;
            b.Gravity = true;

            parent.AddEntity(b);

        }
    }

}
