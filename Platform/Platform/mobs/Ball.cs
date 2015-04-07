using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Platform.World;
using Microsoft.Xna.Framework;

namespace Platform.Mobs {
	public class Ball : Entity {

		private Entity creator;
		int damage;

		float lifeLeft;

		public float LifeLeft {
			get {
				return lifeLeft;
			}
			set {
				lifeLeft = value;
			}
		}
		public Entity Creator {
			get {
				return creator;
			}
			set {
				creator = value;
			}
		}
		public int Damage {
			get {
				return damage;
			}
			set {
				damage = value;
			}
		}

		public Ball()
			: base() {
			texture = Game1.entSheets[ "Player" ];
			texSrc = texture.Bounds;
			damage = 10;
			size = new Vector2( 5, 5 );
			lifeLeft = 10;
		}

		public override void OnCollide( Entity other ) {
			base.OnCollide( other );
			if ( !( other == creator || ( other is Ball && ( ( Ball )other ).creator == creator ) ) ) {
				if ( other is Mob ) {
					( ( Mob )other ).Damage( damage );
				}
				for ( int i = 0; i < size.X * 2; i++ ) {
					Particle poi = new Particle( ( float )2, ( float )size.X / ( float )1.5 );
					poi.Color = Color.SkyBlue;
					poi.Position = Position;
					double rAngle = MathHelper.ToRadians( Game1.rand.Next( 0, 360 ) );
					double speed = Game1.rand.Next( 20, 40 );
					poi.Velocity = new Vector2( ( float )Math.Round( Math.Cos( rAngle ) * speed ), ( float )Math.Round( Math.Sin( rAngle ) * speed ) );
					poi.ColorSpeed = new Vector3( Game1.rand.Next( -10, 10 ), Game1.rand.Next( -10, 10 ), Game1.rand.Next( -10, 10 ) );
					Parent.AddParticle( poi );
				}
				parent.RemoveEntity( this );
			}
		}
		public override void Update( float gameTime ) {
			base.Update( gameTime );
			lifeLeft -= gameTime;
			if ( lifeLeft < 0 ) {
				parent.RemoveEntity( this );
			}
		}

	}

}