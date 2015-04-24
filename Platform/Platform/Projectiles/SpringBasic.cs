using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.world;
using Platform.mobs;
using Platform.logger;

namespace Platform.projectiles
{
    class SpringBasic : Projectile
    {

        public SpringBasic()
            : base()
        {

        }

        public SpringBasic(Mob creator, float lifeTime)
            : base(creator, lifeTime)
        {

        }

        public override void Update(GameTime gametime)
        {
            float timeElapsed = (float)gametime.ElapsedGameTime.TotalSeconds;
            UpdatePosition(timeElapsed);

        }

        public override void OnCollide(Entity other)
        {
            if (other is Mob) {
                ((Mob)other).Damage(creator.Attack, creator);
            }
            if (other is TileEntity) {
                //do tile collision stuff
            }
        }

    }
}
