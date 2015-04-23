using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Microsoft.Xna.Framework;

using Platform.gameflow;
using Platform.world;
using Platform.logger;

namespace Platform.mobs {
	public class Mob : Entity {

		protected float maxHealth;
		protected float health;
		protected int attack; //the amount of damage dealt by a 100% power attack on an enemy with no defense
		protected int defense; //the amount subtracted from attack when taking damage

		public Vector2 previousPosition;

		protected Vector2 walkVelocity;
		protected float walkSpeed;
		protected float jumpSpeed;
		protected bool onGround;

		protected float movementAccel;

		protected float airControl;

		protected float AirControl {
			get {
				return airControl;
			}
			set {
				airControl = value;
			}
		}

		public Vector2 WalkVelocity {
			get {
				return walkVelocity;
			}
			set {
				walkVelocity = value;
			}
		}
		public float WalkSpeed {
			get {
				return walkSpeed;
			}
			set {
				walkSpeed = value;
			}
		}
		public float JumpSpeed {
			get {
				return jumpSpeed;
			}
			set {
				jumpSpeed = value;
			}
		}
		public bool OnGround {
			get {
				return onGround;
			}
			set {
				onGround = value;
			}
		}

		public float MaxHealth {
			get {
				return maxHealth;
			}
			set {
				maxHealth = value;
			}
		}
		public virtual float Health {
			get {
				return health;
			}
			set {
				health = value;
			}
		}
		public int Attack {
			get {
				return attack;
			}
			set {
				attack = value;
			}
		}
		public int Defense {
			get {
				return defense;
			}
			set {
				defense = value;
			}
		}

		public Mob()
			: base() {
			Size = new Vector2( 10, 10 );
			Texture = Game1.CurrentGame.Textures[ "Square" ];
			SourceRect = Texture.Bounds;
			walkSpeed = 20;
			jumpSpeed = 50;
			walkVelocity = new Vector2();
			onGround = false;
			maxHealth = 100;
			health = maxHealth;
			movementAccel = 400;
			airControl = .5f;
			previousPosition = new Vector2();
		}

		public static Vector2 GetDirection( MoveDirection dir ) {
			switch ( dir ) {
				case MoveDirection.Up:
					return new Vector2( 0, 1 );
				case MoveDirection.Down:
					return new Vector2( 0, -1 );
				case MoveDirection.Left:
					return new Vector2( -1, 0 );
				case MoveDirection.Right:
					return new Vector2( 1, 0 );
				default:
					return new Vector2();
			}
		}

		public virtual void Damage( float amount ) {
			health -= amount;
		}
		public virtual void Damage( Mob attacker, float power ) {
			health -= ( attacker.Attack - defense ) * power / 100;
		}
		public virtual void Damage( float amount, Mob attacker ) {
			health -= amount;
		}

		public override void Update( GameTime gameTime ) {
			float timeDifference = ( float )gameTime.ElapsedGameTime.TotalSeconds;

			oldPos = Position;

			if ( anchored == false ) {//don't handle movement if ent is anchored
				Position += timeDifference * ( velocity + WalkVelocity );
			}
			if ( gravity == true ) { //handle gravity on ent if gravity is true
				velocity = new Vector2( velocity.X, velocity.Y + parent.Gravity * timeDifference );
			}

			if ( onGround ) {
				if ( !velocity.Equals( Vector2.Zero ) ) {
					Vector2 toDisp = velocity;
					toDisp.Normalize();

					int tilePositionX = ( int )( Position.X / Tile.TILE_WIDTH );
					int tilePositionY = ( int )( Position.Y / Tile.TILE_WIDTH );
					Tile nextTile = null;
					try {
						nextTile = parent.Tiles[ tilePositionY - 1, tilePositionX ];
					} catch ( Exception e ) {
						Console.Error.WriteLine( "Error finding nextTile!" );
						Console.Error.WriteLine( e.Message );
					}
					if ( nextTile != null ) {
						previousPosition = new Vector2( ( tilePositionX + .5f ) * Tile.TILE_WIDTH, Position.Y + .1f );
					}



					if ( velocity.X != 0 ) {
						velocity.X = Math.Sign( velocity.X ) * ( Math.Max( Math.Abs( velocity.X ) - movementAccel / 2 * Math.Abs( toDisp.X ) * timeDifference, 0 ) );
					}
				}
				walkVelocity.Y = 0;
			}

			if ( !walkVelocity.Equals( Vector2.Zero ) ) {
				Vector2 toDisp = walkVelocity;
				toDisp.Normalize();

				if ( walkVelocity.X != 0 && onGround ) {
					walkVelocity.X = Math.Sign( walkVelocity.X ) * ( Math.Max( Math.Abs( walkVelocity.X ) - movementAccel / 2 * Math.Abs( toDisp.X ) * timeDifference, 0 ) );
				}
				if ( walkVelocity.Y != 0 ) {
					walkVelocity.Y = Math.Sign( walkVelocity.Y ) * ( Math.Max( Math.Abs( walkVelocity.Y ) - movementAccel / 2 * Math.Abs( toDisp.Y ) * timeDifference, 0 ) );
				}
			}

		}

		public override void CorrectCollisionPosition( List<Entity> ents ) {
			onGround = false;
			System.Drawing.RectangleF thisRekt = rect;
			System.Drawing.RectangleF botPlace = new System.Drawing.RectangleF( thisRekt.X, thisRekt.Y + thisRekt.Height, thisRekt.Width, .1f );
			foreach ( Entity tilent in ents ) { //Tile collisions
				System.Drawing.RectangleF otherRekt = tilent.Rekt;
				if ( thisRekt.IntersectsWith( otherRekt ) ) {
					if ( oldPos.Y + Size.Y / 2 <= tilent.Position.Y - tilent.Size.Y / 2 ) {// if ent is under tile

						Position = new Vector2( Position.X, tilent.Position.Y - ( tilent.Size.Y + Size.Y ) / 2 );
						velocity = new Vector2( velocity.X, 0 );
					}

					if ( oldPos.X - Size.X / 2 >= tilent.Position.X + tilent.Size.X / 2 ) {// if ent is to the right of tile 

						Position = new Vector2( tilent.Position.X + ( tilent.Size.X + Size.X ) / 2, Position.Y );
						velocity = new Vector2( 0, velocity.Y );
						OnCollide( tilent );
						continue;
					}
					if ( oldPos.X + Size.X / 2 <= tilent.Position.X - tilent.Size.X / 2 ) { // if ent is to the left of tile

						Position = new Vector2( tilent.Position.X - ( tilent.Size.X + Size.X ) / 2, Position.Y );
						velocity = new Vector2( 0, velocity.Y );
						OnCollide( tilent );
						continue;

					}
					if ( oldPos.Y - Size.Y / 2 >= tilent.Position.Y + tilent.Size.Y / 2 ) { //if ent is over tile
						Position = new Vector2( Position.X, tilent.Position.Y + ( tilent.Size.Y + Size.Y ) / 2 );
						velocity = new Vector2( velocity.X, 0 );
						OnGround = true;
					}
					OnCollide( tilent );
				}
				if ( botPlace.IntersectsWith( otherRekt ) ) {
					onGround = true;
				}


			}
		}

	}
}