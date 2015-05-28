using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.graphics;

namespace Platform.characters.animation
{
    public class PlayerMovementAnimation: AnimationState
    {
        public PlayerMovementAnimation()
            : base()
        {
            //set default player sourceRect
            LastFrame = delegate(int frame) { frameState = 0; };
        }

        public PlayerMovementAnimation(float inc, int maxStates)
            : base(inc, maxStates)
        {
            LastFrame = delegate(int frame) { frameState = 0; };
        }


    }
}
