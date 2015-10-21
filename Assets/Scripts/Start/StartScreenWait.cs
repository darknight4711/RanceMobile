using UnityEngine;
using System.Collections;

public class StartScreenWait : MonoBehaviour {
	private float startTime;

	public OnTouchStart pressEvent;


	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 && Time.time - startTime > 1) {
            Application.LoadLevel("Story");
        }
	}
}
