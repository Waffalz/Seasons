using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.world;
using Platform.mobs;
using Platform.gameflow;
using Platform.logger;

namespace Platform.projectiles
{
    class SpringBasic : Projectile
    {

        public SpringBasic()
            : base()
        {
            Size = new Vector2(5f,5f);
            texture = Game1.CurrentGame.Textures["CircleParticle"];
            sourceRect = texture.Bounds;
        }

        public SpringBasic(Mob creator, float lifeTime)
            : base(creator, lifeTime)
        {
            Size = new Vector2(5f, 5f);
            texture = Game1.CurrentGame.Textures["CircleParticle"];
            sourceRect = texture.Bounds;
        }

        public override void Update(GameTime gameTime)
        {
            float timeElapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
            UpdatePosition(timeElapsed);
            UpdateGravity(timeElapsed);
            CorrectCollisionPosition();
        }

        public override void OnCollide(Entity other)
        {
            if (other != creator && !(other is Projectile)) {
                if (other is Mob) {
                    ((Mob)other).Damage(creator.Attack, creator);
                }

                //if the seed lands on a tile block it has a chance to spawn a plant thrall
                if (other is TileEntity && ((oldPos.Y - Size.Y / 2) > (other.Position.Y + other.Size.Y / 2))) {
                    //implement 

                }
                Remove();
            }
        }

    }
}
