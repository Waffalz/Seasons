using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.mobs;
using Platform.world;
using Platform.logger;
using Platform.gameflow;
using Microsoft.Xna.Framework.Input;
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

            controls.Add("Basic Attack", new ContinuousAction(this, (float).75,
                delegate() { return (Game1.CurrentGame.MouseInput.LeftButton == ButtonState.Pressed);},
                delegate(GameTime gametime){
                    Entity icicle = new Entity();
                    icicle.Size = new Vector2(20,40);
                    Vector2 p = parent.Camera.PositionFromScreen(new Point(Game1.CurrentGame.MouseInput.X, Game1.CurrentGame.MouseInput.Y));
                    icicle.Position = new Vector2(this.Position.X + this.Size.X/2 + Math.Sign(p.X-this.Position.X)*(icicle.Size.X/2),this.Position.Y);
                    foreach(Entity ent in icicle.CheckForCollision(parent)){
                        if (ent is Mob && ent != this){
                            ((Mob)ent).Damage(attack, this);
                            Console.WriteLine("Attacking someone");
                        }
                    }
                    

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
