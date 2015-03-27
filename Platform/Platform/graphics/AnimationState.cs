using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.World;

namespace Platform.Graphics
{
    public class AnimationState
    {
        public Entity adaptee;

        public float time;
        public float timeIncrement;

        public int frameState;
        public int maxFrameStates;

        public virtual void Update (GameTime gameTime){
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (time > timeIncrement) {
                frameState++;
                time = 0;
            }

        }

        public virtual void FrameStep()
        {

        }

    }
}
