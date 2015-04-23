using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Platform.world;
using Platform.mobs;

using Microsoft.Xna.Framework;

namespace Platform.projectiles
{
    class Projectile: Entity
    {
        public Mob creator;
        protected float maxLifeTime;
        protected float lifeTime;

        public Projectile()
        {
            creator = null;
        }

        public Projectile(Mob creator, float time)
        {
            this.creator = creator;
            maxLifeTime = time;
            lifeTime = maxLifeTime;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            lifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (lifeTime <= 0 && maxLifeTime > 0) {
                Remove();
            }
        }



    }
}
