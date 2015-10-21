using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeadAnimation : MonoBehaviour {
    private Image icon;
    private float speed = 50;
    [SerializeField]
    private bool playAnimation = false;

    public bool isPlayAnimationFinish() {
        return !playAnimation;
    }

    public void playDeadAnimation(Image icon) {
        this.icon = icon;
        playAnimation = true;
    }

    // Update is called once per frame
    void Update() {
        if (playAnimation) {
            float step = speed * Time.deltaTime;
            if (step < icon.color.a) {
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, icon.color.a - step);
            } else {
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0);
                playAnimation = false;
            }
        }
    }
}
