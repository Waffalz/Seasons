using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.Graphics;
using Platform.World;
using Microsoft.Xna.Framework.Input;

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
            
            if (Game1.kipz.IsKeyDown(Keys.D)) //running right
            {
                walkVelocity += new Vector2(walkSpeed, 0);
            }
            if (Game1.kipz.IsKeyDown(Keys.A)) //running left
            {
                walkVelocity += new Vector2(-walkSpeed, 0);

            }

            if ((Game1.kipz.IsKeyDown(Keys.Space) || Game1.kipz.IsKeyDown(Keys.W)) && onGround)//jumping
            {
                for (int i = 0; i < 30; i++)//particle effects
                {
                    Particle poi = new Particle((float)2, (float)2);
                    poi.Color = Color.SkyBlue;
                    poi.Position = new Vector2(position.X, position.Y - size.Y / 2);
                    double rAngle = MathHelper.ToRadians(Game1.rand.Next(0, 360));
                    double speed = Game1.rand.Next(20, 40);
                    poi.Velocity = new Vector2((float)Math.Round(Math.Cos(rAngle) * speed), Math.Abs((float)Math.Round(Math.Sin(rAngle) * speed)));
                    poi.ColorSpeed = new Vector3(Game1.rand.Next(-10, 10), Game1.rand.Next(-10, 10), Game1.rand.Next(-10, 10));
                    parent.AddParticle(poi);

                }
                onGround = false;
                velocity = new Vector2(velocity.X, jumpSpeed);
            }

            if (walkVelocity.X != 0 && onGround == true)
            { //fancy particles effects when running
                for (int i = 0; i < 5; i++)
                {
                    Particle poi = new Particle((float)2, (float)1);
                    poi.Color = Color.SkyBlue;
                    poi.Position = new Vector2(position.X, position.Y - size.Y / 2);
                    double rAngle = MathHelper.ToRadians(Game1.rand.Next(0, 360));
                    float speed = Game1.rand.Next(20, 40);
                    poi.Velocity = new Vector2((float)Math.Cos(rAngle), Math.Abs((float)(Math.Sin(rAngle)))) * speed;
                    poi.ColorSpeed = new Vector3(Game1.rand.Next(-10, 10), Game1.rand.Next(-10, 10), Game1.rand.Next(-10, 10));
                    parent.AddParticle(poi);

                }
            }
            if (Game1.mus.LeftButton == ButtonState.Pressed)
            {//left click firing
                if (AttackTime > MaxAttack)
                {
                    Attack(parent.Cam.PositionFromScreen(new Point(Game1.mus.X, Game1.mus.Y)));

                    AttackTime = 0;

                }
            }
            parent.Cam.ZoomScale += (Game1.mus.ScrollWheelValue - Game1.oMus.ScrollWheelValue) / 120;
            
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
