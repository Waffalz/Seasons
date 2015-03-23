using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Platform.Mobs
{
    public class Baddu:Mob, Behavior
    {
        MoveDirection moveDirection;

        public MoveDirection MoveIntention
        {
            get { return moveDirection; }
            set { moveDirection = value; }
        }

        public Baddu():base()
        {
            moveDirection = MoveDirection.Left;
            size = new Vector2(10,10);
            texture = Game1.entSheets["Player"];
            texSrc = texture.Bounds;
            color = Color.Red;
        }

        public void Behave(float timeDif){
            Vector2 dir = Mob.GetDirection(moveDirection);
            WalkVelocity += new Vector2(dir.X * WalkSpeed, 0);
            



        }

    }
}
