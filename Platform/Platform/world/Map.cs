using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.mobs;
using Platform.graphics;
using Platform.gameflow;
using Platform.logger;

namespace Platform.world {
	public class Map {
		public const int MAP_BOUNDS = 256;//max number of tiles in each dimension
		public const float WALL_BUFFER = 1;

		public const float MAX_WUBS = MAP_BOUNDS * Tile.TILE_WIDTH;

		private Tile[,] tiles;
		private List<Entity> entList;
		private List<Entity> addEList;
		private List<Entity> removeEList;

		private List<Particle> partList;
		private List<Particle> addPList;
		private List<Particle> removePList;

		private Camera cam;
		private Player player;
		private float gAccel; //default -150

		private List<Entity> tileEnts;//since I don't wanna make tile to entity collisions, create an entity based off of tiles to check for entity collision
        private List<Mob> mobList;

		private List<BackgroundObject> backList;

		public Tile[ , ] Tiles {
			get {
				return tiles;
			}
		}
        public List<Entity> TileEntities
        {
            get {
                return tileEnts;
            }
        }
		public List<Entity> Entities {
			get {
				return entList;
			}
		}
		public List<Particle> Particles {
			get {
				return partList;
			}
		}
        public List<Mob> Mobs
        {
            get { return mobList; }
        }
		public float Gravity {
			get {
				return gAccel;
			}
			set {
				gAccel = value;
			}
		}
		public Player Player {
			get {
				return player;
			}
			set {
				player = value;
			}
		}
		public Camera Camera {
			get {
				return cam;
			}
			set {
				cam = value;
			}
		}
		public List<BackgroundObject> BackList {
			get {
				return backList;
			}
			set {
				backList = value;
			}
		}

		public Map() {
			tiles = new Tile[ MAP_BOUNDS, MAP_BOUNDS ];
			entList = new List<Entity>();
			addEList = new List<Entity>();
			removeEList = new List<Entity>();
			partList = new List<Particle>();
			addPList = new List<Particle>();
			removePList = new List<Particle>();
			gAccel = -150;
			cam = new DefaultCamera( this );
			backList = new List<BackgroundObject>();
            mobList = new List<Mob>();

			tileEnts = new List<Entity>();

			for ( int i = 0; i < 400; i++ ) {

				BackgroundObject boi = new BackgroundObject();
				boi.Depth = ( float )Game1.CurrentGame.Rand.Next( 1, 100 ) / 100;
				boi.Position = new Vector2( Game1.CurrentGame.Rand.Next( -100, 1000 ), Game1.CurrentGame.Rand.Next( -100, 500 ) ) * ( 2 - boi.Depth );
				boi.Size = new Vector2( Game1.CurrentGame.Rand.Next( 5, 10 ) ) * ( boi.Depth / 2 + ( float ).5 );
				boi.Col = Color.White;
				boi.Image = Game1.CurrentGame.Textures[ "Blocks" ];
				boi.SrcRect = new Rectangle(
								(2) * Tile.TILE_TEX_WIDTH,
								(4) * Tile.TILE_TEX_WIDTH,
								Tile.TILE_TEX_WIDTH, Tile.TILE_TEX_WIDTH );
				backList.Add( boi );

			}
			backList.Sort();

		}

		public bool IsValidEntityPosition( int posX, int posY ) {
			return false;
		}

		public void AddEntity( Entity toAdd ) {
			addEList.Add( toAdd );
		}
		public void RemoveEntity( Entity toRemove ) {
			removeEList.Add( toRemove );
		}

		public void AddParticle( Particle toAdd ) {
			addPList.Add( toAdd );
		}
		public void RemoveParticle( Particle toRemove ) {
			removePList.Add( toRemove );
		}

		public void Tick( GameTime gameTime ) {
			//the method that updates all stuff going on in the map, calling to it will update the map by 1 tick (ideally 60 times per sec)

			float timeDifference = ( float )gameTime.ElapsedGameTime.TotalSeconds;

            //add entities in queue
			foreach ( Entity ent in addEList ) {
				entList.Add( ent );
				ent.parent = this;
                if (ent is Mob) {
                    mobList.Add((Mob)ent);
                }
			}
			addEList.Clear();

            //add particles in queue
			foreach ( Particle p in addPList ) {
				partList.Add( p );
				p.parent = this;
			}
			addPList.Clear();

			foreach ( Entity ent in entList ) { //iterate over each entity in entlist to calculate all happenings
				Vector2 oldPos = ent.Position;//old position of ent in case we ever need to revert to it

				if ( ent is Behavior ) { //if ent implements the Behavior interface, do that stuff
					( ( Behavior )ent ).Behave( timeDifference );
				}

				ent.Update( gameTime );//make the ent do stuff

                if (ent.Interactable) {
                    foreach (Entity other in entList) { //check for interactions between entities in entlis
                        if (ent != other && other.Interactable) {
                            if (ent.Collides(other)) {
                                ent.OnCollide(other);
                                other.OnCollide(ent);
                            }
                        }
                    }
                }
                
				if ( ent.Position.Y < -50 ) {//remove ent if below the kill level
					if ( ent is Mob ) {
						ent.Position = ( ( Mob )ent ).previousPosition;
						ent.Velocity = new Vector2( 0, 0 );
						( ( Mob )ent ).WalkVelocity = new Vector2();
						( ( Mob )ent ).Damage( 20 );
					} else {
						RemoveEntity( ent );
					}
				}


			}

			foreach ( Particle p in partList ) {//call to each particle's specific behaviors
				p.Update( gameTime );
			}

			foreach ( Entity ent in removeEList ) {//removes entities in queue
				if ( entList.Contains( ent ) ) {
					entList.Remove( ent );
					ent.parent = null;
					if ( ent is Player && player == ent ) {
						player = null;
					}
                    if (ent is Mob) {
                        mobList.Remove((Mob)ent);
                    }
				}
			}
			removeEList.Clear();

			foreach ( Particle p in removePList ) {//removes entities in queue
				if ( partList.Contains( p ) ) {
					partList.Remove( p );
					p.parent = null;
				}
			}
			removePList.Clear();

            

			cam.Update( gameTime );
		}

        //initializes tileEnts with TileEntities based on each tile
		public void UpdateTileWorld() {
			tileEnts = new List<Entity>();
			for ( int y = 0; y < tiles.GetLength( 0 ); y++ ) {
				for ( int x = 0; x < tiles.GetLength( 1 ); x++ ) {
					if ( tiles[ y, x ] != null ) {
						TileEntity lent = new TileEntity();
						lent.Size = new Vector2( Tile.TILE_WIDTH, Tile.TILE_WIDTH );
						lent.Position = new Vector2( x * Tile.TILE_WIDTH + Tile.TILE_WIDTH / 2, y * Tile.TILE_WIDTH + Tile.TILE_WIDTH / 2 );
						lent.Anchored = true;
						lent.Gravity = false;
						lent.Solid = true;
						tileEnts.Add( lent );
					}
				}
			}
		}

		//Loading the map into an array to store the initial locations of tiles, enemies, and the player. I really should have commented this
		public static Map LoadMap2( string pathname ) {
			Map nMap = new Map();
			try {
				using ( StreamReader file = new StreamReader( pathname ) ) {
					Dictionary<char, string> tileKey = new Dictionary<char, string>();

					List<List<char>> rawmap = new List<List<char>>();

					int y = 0;
					while ( !file.EndOfStream ) {
						string line = file.ReadLine();
						if ( line.StartsWith( "<" ) ) {
							while ( !file.EndOfStream ) {

								line = file.ReadLine();
								Console.WriteLine( line );

								if ( line.StartsWith( ">" ) ) {
									line = file.ReadLine();
									break;
								}
								try {
									char[] kLine = line.ToCharArray();
									tileKey.Add( kLine[ 0 ], line.Substring( line.IndexOf( '=' ) + 1 ).Trim() );
								} catch ( Exception o ) {
									Console.WriteLine( "bad file line" );
									Console.WriteLine( o );
								}
							}
						}
						char[] cLine = line.ToCharArray();
						List<char> toAdd = new List<char>();

						for ( int x = 0; x < line.Length; x++ ) {
							char raw = cLine[ x ];

							toAdd.Add( raw );
						}
						rawmap.Add( toAdd );
						y++;
					}
					rawmap.Reverse();
					//
					for ( int y1 = 0; y1 < rawmap.Count; y1++ ) {
						for ( int x1 = 0; x1 < rawmap[ y1 ].Count; x1++ ) {
							//Load tile
							string tileDat = tileKey[ rawmap[ y1 ][ x1 ] ];
							switch ( tileDat ) {
								case "null":
									nMap.Tiles[ y1, x1 ] = null;
									break;
								case "Player":
									Player p = Game1.CurrentGame.Player;
									p.Position = new Vector2( x1 * Tile.TILE_WIDTH + Tile.TILE_WIDTH / 2, y1 * Tile.TILE_WIDTH + p.Size.Y / 2 );
									p.Parent = nMap;
									nMap.Player = p;
									break;
								case "Baddu":
									Baddu b = new Baddu();
									b.Position = new Vector2( x1 * Tile.TILE_WIDTH + Tile.TILE_WIDTH / 2, y1 * Tile.TILE_WIDTH + b.Size.Y / 2 );
									b.Parent = nMap;
									break;
								default:
									try {
										string[] values = tileDat.Split( ',' );
										string type = values[ 0 ];
										int row = Convert.ToInt32( values[ 1 ].Trim() );
										int col = Convert.ToInt32( values[ 2 ].Trim() );
										nMap.Tiles[ y1, x1 ] = Tile.getVariation( type, row, col );
									} catch ( Exception i ) {
										Console.WriteLine( "Tile couldn't be loaded, bad key" );
										Console.WriteLine( i );
									}
									break;
							}

						}
					}
				}
			} catch ( FileNotFoundException e ) {
				Console.WriteLine( "File couldn't be read." );
				Console.WriteLine( e.StackTrace );
			}
			nMap.UpdateTileWorld();
			return nMap;
		}

	}
}