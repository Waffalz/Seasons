using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.Graphics;

namespace Platform.World {

	public class Tile {

		public const float TILE_WIDTH = 10;// in wubs
		public const int VARS = 5;
		public const int TILE_TEX_WIDTH = 64;

		public string TileSheetName;
		public int TileSheetIndex;


		public Tile( string sheetName, int sheetIndex ) {
			TileSheetName = sheetName;
			TileSheetIndex = sheetIndex;
		}

		/* Old tile loading stuff
		public static Tile getTile(char from){
			switch(from){
				case '.':
					return null;

				case 'B':
					return getVariation("Platforms", 0);
				case 'G':
					return getVariation("Platforms", VARS);
				case 'O':
					return getVariation("Platforms", VARS*2);
				case 'R':
					return getVariation("Platforms", VARS*3);
				case 'Y':
					return getVariation("Platforms", VARS*4);

				case 'b':
					return getVariation("Blocks", 0);
				case 'g':
					return getVariation("Blocks", VARS);
				case 'o':
					return getVariation("Blocks", VARS*2);
				case 'r':
					return getVariation("Blocks", VARS*3);
				case 'y':
					return getVariation("Blocks", VARS*4);

				default:
					return null;
			}
		}
		*/

		public static Tile getVariation( string sheetName, int colorRow ) {
			return new Tile( sheetName, colorRow + Game1.rand.Next( VARS ) );
		}

	}

}