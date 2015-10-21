using UnityEngine;
using System.Collections;

public class MoveForwardAnimation : MonoBehaviour {
    private Vector3 start;
    private Vector3 target;
    private float speed = 500;
    [SerializeField]
    private bool playAnimation = false;

    public bool isPlayAnimationFinish() {
        return !playAnimation;
    }

    public void playMoveForwardAnimation(int startIndex, int positionIndex) {
        start = new Vector3(startIndex * 30, 95 + startIndex * 30, 0);
        target = new Vector3(positionIndex * 30, 95 + positionIndex * 30, 0);
        playAnimation = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (playAnimation) {
            float step = speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, step);
            if (transform.localPosition == target) {
                playAnimation = false;
            }
        }
	}
}
