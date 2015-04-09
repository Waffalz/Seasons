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
        public const int TILE_TEX_WIDTH = 64;
        public const int VARS = 5;

        public string TileSheetName;
        public int TileSheetRow;
        public int TileSheetCol;

        
        public Tile(string sheetName, int sheetRowIndex, int sheetColIndex)
        {
            TileSheetName = sheetName;
            TileSheetRow = sheetRowIndex;
            TileSheetCol = sheetColIndex;
        }

        public static Tile getVariation(string sheetName, int colorRow, int colorCol)
        {
            return new Tile(sheetName, colorRow, colorCol);
        }
    }
}
