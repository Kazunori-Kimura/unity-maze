using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// click event
	/// </summary>
	public void Clicked () {
		Debug.Log ("click!");
		// 現在のシーン
		int index = SceneManager.GetActiveScene().buildIndex;
		// 再読込	
		SceneManager.LoadScene(index);
	}
}
