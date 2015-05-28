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
    class SummerCharacter : Player
    {

        public SummerCharacter()
            : base()
        {
            texture = Game1.CurrentGame.Textures["SummerAnim"];

            attack = 7;

            //adds player control for basic attacks
            controls.Add("Basic Attack", new ContinuousAction(this, (float).75,
                delegate() { return (Game1.CurrentGame.MouseInput.LeftButton == ButtonState.Pressed); },
                delegate(GameTime gametime) {

                    //create a projectile that checks for collision
                    SummerBasic slash = new SummerBasic(this, .5f);
                    slash.Size = new Vector2(7, Size.Y);

                    //get position of the mouse in the world to calculate the slash's new position
                    Vector2 p = parent.Camera.PositionFromScreen(new Point(Game1.CurrentGame.MouseInput.X, Game1.CurrentGame.MouseInput.Y));

                    slash.Position = new Vector2(this.Position.X + Math.Sign(p.X - this.Position.X) * (slash.Size.X / 2 + this.Size.X / 2), this.Position.Y);

                    foreach (Entity ent in slash.CheckForCollision(parent)) {//check for collision
                        if (ent is Mob && ent != this) { //if collided ent is a mob then
                            ((Mob)ent).Damage(attack, this); // damage mob
                            mana += attack; //mana-steal
                        }

                        for (int i = 0; i < 10; i++) {//particle effects
                            Particle poi = new Particle((float)1f, (float)2);
                            poi.Position = new Vector2(ent.Position.X, ent.Position.Y);
                            double rAngle = MathHelper.ToRadians(Game1.CurrentGame.Rand.Next(0, 360));
                            double speed = Game1.CurrentGame.Rand.Next(10, 100);
                            poi.Velocity = new Vector2((float)Math.Round(Math.Cos(rAngle) * speed), (float)Math.Round(Math.Sin(rAngle) * speed));
                            poi.ColorSpeed = new Vector4(Game1.CurrentGame.Rand.Next(-10, 10), Game1.CurrentGame.Rand.Next(-10, 10), Game1.CurrentGame.Rand.Next(-10, 10), Game1.CurrentGame.Rand.Next(-4000, -2000));
                            poi.Color = Color.Gold;
                            poi.Gravity = false;
                            parent.AddParticle(poi);
                        }
                         

                    }
                    parent.AddEntity(slash);
                }));

        }
    }
}
