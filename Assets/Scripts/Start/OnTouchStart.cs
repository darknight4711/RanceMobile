using UnityEngine;
using System.Collections;

public class OnTouchStart : MonoBehaviour {

	void Update() {
		if (Input.touchCount > 0 ) {
			Application.LoadLevel("Story");
		}
	}
}
