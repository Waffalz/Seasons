using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.Graphics;
using Platform.World;

namespace Platform.Mobs
{
    class Player : Mob
    {

        float attackTime;
        float maxAttack;

        float spread;
        float shotspeed;

        public float ShotSpeed
        {
            get { return shotspeed; }
            set { shotspeed = value; }
        }
        public float Spread
        {
            get { return spread; }
            set { spread = value; }
        }
        public float AttackTime
        {
            get { return attackTime; }
            set { attackTime = value; }
        }
        public float MaxAttack
        {
            get { return maxAttack; }
            set { maxAttack = value; }
        }

        public Player():base()
        {
            Size = new Vector2(10,10);
            texture = Game1.entSheets["Player"];
            SourceRect = texture.Bounds;
            WalkSpeed = 50;
            JumpSpeed = 100;
            WalkVelocity = new Vector2();
            OnGround = false;

            spread = 10;
            shotspeed = 200;

            attackTime = 0;
            maxAttack = (float).01;
        }


        public override void Update(float gameTime)
        {
            base.Update(gameTime);
            attackTime += gameTime;

        }

        public void Attack(Vector2 target)
        {
            Vector2 dif = target - position;

            
            double ang = (float)Math.Atan2((double)dif.Y, (double)dif.X) + MathHelper.ToRadians(Game1.rand.Next((int)(-spread / 2), (int)(spread / 2)));//calculate spread

            dif.Normalize();

            Ball b = new Ball();
            b.Creator = this;
            b.Position = position;
            b.Velocity = new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang)) * shotspeed;
            b.Size = new Vector2(5);
            b.LifeLeft = 5;
            b.Gravity = true;

            parent.AddEntity(b);
        }
    }
}
