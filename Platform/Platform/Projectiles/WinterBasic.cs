using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.world;
using Platform.mobs;

namespace Platform.projectiles
{
    class WinterBasic: Projectile
    {
        public WinterBasic()
            : base()
        {
            
        }

        
        public WinterBasic(Mob creator, float time): base(creator, time)
        {
        }
         

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            color.A = (byte)(255*lifeTime/maxLifeTime);
        }
        

    }
}
