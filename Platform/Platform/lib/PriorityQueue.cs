using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.lib {

	class PriorityQueue< T > {

		private List< T > queue;
		private readonly Comparer< T > comparer;

		public int Count {
			get {
				return queue.Count;
			}
			private set {
			}
		}

		public PriorityQueue( Comparer< T > comparer ) {
			this.queue = new List< T >();
			this.comparer = comparer;
		}

		public void Enqueue( T element ) {
			queue.Add( element );
			InsertionSort();
		}

		public T Dequeue() {
			T element = queue[ queue.Count - 1 ];
			queue.RemoveAt( queue.Count - 1 );
			return element;
		}

		private void InsertionSort() {
			for ( int index = 1; index <= queue.Count - 1; index++ ) {

				int i = index;
				T temp = queue[ i ];

				while ( i > 0 && comparer.Compare( queue[ i - 1 ], temp ) < 0) {
					queue[ i ] = queue[ i - 1 ];
					i--;
				}
				queue[ i ] = temp;
			}
		}

	}

}