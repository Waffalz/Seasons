using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.mobs;

namespace Platform.projectiles
{
    class AutumnBasic : Projectile
    {
        public AutumnBasic()
            : base()
        {
            gravity = false;
            solid = false;
        }

        public AutumnBasic(Mob creator, float lifeTime)
            : base(creator, lifeTime)
        {
            gravity = false;
            solid = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }

    }
}
