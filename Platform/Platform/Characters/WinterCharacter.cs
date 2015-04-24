using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Platform.mobs;
using Platform.world;
using Platform.logger;
using Platform.gameflow;
using Platform.projectiles;
using Platform.control;

namespace Platform.characters
{
    class WinterCharacter : Player
    {
        private float ghostShield = 20f; //percent of damage shielded from


        public WinterCharacter()
            : base()
        {
            manaGen = -5f;
            ghostShield = 0.20f;
            attack = 10;

            //adds player control for basic attacks
            controls.Add("Basic Attack", new ContinuousAction(this, (float)1.2f,
                delegate() { return (Game1.CurrentGame.MouseInput.LeftButton == ButtonState.Pressed);},
                delegate(GameTime gametime){

                    //create a projectile that checks for collision
                    WinterBasic icicle = new WinterBasic(this, .5f);
                    icicle.Size = new Vector2(20,10);

                    //get position of the mouse in the world to calculate the icicle's new position
                    Vector2 p = parent.Camera.PositionFromScreen(new Point(Game1.CurrentGame.MouseInput.X, Game1.CurrentGame.MouseInput.Y));
                    
                    icicle.Position = new Vector2(this.Position.X + Math.Sign(p.X - this.Position.X) * (icicle.Size.X / 2 + this.Size.X / 2), this.Position.Y);
                    
                    foreach(Entity ent in icicle.CheckForCollision(parent)){//check for collision
                        if (ent is Mob && ent != this){ //if collided ent is a mob then
                            ((Mob)ent).Damage(attack, this); // damage mob
                            mana += attack; //mana-steal
                        }
                        for (int i = 0; i < 30; i++) {//particle effects
                            Particle poi = new Particle((float)1.5f, (float)2);
                            poi.Color = Color.SkyBlue;
                            poi.Position = new Vector2(ent.Position.X, ent.Position.Y);
                            double rAngle = MathHelper.ToRadians(Game1.CurrentGame.Rand.Next(0, 360));
                            double speed = Game1.CurrentGame.Rand.Next(10, 40);
                            poi.Velocity = new Vector2((float)Math.Round(Math.Cos(rAngle) * speed), (float)Math.Round(Math.Sin(rAngle) * speed));
                            poi.ColorSpeed = new Vector4(Game1.CurrentGame.Rand.Next(-10, 10), Game1.CurrentGame.Rand.Next(-10, 10), Game1.CurrentGame.Rand.Next(-10, 10), Game1.CurrentGame.Rand.Next(-1000, -700));
                            poi.Color = Color.Red;
                            parent.AddParticle(poi);
                        }

                    }
                    parent.AddEntity(icicle);
                }));

        }

        public override void Damage(float amount)
        {
            health -= amount * (100-ghostShield)/100;
        }
        public override void Damage(Mob attacker, float power)
        {
            health -= ((attacker.Attack - defense) * power / 100) * (100 - ghostShield) / 100;
        }
        public override void Damage(float amount, Mob attacker)
        {
            health -= amount * (100 - ghostShield) / 100;
        }

        public override void Update(GameTime gameTime)
        {
            float timeDifference = (float)gameTime.ElapsedGameTime.TotalSeconds;

            oldPos = Position;

            UpdatePosition(timeDifference);

            UpdateGravity(timeDifference);

            RecordLastSafePosition(timeDifference);

            UpdateGroundVelocity(timeDifference);

            UpdateWalkVelocity(timeDifference);

            UpdateControls(gameTime);

            mana = Math.Max(mana,0);

            //kill Winter if he's out of mana
            if (mana <= 0) {
                health = 0;
            }

            UpdateZoom();

            UpdateMana(timeDifference);

            CorrectCollisionPosition();
        }

        public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
