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

        protected int health;

        protected Vector2 walkVelocity;
        protected float walkSpeed;
        protected float jumpSpeed;
        protected bool onGround;

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
        }

        public void Damage(int toHit)
        {
            health -= toHit;
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
    }
}
