using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class MonsterPanelControl : MonoBehaviour {
	private Monster monster;

    [SerializeField]
	private Image icon;
    [SerializeField]
    private HPControl HPLine;
    [SerializeField]
    private Image attackedImage;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private MoveForwardAnimation moveForwardAnimation;
    [SerializeField]
    private DeadAnimation deadAnimation;

    private bool inPlayAttackedAnimation;

    public void setMonster(Monster value, bool playShowUp=false) {
        monster = value;
        monster.View = this;
        setIcon();
        updateHP();
        playShowUpAnimation();
    }

	private void setIcon () {
        if (monster == null) {
            Sprite sprite = Resources.Load<Sprite>("none");
            icon.sprite = sprite;
        } else {
            Sprite sprite = Resources.Load<Sprite>(monster.Info.Name);
            icon.sprite = sprite;
            LightIcon();
        }
	}

    public void updateHP() {
        HPLine.MaxHP = monster.HP;
        HPLine.CurrentHP = monster.CurrentHP;
    }

    /*player function*/

    public bool isShowUpFinish() {
        if (monster == null)
            return true;
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterShow")) {
            HPLine.setVisible(true);
            return true;
        } else {
            return false;
        }
    }

    public void playShowUpAnimation() {
        icon.transform.localPosition = new Vector2(120, 180);
        HPLine.setVisible(false);
        Animator.SetTrigger("monsterShowUp");
    }

    public bool isPlayAttackedFinish() {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterSlash")) {
            inPlayAttackedAnimation = true;
        } else if (inPlayAttackedAnimation) {
            inPlayAttackedAnimation = false;
            return true;
        }
        return false;
    }

    public void playAttackedAnimation() {
        Animator.SetTrigger("playAttacked");
    }

    public bool isMoveForwardAnimationFinish() {
        return moveForwardAnimation.isPlayAnimationFinish();
    }

    public void playMoveForwardAnimation(int targetIndex) {
        moveForwardAnimation.playMoveForwardAnimation(Monster.CurrentPlace, targetIndex);
        Monster.CurrentPlace = targetIndex;
    }

    public bool isPlayDeadFinish() {
        if (!monster.Dead)
            return true;
        return deadAnimation.isPlayAnimationFinish();
    }

    public void playDeadAnimation() {
        deadAnimation.playDeadAnimation(icon);
    }

    /*variable properrty*/

    public Monster Monster {
		get {
			return monster;
		}
	}

    public Animator Animator {
        get {
            return animator;
        }
    }

    public Image AttackedImage {
        get {
            return attackedImage;
        }

        set {
            attackedImage = value;
        }
    }

    /*light-deslight function*/

    public void LightIcon() {
        icon.color = Color.white;
    }

    public void DelightIcon() {
        icon.color = Color.grey;
    }

    
}
