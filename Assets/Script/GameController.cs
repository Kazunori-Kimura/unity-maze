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
	/// The player.
	/// </summary>
	public GameObject player;

	/// <summary>
	/// The ball prefab.
	/// </summary>
	public GameObject ballPrefab;

	/// <summary>
	/// The ball count.
	/// </summary>
	public int ballCount;

	/// <summary>
	/// The rows.
	/// </summary>
	public int rows;

	/// <summary>
	/// The columns.
	/// </summary>
	public int columns;

	/// <summary>
	/// The score label.
	/// </summary>
	public UnityEngine.UI.Text scoreLabel;

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

		// Unity-chanを配置
		player.transform.position = startPoint + new Vector3(width/2, 0, width/2);

		// ボールを配置
		var list = Utility.Shuffle(rooms);
		int count = 0;
		foreach (var room in list) {
			Debug.Log (room.roomNo);
			if (this.PutBall (room)) {
				count++;
				if (count > ballCount) {
					// ボール配置完了
					break;
				}
			}
		}
	}

	/// <summary>
	/// Puts the wall.
	/// </summary>
	/// <param name="wall">Wall.</param>
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

	/// <summary>
	/// Puts the ball.
	/// </summary>
	/// <returns><c>true</c>, if ball was put, <c>false</c> otherwise.</returns>
	/// <param name="room">Room.</param>
	bool PutBall(Room room) {
		if (room.row == 0 && room.col == 0) {
			// playerと同じ座標なのでキャンセル
			return false;
		}
		// 部屋の中心
		float width = wallPrefab.transform.localScale.z;
		Vector3 position = new Vector3 (room.col * width + (width / 2), 0.7f, room.row * width + (width / 2)) + startPoint;

		Debug.Log (string.Format("Ball Position = {0}", position));

		// ballの配置
		Instantiate(ballPrefab, position, Quaternion.identity);

		return true;
	}
	
	// Update is called once per frame
	void Update () {
		int count = GameObject.FindGameObjectsWithTag ("Ball").Length;
		scoreLabel.text = count.ToString ();
	}
}
