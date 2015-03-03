using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


using Platform.World;
using Platform.Graphics;

namespace Platform.GameFlow
{
    public class CombatGame : GameScreen
    {
        public const int MAX_SCROLL = 10, MIN_SCROLL = 1;

        Map world;
        bool paused;

        public Map World
        {
            get { return world; }
            set { world = value; }
        }
        public bool Paused
        {
            get { return paused; }
            set { paused = value; }
        }

        public CombatGame (){
            //TODO: initialize world

            world = Map.LoadMap2(@"Content/maps/Level02.txt");
            world.Camera.PointOnScreen = new Point(Game1.CurrentGame.Window.ClientBounds.Width / 2, Game1.CurrentGame.Window.ClientBounds.Height / 2);
            
            for (int i = 0; i < 300; i++) {

                BackgroundObject boi = new BackgroundObject();
                int ro = Game1.CurrentGame.Rand.Next(0, 25);
                boi.Depth = (float)Game1.CurrentGame.Rand.Next(1, 100) / 100;
                boi.Position = new Vector2(Game1.CurrentGame.Rand.Next(-100, 500), Game1.CurrentGame.Rand.Next(-100, 500))*(2-boi.Depth);
                boi.Size = new Vector2(Game1.CurrentGame.Rand.Next(5, 10))*(boi.Depth/2 + (float).5);
                boi.Col = Color.White;
                boi.Image = Game1.CurrentGame.Textures["Blocks"];
                boi.SrcRect = new Rectangle(
                                (ro % Tile.VARS) * Tile.TILE_TEX_WIDTH,
                                (ro / Tile.VARS) * Tile.TILE_TEX_WIDTH,
                                Tile.TILE_TEX_WIDTH, Tile.TILE_TEX_WIDTH);
                world.BackList.Add(boi);

            }
            world.BackList.Sort();


        }

        public override void Update(GameTime gameTime)
        {
            MouseState mus = Game1.CurrentGame.MouseInput;
            MouseState oMus = Game1.CurrentGame.OldMouseInput;
            int scroll = mus.ScrollWheelValue - oMus.ScrollWheelValue;

            if (!paused) {
                world.Tick(gameTime); //update stuff in the Map
            }

            if (scroll < 0){
                world.Camera.ZoomScale = Math.Max(MIN_SCROLL, world.Camera.ZoomScale + scroll / 120);
            }
            if (scroll > 0){
                world.Camera.ZoomScale = Math.Min(MAX_SCROLL, world.Camera.ZoomScale + scroll / 120);
            }


        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            world.Camera.Draw(gameTime, spriteBatch);
        }

    }
}
