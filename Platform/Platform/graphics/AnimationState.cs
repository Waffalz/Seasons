using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.world;
using Platform.logger;

namespace Platform.graphics
{
    public delegate void AnimationDelegate(int state);
    public delegate void DrawingDelegate(GameTime gameTime, SpriteBatch spriteBatch);

    public class AnimationState
    {
        public Entity adaptee;

        public float time;
        public float timeIncrement;

        public int frameState;
        public int maxFrameStates;

        public AnimationDelegate FrameStep;
        public AnimationDelegate LastFrame;
        public DrawingDelegate Draw;

        public AnimationState()
        {
            time = 0;
            timeIncrement = 0;

            frameState = 0;
            maxFrameStates = 0;
            FrameStep = delegate(int frame) {  };
            LastFrame = delegate(int frame) { frameState = 0; };
            Draw = delegate(GameTime gameTime, SpriteBatch spriteBatch) { spriteBatch.Draw(adaptee.Texture, adaptee.SourceRect, adaptee.Color); };
        }

        public AnimationState(float timeIncs, int maxFrames)
        {
            time = 0;
            timeIncrement = timeIncs;
            frameState = 0;
            maxFrameStates = 0;
            FrameStep = delegate(int frame) { };
            LastFrame = delegate(int frame) { frameState = 0; };
            Draw = delegate(GameTime gameTime, SpriteBatch spriteBatch) { spriteBatch.Draw(adaptee.Texture, adaptee.SourceRect, adaptee.Color); };
        }

        public virtual void Update (GameTime gameTime){
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (time > timeIncrement) {
                frameState++;
                time = 0;
                if (frameState > maxFrameStates) {
                    LastFrame(frameState);
                }
                FrameStep(frameState);
            }
        }


    }
}
