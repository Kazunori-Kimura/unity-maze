using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public static class Utility
	{
		/// <summary>
		/// リストをシャッフルする
		/// </summary>
		/// <param name="list">List.</param>
		public static List<Wall> Shuffle(List<Wall> list) {
			var rand = new Random ();

			for (int i = list.Count - 1; i > 0; i--) {
				int j = rand.Next (i + 1);
				Wall item = list [i];
				list [i] = list [j];
				list [j] = item;
			}

			return list;
		}
	}
}

