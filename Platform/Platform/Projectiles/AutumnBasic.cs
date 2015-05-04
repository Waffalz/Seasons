using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.mobs;
using Platform.world;
using Platform.gameflow;

namespace Platform.projectiles
{
    class AutumnBasic : Projectile
    {
        public AutumnBasic()
            : base()
        {
            Anchored = false;
            gravity = false;
            solid = false;
            Size = new Vector2(10f, 10f);
        }

        public AutumnBasic(Mob creator, float lifeTime)
            : base(creator, lifeTime)
        {
            Anchored = false;
            gravity = false;
            solid = false;
            Size = new Vector2(10f, 10f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float timeElapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
            UpdatePosition(timeElapsed);

            List<Entity> hitlist = CheckForCollision(parent);
            foreach (Entity ent in hitlist) {
                if (ent is Mob && ent != creator) {
                    ((Mob)ent).Damage(creator.Attack * timeElapsed, creator);

                    for (int i = 0; i < 1; i++) {//particle effects
                        Particle poi = new Particle((float)1.5f, (float)2);
                        poi.Position = new Vector2(Position.X, Position.Y);
                        double rAngle = MathHelper.ToRadians(Game1.CurrentGame.Rand.Next(0, 360));
                        double speed = Game1.CurrentGame.Rand.Next(10, 40);
                        poi.Velocity = new Vector2((float)Math.Round(Math.Cos(rAngle) * speed), (float)Math.Round(Math.Sin(rAngle) * speed));
                        poi.ColorSpeed = new Vector4(Game1.CurrentGame.Rand.Next(-10, 10), Game1.CurrentGame.Rand.Next(-10, 10), Game1.CurrentGame.Rand.Next(-10, 10), Game1.CurrentGame.Rand.Next(-1000, -700));
                        poi.Color = Color.Gray;
                        parent.AddParticle(poi);
                    }
                }
            }
        }
    }
}
