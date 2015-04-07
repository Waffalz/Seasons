using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Platform.Mobs {

	interface Behavior {
		void Behave( float timeDif );
	}

	public enum MoveDirection {
		Up,
		Down,
		Left,
		Right
	}

}