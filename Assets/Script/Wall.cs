using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	/// <summary>
	/// Wall.
	/// </summary>
	public class Wall
	{
		public Wall (int index, int startCol, int startRow, int endCol, int endRow, Direction direction, bool isBreakable)
		{
			this.index = index;
			this.startCol = startCol;
			this.startRow = startRow;
			this.endCol = endCol;
			this.endRow = endRow;
			this.direction = direction;
			this.isBreakable = isBreakable;
			this.isBroken = false;
		}

		public int index { get; set; }
		public int startCol { get; set; }
		public int startRow { get; set; }
		public int endCol { get; set; }
		public int endRow { get; set; }

		public Direction direction { get; }

		public bool isBreakable { get; }

		public bool isBroken { get; set; }
	}
}

