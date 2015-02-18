using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.World;

namespace Platform.Mobs
{
    class Mob:Entity
    {

        private int health;

        private Vector2 walkVelocity;
        private float walkSpeed;
        private float jumpSpeed;
        private bool onGround;
        private int shieldAmount;
        private int attackSpeed;
        private int damage;
        private int defense;


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

        public int AttackSpeed
        {
            get { return attackSpeed; }
            set { attackSpeed = value; }
        }

        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }

        public Mob():base()
        {
            Size = new Vector2(10,10);
            Texture = Game1.particleSheets["DefaultParticle"];
            SourceRect = Texture.Bounds;
            walkSpeed = 20;
            jumpSpeed = 50;
            walkVelocity = new Vector2();
            onGround = false;
            health = 100;
            attackSpeed = 1;
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
