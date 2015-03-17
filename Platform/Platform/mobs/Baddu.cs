using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.World;

namespace Platform.Mobs
{
    class Baddu:Mob, Behavior
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

            int tilePositionX = (int)(position.X / Tile.TILE_WIDTH);
            int tilePositionY = (int)(position.Y / Tile.TILE_WIDTH);

            Tile nextTile = parent.Tiles[tilePositionY, tilePositionX + (int)(Mob.GetDirection(moveDirection).X)];


            Entity nextPlace = new Entity();
            nextPlace.Position = this.position + (velocity + WalkVelocity) * timeDif;
            nextPlace.Size = this.size;

            if (nextTile != null)
            {
                Entity lent = new Entity();
                lent.Size = new Vector2(Tile.TILE_WIDTH, Tile.TILE_WIDTH);
                lent.Position = new Vector2((tilePositionX + (int)(Mob.GetDirection(moveDirection).X)) * Tile.TILE_WIDTH + Tile.TILE_WIDTH / 2, (tilePositionY) * Tile.TILE_WIDTH + Tile.TILE_WIDTH / 2);
                lent.Anchored = true;
                lent.Gravity = false;
                lent.Solid = true;

                if (nextPlace.Collides(lent))
                {
                    if (moveDirection == MoveDirection.Left)
                    {
                        moveDirection = MoveDirection.Right;
                    }
                    else if (moveDirection == MoveDirection.Right)
                    {
                        moveDirection = MoveDirection.Left;
                    }

                }

            }
            

        }

    }
}
