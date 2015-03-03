using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platform.World;

namespace Platform.Graphics
{
    public class BackgroundObject : IComparable
    {

        private Texture2D image;
        private Rectangle srcRect;
        private Vector2 position;
        private Vector2 size;
        private float depth;
        private Color col;

        public Color Col
        {
            get { return col; }
            set { col = value; }
        }
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }
        public Rectangle SrcRect
        {
            get { return srcRect; }
            set { srcRect = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }
        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public BackgroundObject()
        {
            image = null;
            srcRect = new Rectangle();
            position = new Vector2();
            size = new Vector2();
            depth = 0;
            col = Color.White;
        }

        public int CompareTo(object e)
        {
            if (e == null){
                return 1;
            }

            BackgroundObject other = e as BackgroundObject;
            if (other != this)
            {
                return this.depth > other.depth ? 1 : this.depth < other.depth ? -1 : 0;
            }
            else
            {
                throw new ArgumentException("Object is not BackgroundObject");
            }

        }

    }
}
