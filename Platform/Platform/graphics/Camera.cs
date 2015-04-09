using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.GameFlow;
using Platform.World;

namespace Platform.Graphics
{
    public abstract class Camera
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
            set{
                if (parent != null){
                    parent.Camera = null;
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

        public virtual void Update(GameTime gameTime)
        {
            if (parent.Player != null) {
                position = parent.Player.Position;
            }
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //draw tiles
            if (parent != null){
                

                for (int i = 0; i < parent.BackList.Count; i++ ){//draw background
                
                    BackgroundObject o = parent.BackList[i];

                    if (o.Image != null){
                        spriteBatch.Draw(o.Image,
                            new Rectangle(
                            (int)(((o.Position.X - o.Size.X / 2) - position.X) * zoomScale *o.Depth + PointOnScreen.X),
                            (int)(-((o.Position.Y + o.Size.Y / 2) - position.Y) * zoomScale *o.Depth + PointOnScreen.Y),
                            (int)(o.Size.X * zoomScale), (int)(o.Size.Y * zoomScale)),
                            o.SrcRect, o.Col);
                    }

                }

                for (int y = 0; y < parent.Tiles.GetLength(0); y++){
                    for (int x = 0; x < parent.Tiles.GetLength(1); x++){
                        Tile til = parent.Tiles[y, x];
                        if (til != null){
                            spriteBatch.Draw(Game1.CurrentGame.Textures[til.TileSheetName],
                                //destination rectangle
                                //*NOTE: -8 to positions and +16 to dimensions are to make the player appear to stand in the grass on blocks instead of on top of the grass
                                new Rectangle(
                                    (int)((x * Tile.TILE_WIDTH - position.X) * zoomScale + PointOnScreen.X - 8),
                                    (int)(-((y + 1) * Tile.TILE_WIDTH - position.Y) * zoomScale + PointOnScreen.Y - 8),
                                    (int)(Tile.TILE_WIDTH * zoomScale + 16), (int)(Tile.TILE_WIDTH * zoomScale + 16)),
                                //source rectangle
                                new Rectangle(
                                    (til.TileSheetRow) * Tile.TILE_TEX_WIDTH,
                                    (til.TileSheetCol) * Tile.TILE_TEX_WIDTH,
                                    Tile.TILE_TEX_WIDTH, Tile.TILE_TEX_WIDTH),
                                Color.White);

                        }
                    }
                }

                foreach (Entity ent in Parent.Entities){
                    ent.Draw(gameTime, spriteBatch);
                }
                
                foreach (Particle p in Parent.Particles){
                    if (p.Texture != null){
                        p.Draw(gameTime, spriteBatch);
                    }
                }
            }
        }
    }
}
