using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Platform.World;
using Platform.GameFlow;

namespace Platform.Graphics
{
    class LookCamera : Camera
    {
        private float maxWorldExtend; //maximum amount camera will pan out from origin
        private float maxMouseExtend;

        private Vector2 origin;

        public LookCamera(Map p):base(p)
        {
            maxWorldExtend = 70;
            maxMouseExtend = 200;
        }

       /* public override void Update(GameTime gameTime)
        {
            if (parent.Player != null) {
                origin = parent.Player.Position;
            }

            Point mousePlace = new Point(Game1.CurrentGame.MouseInput.X, Game1.CurrentGame.MouseInput.Y);
            
            Point windowMiddle = new Point(Game1.CurrentGame.Window.ClientBounds.Width/2,Game1.CurrentGame.Window.ClientBounds.Height/2);

            Vector2 dif = new Vector2(-(windowMiddle.X - mousePlace.X), windowMiddle.Y - mousePlace.Y);

            if (dif.LengthSquared() > 0) {

                Vector2 jif = dif;
                jif.Normalize();

                if (dif.LengthSquared() > Math.Pow(maxMouseExtend, 2)) {
                    dif = jif * maxMouseExtend;
                }

                position = origin + maxWorldExtend * jif * (float)Math.Sqrt(dif.LengthSquared() / Math.Pow(maxMouseExtend, 2));

            }
            else {
                position = origin;
            }
        }
        */
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            /*
            if (parent != null) {


                for (int i = 0; i < parent.BackList.Count; i++) {//draw background

                    BackgroundObject o = parent.BackList[i];

                    if (o.Image != null) {
                        spriteBatch.Draw(o.Image,
                            new Rectangle(
                            (int)(((o.Position.X - o.Size.X / 2) - position.X) * zoomScale * o.Depth + PointOnScreen.X),
                            (int)(-((o.Position.Y + o.Size.Y / 2) - position.Y) * zoomScale * o.Depth + PointOnScreen.Y),
                            (int)(o.Size.X * zoomScale), (int)(o.Size.Y * zoomScale)),
                            o.SrcRect, o.Col);
                    }

                }

                for (int y = 0; y < parent.Tiles.GetLength(0); y++) {
                    for (int x = 0; x < parent.Tiles.GetLength(1); x++) {
                        Tile til = parent.Tiles[y, x];
                        if (til != null) {
                            spriteBatch.Draw(Game1.CurrentGame.Textures[til.TileSheetName],
                                new Rectangle(
                                    (int)((x * Tile.TILE_WIDTH - position.X) * zoomScale + PointOnScreen.X),
                                    (int)(-((y + 1) * Tile.TILE_WIDTH - position.Y) * zoomScale + PointOnScreen.Y),
                                    (int)(Tile.TILE_WIDTH * zoomScale), (int)(Tile.TILE_WIDTH * zoomScale)),
                                new Rectangle(
                                    (til.TileSheetRowIndex % Tile.VARS) * Tile.TILE_TEX_WIDTH,
                                    (til.TileSheetRowIndex / Tile.VARS) * Tile.TILE_TEX_WIDTH,
                                    Tile.TILE_TEX_WIDTH, Tile.TILE_TEX_WIDTH),
                                Color.White);

                        }
                    }
                }

                foreach (Entity ent in Parent.Entities) {
                    ent.Draw(gameTime, spriteBatch);
                }

                foreach (Particle p in Parent.Particles) {
                    if (p.Texture != null) {
                        p.Draw(gameTime, spriteBatch);
                    }
                }
            }*/
        }

    }
}
