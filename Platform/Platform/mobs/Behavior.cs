using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Platform.logger;

namespace Platform.mobs {
	interface Behavior {
		void Behave( float timeDif );

	}
	public enum MoveDirection {
		Up,
		Down,
		Left,
		Right,
		None
	}

}