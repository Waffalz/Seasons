using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using Platform.gameflow;
using Platform.graphics;
using Platform.world;
using Platform.control;
using Platform.characters.animation;
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

        protected PlayerMovementAnimation defaultAnimState;

		protected Dictionary<string,GameAction> controls;

		public Player()
			: base() {
			Size = new Vector2( ( float )8, ( float )20 );
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

            //the moving left
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


            //the moving right
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

            //the jumps
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

            //movement animations
            
            defaultAnimState = new PlayerMovementAnimation(0, 1);
            defaultAnimState.Draw = delegate(GameTime gameTime, SpriteBatch spriteBatch) {
                KeyboardState kipz = Game1.CurrentGame.KeyboardInput;
                if (kipz.IsKeyDown(Keys.D) && moveDir == MoveDirection.Right) {
                    if (onGround) {
                        //running right on ground animation
                        DrawChar(spriteBatch, new Rectangle(512 * 2, 0, 512, 512));
                        return;
                    } else {
                        //going right in air animation
                        DrawChar(spriteBatch, new Rectangle(512 * 2, 0, 512, 512));
                        return;
                    }
                }
                if (kipz.IsKeyDown(Keys.A) && moveDir == MoveDirection.Left) {
                    if (onGround) {
                        //running left on ground animation
                        DrawChar(spriteBatch, new Rectangle(512 * 3, 0, 512, 512));
                        return;
                    } else {
                        //going left in air animation
                        DrawChar(spriteBatch, new Rectangle(512 * 3, 0, 512, 512));
                        return;
                    }
                }
                if (!(kipz.IsKeyDown(Keys.D) || (kipz.IsKeyDown(Keys.A)))) {
                    if (onGround) {
                        if (moveDir == MoveDirection.Right) {
                            //idle on ground facing right animation
                            DrawChar(spriteBatch, new Rectangle(512 * 0, 0, 512, 512));
                            return;
                        }
                        if (moveDir == MoveDirection.Left) {
                            //idle on ground facing left animation
                            DrawChar(spriteBatch, new Rectangle(512 * 1, 0, 512, 512));
                            return;
                        }
                    } else {
                        if (moveDir == MoveDirection.Right) {
                            //idle in air facing right animation
                            DrawChar(spriteBatch, new Rectangle(512 * 0, 0, 512, 512));
                            return;
                        }
                        if (moveDir == MoveDirection.Left) {
                            //idle in air facing left animation
                            DrawChar(spriteBatch, new Rectangle(512 * 1, 0, 512, 512));
                            return;
                        }
                    }

                }
                DrawChar(spriteBatch, new Rectangle(512 * 0, 0, 512, 512));
            };
            

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

            if (AnimState != null) {
                AnimState.Update(gameTime);
            } else {
                defaultAnimState.Update(gameTime);
            }

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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // vbase.Draw(gameTime, spriteBatch);
            if (AnimState != null) {
                AnimState.Draw(gameTime, spriteBatch);
            } else {
                defaultAnimState.Draw(gameTime, spriteBatch);
            }
        }

        public void DrawChar(SpriteBatch spriteBatch, Rectangle src)
        {
            if (texture != null) {
                spriteBatch.Draw(texture, new Microsoft.Xna.Framework.Rectangle(
                    (int)((Position.X - parent.Camera.Position.X - Size.Y/2) * parent.Camera.ZoomScale + parent.Camera.PointOnScreen.X),
                    (int)(-(Position.Y + Size.Y / 2 - parent.Camera.Position.Y) * parent.Camera.ZoomScale + parent.Camera.PointOnScreen.Y),
                    (int)(Size.Y * parent.Camera.ZoomScale), (int)(Size.Y * parent.Camera.ZoomScale)),
                    src, color * ((float)(color.A) / 255));
            }
        }
	}

}