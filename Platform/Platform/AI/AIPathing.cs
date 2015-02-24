using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.World;
using Platform.lib;

namespace Platform.AI {

	public struct pathNode_t {
		private readonly int x, y, cost;

		public int X {
			get {
				return x;
			}
			private set {
			}
		}
		public int Y {
			get {
				return y;
			}
			private set {
			}
		}
		public int Cost {
			get {
				return cost;
			}
			private set {
			}
		}

		public pathNode_t( int x, int y, int cost ) {
			this.x = x;
			this.y = y;
			this.cost = cost;
		}
		public pathNode_t( int x, int y ) {
			this.x = x;
			this.y = y;
			this.cost = 0;
		}
	}

	class AIPathing {

		public static pathNode_t[] AStar( ref Map map, pathNode_t start, pathNode_t end, int maxRadius ) {
			if ( maxRadius == 0 ) {
				return new pathNode_t[ 0 ];
			}

			PriorityQueue< pathNode_t > frontier = new PriorityQueue< pathNode_t >( new PathNodeCostComparer() );
			frontier.Enqueue( start );
			Dictionary< pathNode_t, pathNode_t > destinationToSourceMap = new Dictionary< pathNode_t, pathNode_t >();
			destinationToSourceMap[ start ] = start;
			Dictionary< pathNode_t, int > pathCosts = new Dictionary< pathNode_t, int >();
			pathCosts[ start ] = 0;

			while ( frontier.Count != 0 ) {
				pathNode_t curNode = frontier.Dequeue();

				if ( curNode.Equals( end ) ) {
					break;
				}

				pathNode_t[] neighbors = getAllNeighborPositions( curNode );

				for ( int index = 0; index < neighbors.Length; index++ ) {
					pathNode_t neighbor = neighbors[ index ];

					if ( !map.IsValidEntityPosition( neighbor.X, neighbor.Y ) ) {
						int newCost = pathCosts[ curNode ] + map.TileCost( curNode.X, curNode.Y, neighbor.X, neighbor.Y );

						if ( !pathCosts.ContainsKey( neighbor ) ) {
							pathCosts[ neighbor ] = newCost;
						} else if ( newCost < pathCosts[ neighbor ] ) {
							pathCosts[ neighbor ] = newCost;
						}

						if ( !destinationToSourceMap.ContainsKey( neighbor ) ) {
							destinationToSourceMap[ neighbor ] = curNode;
						}
					}
				}
			}

			return new pathNode_t[ 0 ];
		}

		private static pathNode_t[] getAllNeighborPositions( pathNode_t center ) {
			pathNode_t[] neighbors = new pathNode_t[ 8 ];
			neighbors[ 0 ] = new pathNode_t( center.X - 1,	center.Y - 1 );
			neighbors[ 1 ] = new pathNode_t( center.X,		center.Y - 1 );
			neighbors[ 2 ] = new pathNode_t( center.X + 1,	center.Y - 1 );
			neighbors[ 3 ] = new pathNode_t( center.X - 1,	center.Y );
			neighbors[ 4 ] = new pathNode_t( center.X + 1,	center.Y );
			neighbors[ 5 ] = new pathNode_t( center.X - 1,	center.Y + 1 );
			neighbors[ 6 ] = new pathNode_t( center.X,		center.Y + 1 );
			neighbors[ 7 ] = new pathNode_t( center.X + 1, center.Y + 1 );
			return neighbors;
		}

	}

	private class PathNodeCostComparer : Comparer< pathNode_t > {

		public PathNodeCostComparer() {
		}

		int Compare( pathNode_t x, pathNode_t y ) {
			if ( x.Cost > y.Cost ) {
				return 1;
			} else if ( x.Cost < y.Cost ) {
				return -1;
			} else {
				return 0;
			}
		}

	}

}