using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.mobs;

namespace Platform.projectiles
{
    class SummerBasic:Projectile
    {
        public SummerBasic()
            : base()
        {

        }

        public SummerBasic(Mob creator, float time)
            : base(creator, time)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            color.A = (byte)(255*lifeTime/maxLifeTime);
        }

    }
}
