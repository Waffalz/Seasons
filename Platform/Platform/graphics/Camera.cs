using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.World;

namespace Platform.Graphics
{
    abstract class Camera
    {
        protected Map parent;
        protected int zoomScale;

        private Point pointOnScreen;
        protected Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Point PointOnScreen
        {
            get { return pointOnScreen; }
            set { pointOnScreen = value; }
        }

        public Map Parent
        {
            get { return parent; }
            set
            {
                if (parent != null){
                    parent.Cam = null;
                }
                parent = value;
            }
        }
        public int ZoomScale
        {
            get { return zoomScale; }
            set { zoomScale = value; }
        }

        public Camera (Map p){
            parent = p;
            zoomScale = 5;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public Vector2 PositionFromScreen(Point p)
        {
            Point dist = new Point(p.X - PointOnScreen.X, -(p.Y - PointOnScreen.Y));
            Vector2 mousePlace = new Vector2(Position.X + dist.X / zoomScale, Position.Y + dist.Y / zoomScale);
            return mousePlace;
        }
    }



    class DefaultCamera : Camera
    {


       
        public DefaultCamera(Map p) : base(p)
        {
            position = new Vector2();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //draw tiles
            if (parent != null)
            {
                if (parent.Player != null){
                    position = parent.Player.Position;
                }

                for (int i = parent.BackList.Count - 1; i >= 0; i-- )
                {
                    BackgroundObject o = parent.BackList[i];

                }

                for (int y = 0; y < parent.Tiles.GetLength(0); y++)
                {
                    for (int x = 0; x < parent.Tiles.GetLength(1); x++)
                    {
                        Tile til = parent.Tiles[y, x];
                        if (til != null)
                        {

                            spriteBatch.Draw(Game1.tileSheets[til.TileSheetName],
                                new Rectangle(
                                    (int)((x * Tile.TILE_WIDTH - position.X) * zoomScale + PointOnScreen.X),
                                    (int)(-((y + 1) * Tile.TILE_WIDTH - position.Y) * zoomScale + PointOnScreen.Y),
                                    (int)(Tile.TILE_WIDTH * zoomScale), (int)(Tile.TILE_WIDTH * zoomScale)),
                                new Rectangle(
                                    (til.TileSheetIndex % Tile.VARS) * Tile.TILE_TEX_WIDTH,
                                    (til.TileSheetIndex / Tile.VARS) * Tile.TILE_TEX_WIDTH,
                                    Tile.TILE_TEX_WIDTH, Tile.TILE_TEX_WIDTH),
                                Color.White);

                        }
                    }
                }

                foreach (Entity ent in Parent.Entities)
                {
                    if (ent.Texture != null)
                    {
                        spriteBatch.Draw(ent.Texture, new Rectangle(
                            (int)((ent.Position.X - ent.Size.X / 2 - position.X) * zoomScale + PointOnScreen.X),
                            (int)(-(ent.Position.Y + ent.Size.Y / 2 - position.Y) * zoomScale + PointOnScreen.Y),
                            (int)(ent.Size.X * zoomScale), (int)(ent.Size.Y * zoomScale)), ent.SourceRect, ent.Color);
                    }

                }
                
                foreach (Particle p in Parent.Particles)
                {
                    //TODO: implement particle graphics
                    if (p.Texture != null)
                    {
                        spriteBatch.Draw(p.Texture, new Rectangle(
                            (int)((p.Position.X - p.Size.X / 2 - position.X) * zoomScale + PointOnScreen.X),
                            (int)(-(p.Position.Y + p.Size.Y / 2 - position.Y) * zoomScale + PointOnScreen.Y),
                            (int)(p.Size.X * zoomScale), (int)(p.Size.Y * zoomScale)), p.SourceRect, p.Color);
                    }
                }
                



            }

        }
    }

}
