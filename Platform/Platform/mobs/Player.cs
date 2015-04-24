using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Platform.gameflow;
using Platform.graphics;
using Platform.world;
using Platform.control;
using Platform.logger;

namespace Platform.mobs {
	public class Player : Mob {
		protected float mana;
		public float Mana {
			get {
				return mana;
			}
			set {
				mana = value;
			}
		}

		protected float maxMana;
		public float MaxMana {
			get {
				return maxMana;
			}
			set {
				maxMana = value;
			}
		}

		protected float manaGen;
		public float ManaGen {
			get {
				return manaGen;
			}
			set {
				manaGen = value;
			}
		}

		protected MoveDirection moveDir;

		protected Dictionary<string,GameAction> controls;

		public Player()
			: base() {
			Size = new Vector2( ( float )8, ( float )16 );
			texture = Game1.CurrentGame.Textures[ "Square" ];
			SourceRect = texture.Bounds;
			WalkSpeed = 50;
			JumpSpeed = 100;
			WalkVelocity = new Vector2();
			OnGround = false;

			moveDir = MoveDirection.None;

			controls = new Dictionary<string, GameAction>();

			maxMana = 100;
			mana = maxMana;
			manaGen = 10;

			controls.Add( "Move Left", new ContinuousAction( this, 0,
				delegate() {
					return Game1.CurrentGame.KeyboardInput.IsKeyDown( Keys.D );
				},
				delegate( GameTime gameTime ) {
					if ( Game1.CurrentGame.KeyboardInput.IsKeyDown( Keys.A ) ) {
						if ( moveDir == MoveDirection.Right || !Game1.CurrentGame.OldKeyboardInput.IsKeyDown( Keys.D ) ) {
							//walkVelocity.X = walkSpeed
							if ( onGround ) {
								walkVelocity.X = Math.Min( Math.Abs( walkVelocity.X ) + movementAccel * ( float )gameTime.ElapsedGameTime.TotalSeconds, walkSpeed );
							} else {
								walkVelocity.X = Math.Min( walkVelocity.X + airControl * movementAccel * ( float )gameTime.ElapsedGameTime.TotalSeconds, walkSpeed );
							}
							moveDir = MoveDirection.Right;
						}
					} else {
						//walkVelocity.X = walkSpeed
						if ( onGround ) {
							walkVelocity.X = Math.Min( Math.Abs( walkVelocity.X ) + movementAccel * ( float )gameTime.ElapsedGameTime.TotalSeconds, walkSpeed );
						} else {
							walkVelocity.X = Math.Min( walkVelocity.X + airControl * movementAccel * ( float )gameTime.ElapsedGameTime.TotalSeconds, walkSpeed );
						}
						moveDir = MoveDirection.Right;
					}
				} ) );//add run left control

			controls.Add( "Move Right", new ContinuousAction( this, 0,
				delegate() {
					return Game1.CurrentGame.KeyboardInput.IsKeyDown( Keys.A );
				},
				delegate( GameTime gameTime ) {
					if ( Game1.CurrentGame.KeyboardInput.IsKeyDown( Keys.D ) ) {
						if ( moveDir == MoveDirection.Left || !Game1.CurrentGame.OldKeyboardInput.IsKeyDown( Keys.A ) ) {
							//walkVelocity.X = -walkSpeed
							if ( onGround ) {
								walkVelocity.X = -Math.Min( Math.Abs( walkVelocity.X ) + movementAccel * ( float )gameTime.ElapsedGameTime.TotalSeconds, walkSpeed );
							} else {
								walkVelocity.X = Math.Max( walkVelocity.X - ( airControl * movementAccel * ( float )gameTime.ElapsedGameTime.TotalSeconds ), -walkSpeed );
							}
							moveDir = MoveDirection.Left;
						}
					} else {
						//walkVelocity.X = -walkSpeed
						if ( onGround ) {
							walkVelocity.X = -Math.Min( Math.Abs( walkVelocity.X ) + movementAccel * ( float )gameTime.ElapsedGameTime.TotalSeconds, walkSpeed );
						} else {
							walkVelocity.X = Math.Max( walkVelocity.X - ( airControl * movementAccel * ( float )gameTime.ElapsedGameTime.TotalSeconds ), -walkSpeed );
						}
						moveDir = MoveDirection.Left;
					}
				} ) );//add run right control

			controls.Add( "Jump", new OnceAction( this, 0,
				delegate() {
					return Game1.CurrentGame.KeyboardInput.IsKeyDown( Keys.W ) || Game1.CurrentGame.KeyboardInput.IsKeyDown( Keys.Space );
				},
				delegate() {
					return Game1.CurrentGame.OldKeyboardInput.IsKeyDown( Keys.W ) || Game1.CurrentGame.OldKeyboardInput.IsKeyDown( Keys.Space );
				},
				delegate( GameTime gameTime ) {
					if ( onGround ) {
						onGround = false;
						Position += new Vector2( 0, .1f );//elevating player position by a negligible amount to get around that stupid no-jumping bug
						velocity = new Vector2( velocity.X, jumpSpeed );
					}
				} ) );

		}

		public override void Update( GameTime gameTime ) {
            float timeDifference = (float)gameTime.ElapsedGameTime.TotalSeconds;

            oldPos = Position;

            UpdatePosition(timeDifference);

            UpdateGravity(timeDifference);

            RecordLastSafePosition(timeDifference);

            UpdateGroundVelocity(timeDifference);

            UpdateWalkVelocity(timeDifference);

            UpdateControls(gameTime);


            UpdateZoom();

            UpdateMana(timeDifference);

            CorrectCollisionPosition();
		}

        public virtual void UpdateMana(float timeDifference)
        {
            mana = Math.Min(maxMana, mana + manaGen * timeDifference);
        }

        public virtual void UpdateControls(GameTime gameTime)
        {
            foreach (KeyValuePair<string, GameAction> a in controls) {
                a.Value.Update(gameTime);
            }
        }

        public virtual void UpdateZoom()
        {
            parent.Camera.ZoomScale += (Game1.CurrentGame.MouseInput.ScrollWheelValue - Game1.CurrentGame.OldMouseInput.ScrollWheelValue) / 120;
        }

	}

}