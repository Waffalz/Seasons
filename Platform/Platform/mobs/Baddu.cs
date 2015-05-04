using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.gameflow;
using Platform.world;
using Platform.logger;

namespace Platform.mobs {
	class Baddu : Mob, Behavior {
		MoveDirection moveDirection;

		public MoveDirection MoveIntention {
			get {
				return moveDirection;
			}
			set {
				moveDirection = value;
			}
		}

		public Baddu()
			: base() {
			moveDirection = MoveDirection.Left;
			Size = new Vector2( 10, 10 );
			Texture = Game1.CurrentGame.Textures[ "Player" ];
			SourceRect = texture.Bounds;
			color = Color.Red;
            health = 10;
		}

		public void Behave( float timeDif ) {
			Vector2 dir = Mob.GetDirection( moveDirection );
			WalkVelocity = new Vector2( dir.X * WalkSpeed, 0 );

			int tilePositionX = ( int )( Position.X / Tile.TILE_WIDTH );
			int tilePositionY = ( int )( Position.Y / Tile.TILE_WIDTH );
			Tile nextTile = null;
			try {
				nextTile = parent.Tiles[ tilePositionY, tilePositionX + ( int )( Mob.GetDirection( moveDirection ).X ) ];
			} catch ( Exception e ) {
				nextTile = null;
				Console.WriteLine( e.Message );
			}
			Entity nextPlace = new Entity();
			nextPlace.Position = this.Position + ( velocity + WalkVelocity ) * timeDif;
			nextPlace.Size = this.Size;

			if ( nextTile != null ) {
				Entity lent = new Entity();
				lent.Size = new Vector2( Tile.TILE_WIDTH, Tile.TILE_WIDTH );
				lent.Position = new Vector2( ( tilePositionX + ( int )( Mob.GetDirection( moveDirection ).X ) ) * Tile.TILE_WIDTH + Tile.TILE_WIDTH / 2, ( tilePositionY ) * Tile.TILE_WIDTH + Tile.TILE_WIDTH / 2 );
				lent.Anchored = true;
				lent.Gravity = false;
				lent.Solid = true;

				if ( nextPlace.Collides( lent ) ) {
					if ( moveDirection == MoveDirection.Left ) {
						moveDirection = MoveDirection.Right;
					} else if ( moveDirection == MoveDirection.Right ) {
						moveDirection = MoveDirection.Left;
					}

				}

			}
            if (health <= 0)
            {
                parent.RemoveEntity(this);
            }

		}

	}
}