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
        private float ghostShield = 20f; //percent of damage shielded from


        public WinterCharacter()
            : base()
        {
            manaGen = -5f;
            ghostShield = 0.20f;
        }

        public override void Damage(float amount)
        {
            health -= amount * (100-ghostShield)/100;
        }
        public override void Damage(Mob attacker, float power)
        {
            health -= ((attacker.Attack - defense) * power / 100) * (100 - ghostShield) / 100;
        }
        public override void Damage(float amount, Mob attacker)
        {
            health -= amount * (100 - ghostShield) / 100;
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
