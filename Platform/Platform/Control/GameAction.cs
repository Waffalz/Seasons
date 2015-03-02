using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using Platform.World;

namespace Platform.Control
{
    delegate void ActionDelegate();
    delegate bool ActionCheck();

    abstract class GameAction
    {
        protected float maxCooldown;
        protected float currentCooldown;
        protected Entity user;

        protected bool active;
        protected bool oldActive;

        protected bool enabled;

        public float MaxCooldown
        {
            get { return maxCooldown; }
            set { maxCooldown = value; }
        }
        public float CurrentCooldown
        {
            get { return currentCooldown; }
            set { currentCooldown = value; }
        }
        public Entity User
        {
            get { return user; }
            set { user = value; }
        }
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public bool OldActive
        {
            get { return oldActive; }
            set { oldActive = value; }
        }

        public GameAction()
        {
            maxCooldown = 1;
            currentCooldown = 0;
            user = null;
            enabled = true;
            active = false;
            oldActive = false;
        }

        public abstract void Update(GameTime gameTime);


    }
}
 