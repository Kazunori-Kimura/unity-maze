using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	void OnTriggerEnter(Collider hit) {
		if (hit.CompareTag ("Player")) {
			// 自身を破棄
			Destroy (gameObject);
		}
	}
}
