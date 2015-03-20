using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.GameFlow;
using Platform.World;

namespace Platform.Mobs
{
    public class Mob:Entity
    {

        protected int maxHealth;
        protected int health;

        protected Vector2 walkVelocity;
        protected float walkSpeed;
        protected float jumpSpeed;
        protected bool onGround;

        protected float movementDecel;

        public Vector2 WalkVelocity
        {
            get { return walkVelocity; }
            set { walkVelocity = value; }
        }
        public float WalkSpeed
        {
            get { return walkSpeed; }
            set { walkSpeed = value; }
        }
        public float JumpSpeed
        {
            get { return jumpSpeed; }
            set { jumpSpeed = value; }
        }
        public bool OnGround
        {
            get { return onGround; }
            set { onGround = value; }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; } 
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public Mob():base()
        {
            Size = new Vector2(10,10);
            Texture = Game1.CurrentGame.Textures["Square"];
            SourceRect = Texture.Bounds;
            walkSpeed = 20;
            jumpSpeed = 50;
            walkVelocity = new Vector2();
            onGround = false;
            maxHealth = 100;
            health = maxHealth;
            movementDecel = -150;
        }


        public static Vector2 GetDirection(MoveDirection dir)
        {
            switch (dir) {
                case MoveDirection.Up:
                    return new Vector2(0, 1);
                case MoveDirection.Down:
                    return new Vector2(0, -1);
                case MoveDirection.Left:
                    return new Vector2(-1, 0);
                case MoveDirection.Right:
                    return new Vector2(1, 0);
                default:
                    return new Vector2();
            }
        }

        public override void Update(GameTime gameTime)
        {
            float timeDifference = (float)gameTime.ElapsedGameTime.TotalSeconds;

            oldPos = position;

            if (Anchored == false) {//don't handle movement if ent is anchored
                position = position + timeDifference * (velocity + WalkVelocity);
            }
            if (gravity == true) { //handle gravity on ent if gravity is true
                velocity = new Vector2(velocity.X, velocity.Y + parent.Gravity * timeDifference);
            }

            if (!walkVelocity.Equals(Vector2.Zero)) {
                Vector2 toDisp = walkVelocity;
                toDisp.Normalize();

                if (walkVelocity.X != 0) {
                    walkVelocity.X = Math.Sign(walkVelocity.X) * (Math.Max(Math.Abs(walkVelocity.X) + movementDecel * Math.Abs(toDisp.X) * timeDifference, 0));
                }
                if (walkVelocity.Y != 0) {
                    walkVelocity.Y = Math.Sign(walkVelocity.Y) * (Math.Max(Math.Abs(walkVelocity.Y) + movementDecel * Math.Abs(toDisp.Y) * timeDifference, 0));
                }
            }

        }

        public override void CorrectCollisionPosition(List<Entity> ents)
        {
            onGround = false;
            foreach (Entity tilent in ents) { //Tile collisions
                //System.Drawing.RectangleF re = tilent.getRekt();
                //re.Size += new System.Drawing.SizeF(0,WALL_BUFFER);
                //re = new System.Drawing.RectangleF(tilent.Position.X - re.Size.Width / 2, -(tilent.Position.Y + re.Size.Height), re.Size.Width, re.Size.Height);
                if (Collides(tilent)) {//if ent collides with a tileent
                    if (oldPos.Y - size.Y / 2 >= tilent.Position.Y + tilent.Size.Y / 2) { //if ent is over tile
                        position = new Vector2(position.X, tilent.Position.Y + (tilent.Size.Y + size.Y) / 2);
                        velocity = new Vector2(velocity.X, 0);
                        onGround = true;
                    }
                    else {
                        if (oldPos.Y + size.Y / 2 <= tilent.Position.Y - tilent.Size.Y / 2) {// if ent is under tile

                            position = new Vector2(position.X, tilent.Position.Y - (tilent.Size.Y + size.Y) / 2);
                            velocity = new Vector2(velocity.X, 0);
                        }
                        if (oldPos.X - size.X / 2 >= tilent.Position.X + tilent.Size.X / 2) {// if ent is to the right of tile 

                            position = new Vector2(tilent.Position.X + (tilent.Size.X + size.X) / 2, position.Y);
                            velocity = new Vector2(0, velocity.Y);
                        }
                        if (oldPos.X + size.X / 2 <= tilent.Position.X - tilent.Size.X / 2) { // if ent is to the left of tile

                            position = new Vector2(tilent.Position.X - (tilent.Size.X + size.X) / 2, position.Y);
                            velocity = new Vector2(0, velocity.Y);
                        }
                    }
                    OnCollide(tilent);
                }
            }
        }

    }
}
