using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Platform.gameflow;
using Platform.mobs;
using Platform.control;
using Platform.logger;

namespace Platform.characters
{
    class TestPlayer : Player
    {
        float shotspeed;
        public float ShotSpeed
        {
            get { return shotspeed; }
            set { shotspeed = value; }
        }

        float spread;
        public float Spread
        {
            get { return spread; }
            set { spread = value; }
        }

        public TestPlayer()
            : base()
        {
            spread = 10;
            shotspeed = 200;

            controls.Add("Basic Attack", new ContinuousAction(this, (float).5,
                delegate() { return Game1.CurrentGame.MouseInput.LeftButton == ButtonState.Pressed; },
                delegate(GameTime gameTime)
                {
                    BasicAttack(spread);
                }));
            controls.Add("Shatgann", new OnceAction(this, 1,
                delegate() { return (Game1.CurrentGame.MouseInput.RightButton == ButtonState.Pressed); },
                delegate() { return (Game1.CurrentGame.OldMouseInput.RightButton == ButtonState.Pressed); },
                delegate(GameTime gameTime)
                {
                    float cost = 20;
                    if (mana > cost)
                    {
                        for (int p = 0; p < 10; p++)
                        {
                            BasicAttack(spread * 3);
                        }
                        mana -= cost;
                    }
                }));


        }

        public void BasicAttack(float spread)
        {
            Vector2 target = parent.Camera.PositionFromScreen(new Point(Game1.CurrentGame.MouseInput.X, Game1.CurrentGame.MouseInput.Y));
            Vector2 dif = target - Position;

            double ang = (float)Math.Atan2((double)dif.Y, (double)dif.X) + MathHelper.ToRadians(Game1.CurrentGame.Rand.Next((int)(-spread / 2), (int)(spread / 2)));//calculate spread

            dif.Normalize();

            Ball b = new Ball();
            b.Creator = this;
            b.Position = Position;
            b.Velocity = new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang)) * shotspeed;
            b.Size = new Vector2(5);
            b.LifeLeft = 5;
            b.Gravity = true;

            parent.AddEntity(b);

        }

    }
}
