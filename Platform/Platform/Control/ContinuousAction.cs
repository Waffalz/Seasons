using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.GameFlow;
using Platform.World;

namespace Platform.Control
{
    class ContinuousAction : GameAction
    {

        private ActionDelegate action;
        private ActionCheck activeCheck;

        public ContinuousAction(): base()
        {
            action = null;
            activeCheck = null;
        }

        public ContinuousAction(Entity sub, float cd, ActionCheck toCheck,ActionDelegate todo):base()
        {
            user = sub;
            action = todo;
            maxCooldown = cd;
            activeCheck = toCheck;
        }

        public override void Update(GameTime gameTime)
        {
            if (activeCheck != null) {
                active = activeCheck();
            }
            currentCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (enabled && active && (currentCooldown >= maxCooldown)) {
                if (action != null) {
                    action(gameTime);
                }
                currentCooldown = 0;
            }
            oldActive = active;
        }


    }
}
