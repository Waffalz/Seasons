using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.Graphics;

using System.Drawing;


namespace Platform.World
{
    public class Entity
    {

        protected Texture2D texture;
        protected Microsoft.Xna.Framework.Rectangle sourceRect;

        internal protected Map parent;
        private Vector2 position;
        private Vector2 size;
        protected Vector2 velocity;
        protected bool gravity;
        protected bool anchored;
        protected bool solid;
        protected Microsoft.Xna.Framework.Color color;

        protected Vector2 oldPos;

        private AnimationState animState;

        protected RectangleF rect;

        public RectangleF Rekt
        {
            get { return rect; }
            
        }
        
        public virtual Map Parent
        {
            get { return parent; }
            set{
                if (parent != null){
                    parent.RemoveEntity(this);
                }
                if (value != null){
                    value.AddEntity(this);
                }
                parent = value;
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value;
            rect = new RectangleF(position.X - size.X / 2, -(position.Y + size.Y / 2), size.X, size.Y);
            }
        }
        public Vector2 Size
        {
            get { return size; }
            set { size = value;
            rect = new RectangleF(position.X - size.X / 2, -(position.Y + size.Y / 2), size.X, size.Y);
            }
        }
        public virtual Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public bool Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }
        public bool Anchored
        {
            get { return anchored; }
            set { anchored = value; }
        }
        public bool Solid
        {
            get { return solid; }
            set { solid = value; }
        }
        public Texture2D Texture{
            get { return texture; }
            set { texture = value; }
        }
        public Microsoft.Xna.Framework.Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }
        public Microsoft.Xna.Framework.Color Color
        {
            get { return color; }
            set { color = value; }
        }
        public AnimationState AnimState
        {
            get { return animState; }
            set {
                if (animState != null) {
                    animState.adaptee = null;
                }
                animState = value;
                if (value != null) {
                    value.adaptee = this;
                }
            }
        }

        public Entity()
        {
            position = new Vector2();
            size = new Vector2();
            velocity = new Vector2();
            gravity = true;
            solid = true;
            texture = null;
            sourceRect = new Microsoft.Xna.Framework.Rectangle();
            color = Microsoft.Xna.Framework.Color.White;
            oldPos = position;
            rect = new RectangleF(this.position.X - this.size.X / 2, -(this.position.Y + this.size.Y / 2), this.size.X, this.size.Y);
        }

        public virtual void OnCollide(Entity other){

        }

        public bool Collides(Entity other)
        {
            return rect.IntersectsWith(other.rect);
        }

        public void Destroy()
        {
            Parent = null;

        }

        public virtual void Update(GameTime gameTime)
        {
            float timeDifference = (float)gameTime.ElapsedGameTime.TotalSeconds;

            oldPos = position;
            
            if (Anchored == false) {//don't handle movement if ent is anchored
                position = position + timeDifference * velocity;
            }
            if (gravity == true) { //handle gravity on ent if gravity is true
                velocity = new Vector2(velocity.X, velocity.Y + parent.Gravity * timeDifference);
            }

            if (animState != null) {
                animState.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            if (texture != null) {
                spriteBatch.Draw(texture, new Microsoft.Xna.Framework.Rectangle(
                    (int)((position.X - size.X / 2 - parent.Camera.Position.X) * parent.Camera.ZoomScale + parent.Camera.PointOnScreen.X),
                    (int)(-(position.Y + size.Y / 2 - parent.Camera.Position.Y) * parent.Camera.ZoomScale + parent.Camera.PointOnScreen.Y),
                    (int)(size.X * parent.Camera.ZoomScale), (int)(size.Y * parent.Camera.ZoomScale)),
                    sourceRect, color);
            }
        }


        public virtual void CorrectCollisionPosition(List<Entity> ents)
        {
            foreach (Entity tilent in ents) { //Tile collisions
                //System.Drawing.RectangleF re = tilent.getRekt();
                //re.Size += new System.Drawing.SizeF(0,WALL_BUFFER);
                //re = new System.Drawing.RectangleF(tilent.Position.X - re.Size.Width / 2, -(tilent.Position.Y + re.Size.Height), re.Size.Width, re.Size.Height);
                if (Collides(tilent)) {//if ent collides with a tileent
                    if (oldPos.Y + size.Y / 2 <= tilent.Position.Y - tilent.Size.Y / 2) {// if ent is under tile

                        Position = new Vector2(position.X, tilent.Position.Y - (tilent.Size.Y + size.Y) / 2);
                        velocity = new Vector2(velocity.X, 0);
                    }
                    if (oldPos.X - size.X / 2 >= tilent.Position.X + tilent.Size.X / 2) {// if ent is to the right of tile 

                        Position = new Vector2(tilent.Position.X + (tilent.Size.X + size.X) / 2, position.Y);
                        velocity = new Vector2(0, velocity.Y);
                        OnCollide(tilent);
                        continue;
                    }
                    if (oldPos.X + size.X / 2 <= tilent.Position.X - tilent.Size.X / 2) { // if ent is to the left of tile

                        Position = new Vector2(tilent.Position.X - (tilent.Size.X + size.X) / 2, position.Y);
                        velocity = new Vector2(0, velocity.Y);
                        OnCollide(tilent);
                        continue;
                    }
                    if (oldPos.Y - size.Y / 2 >= tilent.Position.Y + tilent.Size.Y / 2) { //if ent is over tile
                        Position = new Vector2(position.X, tilent.Position.Y + (tilent.Size.Y + size.Y) / 2);
                        velocity = new Vector2(velocity.X, 0);
                    }
                    
                    OnCollide(tilent);
                }
            }
        }
    }
}
