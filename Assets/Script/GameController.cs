using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class GameController : MonoBehaviour {
	/// <summary>
	/// The ground.
	/// </summary>
	public GameObject ground;
	/// <summary>
	/// The wall prefab.
	/// </summary>
	public GameObject wallPrefab;

	/// <summary>
	/// The rows.
	/// </summary>
	public int rows;

	/// <summary>
	/// The columns.
	/// </summary>
	public int columns;

	private Vector3 startPoint;

	// Use this for initialization
	void Start () {
		// 地面のサイズを設定
		float width = wallPrefab.transform.localScale.z;
		ground.transform.localScale = new Vector3(width * columns, 1, width * rows);
		// 起点
		startPoint = new Vector3 (-1 * ground.transform.localScale.x / 2, 0, -1 * ground.transform.localScale.z / 2);

		// 迷路を自動生成
		var maze = new Maze (rows, columns);
		var rooms = maze.Generate ();

		// 壁を配置
		foreach (var room in rooms) {
			PutWall (room.top);
			PutWall (room.left);
			PutWall (room.right);
			PutWall (room.bottom);
		}
	}

	void PutWall(Wall wall) {
		if (wall != null && !wall.isBroken) {
			// 位置
			float width = wallPrefab.transform.localScale.z;
			Vector3 position = new Vector3 (wall.startCol * width, 0, wall.startRow * width + (width / 2)) + startPoint;
			// 回転
			Quaternion q = Quaternion.identity;

			if (wall.direction == Direction.Yoko) {
				position -= new Vector3 (-1 * width/2, 0, width/2);
				q = Quaternion.Euler (0, 90.0f, 0); //90度回転
			}

			Instantiate (wallPrefab, position, q);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
