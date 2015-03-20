using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.GameFlow;
using Platform.World;

namespace Platform.Control
{
    class OnceAction: GameAction
    {

        private ActionDelegate action;
        private ActionCheck activeCheck;
        private ActionCheck OldActiveCheck;

        public OnceAction(): base(){
            action = null;
            activeCheck = null;
            OldActiveCheck = null;
        }
        public OnceAction(Entity sub, float cd, ActionCheck check, ActionCheck oCheck, ActionDelegate todo)
        {
            user = sub;
            maxCooldown = cd;
            activeCheck = oCheck;
            action = todo;
        }

        public override void Update(GameTime gameTime){
            if (activeCheck != null){
                active = activeCheck();
            }
            if (OldActiveCheck != null){
                oldActive = OldActiveCheck();
            }
            currentCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (enabled && active && !oldActive && (currentCooldown >= maxCooldown)) {
                if (action != null) {
                    action(gameTime);
                }
                currentCooldown = 0;
            }
            oldActive = active;
        }


    }
}
