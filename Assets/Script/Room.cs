using System;

namespace AssemblyCSharp
{
	public class Room
	{
		public Room (int roomNo, int col, int row)
		{
			this.roomNo = roomNo;
			this.clusterNo = roomNo;
			this.col = col;
			this.row = row;

			this.top = null;
			this.right = null;
			this.bottom = null;
			this.left = null;
		}

		/// <summary>
		/// Gets the room no.
		/// </summary>
		/// <value>The room no.</value>
		public int roomNo { get; }

		/// <summary>
		/// Gets or sets the cluster no.
		/// </summary>
		/// <value>The cluster no.</value>
		public int clusterNo { get; set; }

		/// <summary>
		/// Gets the col.
		/// </summary>
		/// <value>The col.</value>
		public int col { get; }

		/// <summary>
		/// Gets the row.
		/// </summary>
		/// <value>The row.</value>
		public int row { get; }

		/// <summary>
		/// Gets or sets the top wall.
		/// </summary>
		/// <value>The top.</value>
		public Wall top { get; set; }
		/// <summary>
		/// Gets or sets the right wall.
		/// </summary>
		/// <value>The right.</value>
		public Wall right { get; set; }
		/// <summary>
		/// Gets or sets the bottom wall.
		/// </summary>
		/// <value>The bottom.</value>
		public Wall bottom { get; set; }
		/// <summary>
		/// Gets or sets the left wall.
		/// </summary>
		/// <value>The left.</value>
		public Wall left { get; set; }
	}
}

