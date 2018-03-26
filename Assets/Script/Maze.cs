using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Maze
	{
		private List<Room> rooms = new List<Room>();
		private Dictionary<string, Room> roomMap = new Dictionary<string, Room>();
		private List<Wall> walls = new List<Wall>();
		private Dictionary<string, HashSet<int>> clusters = new Dictionary<string, HashSet<int>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyCSharp.Maze"/> class.
		/// </summary>
		/// <param name="rows">Rows.</param>
		/// <param name="cols">Cols.</param>
		public Maze (int rows, int cols)
		{
			for (int row = 0; row < rows; row++) {
				for (int col = 0; col < cols; col++) {
					var room = this.AddRoom (row, col);

					if (row == 0) {
						// 上端なら上Wallを作成
						var wall = this.AddWall(col, row, col+1, row, Direction.Yoko, false);
						room.top = wall;
					}
					if (col == 0) {
						// 左端なら左wallを作成
						var wall = this.AddWall(col, row, col, row+1, Direction.Tate, false);
						room.left = wall;
					}
					// 下Wall
					var bottom = this.AddWall(col, row+1, col+1, row+1, Direction.Yoko, row != (rows - 1));
					room.bottom = bottom;
					// 右wall
					var right = this.AddWall(col+1, row, col+1, row+1, Direction.Tate, col != (cols - 1));
					room.right = right;
				}
			}
		}

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <returns>The key.</returns>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		private string GetKey(int row, int col) {
			return string.Format ("{0}_{1}", col, row);
		}

		/// <summary>
		/// 部屋を追加
		/// </summary>
		/// <returns>The room.</returns>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		private Room AddRoom(int row, int col) {
			int index = this.rooms.Count;
			var room = new Room (index, col, row);

			this.rooms.Add (room);
			this.roomMap.Add (this.GetKey(row, col), room);
			var set = new HashSet<int> ();
			set.Add (index);
			this.clusters.Add (index.ToString(), set);

			return room;
		}

		/// <summary>
		/// 壁を追加
		/// </summary>
		/// <returns>The wall.</returns>
		/// <param name="x1">The first x value.</param>
		/// <param name="y1">The first y value.</param>
		/// <param name="x2">The second x value.</param>
		/// <param name="y2">The second y value.</param>
		/// <param name="direction">Direction.</param>
		/// <param name="isBreakable">If set to <c>true</c> is breakable.</param>
		private Wall AddWall(int x1, int y1, int x2, int y2, Direction direction, bool isBreakable){
			int index = this.walls.Count;
			var wall = new Wall (index, x1, y1, x2, y2, direction, isBreakable);
			this.walls.Add (wall);

			return wall;
		}

		/// <summary>
		/// Gets the room by position.
		/// </summary>
		/// <returns>The room by position.</returns>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		private Room GetRoomByPosition(int row, int col) {
			return this.roomMap [this.GetKey(row, col)];
		}

		/// <summary>
		/// 壁に隣接する部屋を取得する
		/// </summary>
		/// <returns>The adjacent rooms.</returns>
		/// <param name="wall">Wall.</param>
		private List<Room> GetAdjacentRooms(Wall wall) {
			var list = new List<Room> ();

			var room1 = this.GetRoomByPosition (wall.startRow, wall.startCol);
			list.Add (room1);

			switch (wall.direction) {
			case Direction.Tate:
				if (wall.startCol != 0) {
					var room = this.GetRoomByPosition (wall.startRow, wall.startCol - 1);
					list.Add (room);
				}
				break;
			case Direction.Yoko:
				if (wall.startRow != 0) {
					var room = this.GetRoomByPosition (wall.startRow - 1, wall.startCol);
					list.Add (room);
				}
				break;
			}

			return list;
		}

		/// <summary>
		/// 壁が破壊可能かどうか
		/// </summary>
		/// <param name="wall">Wall.</param>
		private bool IsBreakableWall(Wall wall) {
			// 外壁は破壊不可
			if (!wall.isBreakable) {
				return false;
			}

			// 隣接する部屋のclusterが一致していれば破壊不可
			var list = this.GetAdjacentRooms(wall);
			if (list.Count > 1) {
				return list [0].clusterNo != list [1].clusterNo;
			}

			// なんか想定外
			return false;
		}

		/// <summary>
		/// 壁を破壊した時に隣接する部屋のclusterを同じにする
		/// </summary>
		/// <param name="wall">Wall.</param>
		private void MergeCluster(Wall wall) {
			var list = this.GetAdjacentRooms (wall);
			if (list.Count > 1) {
				int clusterNo = list [0].clusterNo; //この番号に一致させる
				var clusterNoSet = this.clusters[clusterNo.ToString()];

				int removeNo = list [1].clusterNo; //削除するclusterNo
				var removeSet = this.clusters[removeNo.ToString()];
				foreach (int roomNo in removeSet) {
					// roomのclusterNo更新
					this.rooms[roomNo].clusterNo = clusterNo;
					// HashSetに追加
					clusterNoSet.Add(roomNo);
				}

				this.clusters.Remove (removeNo.ToString());
			}
		}

		/// <summary>
		/// 迷路を生成
		/// </summary>
		public List<Room> Generate() {
			// 壁の配列をシャッフル
			var list = Utility.Shuffle(this.walls);
			foreach (var wall in list) {
				if (this.IsBreakableWall(wall)) {
					// 壁を破壊
					wall.isBroken = true;
					// clusterを更新
					this.MergeCluster(wall);

					if (this.clusters.Count == 1) {
						break;
					}
				}
			}

			return this.rooms;
		}
	}
}

