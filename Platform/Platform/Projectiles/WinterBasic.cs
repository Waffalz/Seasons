using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.world;
using Platform.gameflow;
using Platform.mobs;

namespace Platform.projectiles
{
    class WinterBasic: Projectile
    {
        public WinterBasic()
            : base()
        {
            texture = Game1.CurrentGame.Textures["Icicle"];
        }

        public WinterBasic(Mob creator, float time)
            : base(creator, time)
        {
            texture = Game1.CurrentGame.Textures["Icicle"];
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            color.A = (byte)(255*lifeTime/maxLifeTime);
        }
        

    }
}
