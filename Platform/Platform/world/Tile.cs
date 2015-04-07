using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.GameFlow;
using Platform.Graphics;

namespace Platform.World
{
    public class Tile
    {
        
        public const float TILE_WIDTH = 10;// in wubs
        public const int VARS = 5;
        public const int TILE_TEX_WIDTH = 64;

        public string TileSheetName;
        public int TileSheetIndex;

        
        public Tile(string sheetName, int sheetIndex)
        {
            TileSheetName = sheetName;
            TileSheetIndex = sheetIndex;
        }

        public static Tile getVariation(string sheetName, int colorRow)
        {
            return new Tile(sheetName, colorRow + Game1.CurrentGame.Rand.Next(VARS));
        }

        

    }
}
