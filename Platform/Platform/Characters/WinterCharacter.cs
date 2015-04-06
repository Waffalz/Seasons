using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.Mobs;
using Platform.World;

namespace Platform.Characters
{
    class WinterCharacter : Player
    {
        private float ghostShield = 0.20f; //percent of damage shielded from

        public override float Health
        {
            get
            {
                return base.Health;
            }
            set
            {
                float dif = health - value;
                if (dif < 0) {
                    health += value * ghostShield; 
                }
            }
        }

        public WinterCharacter()
            : base()
        {
            manaGen = -5f;
            ghostShield = 0.20f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            mana = Math.Max(mana,0);

            if (mana <= 0) {
                health = 0;
            }
        }

    }
}
