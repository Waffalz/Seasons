using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.Graphics;

using System.Drawing;


namespace Platform.World {

	public class Entity {

		protected Texture2D texture;
		protected Microsoft.Xna.Framework.Rectangle texSrc;

		internal protected Map parent;
		protected Vector2 position;
		protected Vector2 size;
		protected Vector2 velocity;
		protected bool gravity;
		protected bool anchored;
		protected bool solid;
		protected Microsoft.Xna.Framework.Color color;


		public virtual Map Parent {
			get {
				return parent;
			}

			set {
				if ( parent != null ) {
					parent.RemoveEntity( this );
				}
				if ( value != null ) {
					value.AddEntity( this );
				}
				parent = value;
			}
		}

		public Vector2 Position {
			get {
				return position;
			}
			set {
				position = value;
			}
		}
		public Vector2 Size {
			get {
				return size;
			}
			set {
				size = value;
			}
		}
		public virtual Vector2 Velocity {
			get {
				return velocity;
			}
			set {
				velocity = value;
			}
		}
		public bool Gravity {
			get {
				return gravity;
			}
			set {
				gravity = value;
			}
		}
		public bool Anchored {
			get {
				return anchored;
			}
			set {
				anchored = value;
			}
		}
		public bool Solid {
			get {
				return solid;
			}
			set {
				solid = value;
			}
		}
		public Texture2D Texture {
			get {
				return texture;
			}
			set {
				texture = value;
			}
		}
		public Microsoft.Xna.Framework.Rectangle SourceRect {
			get {
				return texSrc;
			}
			set {
				texSrc = value;
			}
		}
		public Microsoft.Xna.Framework.Color Color {
			get {
				return color;
			}
			set {
				color = value;
			}
		}

		public Entity() {
			position = new Vector2();
			size = new Vector2();
			velocity = new Vector2();
			gravity = true;
			solid = true;
			texture = null;
			texSrc = new Microsoft.Xna.Framework.Rectangle();
			color = Microsoft.Xna.Framework.Color.White;
		}

		public virtual void OnCollide( Entity other ) {

		}
		public bool Collides( Entity other ) {

			RectangleF r1 = getRekt();
			RectangleF r2 = other.getRekt();

			return r1.IntersectsWith( r2 );
		}

		public void Destroy() {
			Parent = null;

		}

		public RectangleF getRekt() {
			return new RectangleF( this.position.X - this.size.X / 2, -( this.position.Y + this.size.Y / 2 ), this.size.X, this.size.Y );
		}

		public virtual void Update( float gameTime ) {

		}

	}

}