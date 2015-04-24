using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Platform.world;
using Platform.mobs;
using Platform.gameflow;

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
            Texture = Game1.CurrentGame.Textures["Square"];
            sourceRect = texture.Bounds;
        }

        public Projectile(Mob creator, float time)
        {
            this.creator = creator;
            maxLifeTime = time;
            lifeTime = maxLifeTime;
            Texture = Game1.CurrentGame.Textures["Square"];
            sourceRect = texture.Bounds;
        }

        public override void Update(GameTime gameTime)
        {
            float timeElapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateLifeTime(timeElapsed);
        }

        public virtual void UpdateLifeTime(float timeElapsed)
        {
            lifeTime -= timeElapsed;
            if (lifeTime <= 0 && maxLifeTime > 0) {
                Remove();
            }
        }

    }
}
