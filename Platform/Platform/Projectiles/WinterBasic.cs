using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.world;

namespace Platform.projectiles
{
    class WinterBasic: Projectile
    {
        public WinterBasic()
            : base()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            color = new Color(color.R, color.G, color.B, lifeTime/maxLifeTime);
        }
        

    }
}
