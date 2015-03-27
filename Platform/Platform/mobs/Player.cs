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
    public class Player : Mob
    {
        protected float mana;
        public float Mana
        {
            get { return mana; }
            set { mana = value; }
        }

        protected float maxMana;
        public float MaxMana
        {
            get { return maxMana; }
            set { maxMana = value; }
        }

        protected float manaGen;
        public float ManaGen
        {
            get { return manaGen; }
            set { manaGen = value; }
        }

        protected Dictionary<string,GameAction> controls;

        public Player():base()
        {
            Size = new Vector2((float)8,(float)16);
            texture = Game1.CurrentGame.Textures["Square"];
            SourceRect = texture.Bounds;
            WalkSpeed = 50;
            JumpSpeed = 100;
            WalkVelocity = new Vector2();
            OnGround = false;

            controls = new Dictionary<string, GameAction>();

            maxMana = 100;
            mana = maxMana;
            manaGen = 10;

            controls.Add("Move Left", new ContinuousAction(this, 0,
                delegate() { return Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.D); },
                delegate(GameTime gameTime) {
                    if (Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.A)) {
                        if (walkVelocity.X > 0 || !Game1.CurrentGame.OldKeyboardInput.IsKeyDown(Keys.D)) {
                            //walkVelocity.X = walkSpeed
                            walkVelocity.X = Math.Min(Math.Abs(walkVelocity.X) + 4 * movementAccel * (float)gameTime.ElapsedGameTime.TotalSeconds, walkSpeed);
                        }
                    } else {
                        //walkVelocity.X = walkSpeed
                        walkVelocity.X = Math.Min(Math.Abs(walkVelocity.X) + 4 * movementAccel * (float)gameTime.ElapsedGameTime.TotalSeconds, walkSpeed);
                    }
                }));//add run left control

            controls.Add("Move Right", new ContinuousAction(this, 0,
                delegate() { return Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.A); },
                delegate(GameTime gameTime){
                    if (Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.D)) {
                        if (walkVelocity.X < 0 || !Game1.CurrentGame.OldKeyboardInput.IsKeyDown(Keys.A)) {
                            //walkVelocity.X = -walkSpeed
                            walkVelocity.X = -Math.Min(Math.Abs(walkVelocity.X) + 4 * movementAccel * (float)gameTime.ElapsedGameTime.TotalSeconds, walkSpeed);
                        }
                    } else {
                        //walkVelocity.X = -walkSpeed
                        walkVelocity.X = -Math.Min(Math.Abs(walkVelocity.X) + 4 * movementAccel * (float)gameTime.ElapsedGameTime.TotalSeconds, walkSpeed);
                    }
                }));//add run right control

            controls.Add("Jump", new OnceAction(this, 0,
                delegate() { return Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.W) || Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.Space); },
                delegate() { return Game1.CurrentGame.OldKeyboardInput.IsKeyDown(Keys.W) || Game1.CurrentGame.OldKeyboardInput.IsKeyDown(Keys.Space); },
                delegate(GameTime gameTime) {
                    if (onGround) {
                        onGround = false;
                        position.Y += .1f;//elevating player position by a negligible amount to get around that stupid no-jumping bug
                        velocity = new Vector2(velocity.X, jumpSpeed);
                    }
                }));
            //controls.Add("Jump", new ContinuousAction(this, 0,
            //    delegate() { return Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.W) || Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.Space); },
            //    delegate(GameTime gameTime) {
            //        if (onGround) {
            //            onGrou nd = false;
            //            velocity = new Vector2(velocity.X, jumpSpeed);
            //        }
            //    }));
            
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
            parent.Camera.ZoomScale += (Game1.CurrentGame.MouseInput.ScrollWheelValue - Game1.CurrentGame.OldMouseInput.ScrollWheelValue) / 120;

            mana = Math.Min(maxMana, mana + manaGen * gTime);
        }

        
    }

}
