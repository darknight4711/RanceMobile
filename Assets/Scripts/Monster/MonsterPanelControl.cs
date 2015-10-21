using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MonsterPanelControl : MonoBehaviour {
	private Monster monster;

	public Image icon;
	public HPControl HPLine;
	public Image AttackedImage;
	public Animator animator;
    public MoveForwardAnimation moveForwardAnimation;
    public DeadAnimation deadAnimation;
    [SerializeField]
    private int place;

    private bool inPlayAttackedAnimation;
	private bool inPlayDeadAnimation;

	public Monster Monster {
		get {
			return monster;
		}
	}

    public int Place {
        get {
            return place;
        }
    }

    public void setPlace(int place) {
        this.place = place;
    }

    public void setMonster(Monster value, bool playShowUp=false) {
        monster = value;
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
            icon.transform.localPosition = new Vector2(120, 180);
            LightIcon();
        }
	}

	public bool IsShowUpFinish () {
        if (monster == null)
            return true;
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterShow")) {
            HPLine.setVisible(true);
            return true;
		} else {
			return false;
		}
	}

	public void playShowUpAnimation () {
        HPLine.setVisible(false);
        animator.SetTrigger("monsterShowUp");
    }

	public bool IsPlayAttackedFinish () {
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterSlash")) {
			inPlayAttackedAnimation = true;
		} else if (inPlayAttackedAnimation){
			inPlayAttackedAnimation = false;
			return true;
		}
		return false;
	}

	public void playAttackedAnimation () {
		animator.SetTrigger("playAttacked");
	}

    public void playMoveForwardAnimation(int targetIndex) {
        moveForwardAnimation.playMoveForwardAnimation(place, targetIndex);
        place = targetIndex;
    }

    public bool isMoveForwardAnimationFinish() {
        return moveForwardAnimation.isPlayAnimationFinish();
    }

    public void updateHP () {
        HPLine.MaxHP = monster.HP;
        HPLine.CurrentHP = monster.CurrentHP;
	}

	public bool IsPlayDeadFinish () {
        if (!monster.Dead)
            return true;
		
		return deadAnimation.isPlayAnimationFinish();
	}

	public bool checkDead () {
		if (monster.checkDead()) {
            playDeadAnimation();
            return true;
        }
        return false;
	}

    public void playDeadAnimation() {
        deadAnimation.playDeadAnimation(icon);
    }

    public void LightIcon() {
        icon.color = Color.white;
    }

    public void DelightIcon() {
        icon.color = Color.grey;
    }

    //public bool hasMonster() {
    //    return (monster == null);
    //}

    public void attackHP(int damage, bool guard=true) {
        if (guard) {
            HPLine.CurrentHP -= damage - monster.DEF;
        } else {
            HPLine.CurrentHP -= damage;
        }
        monster.CurrentHP = HPLine.CurrentHP;
        HPLine.updateHPLine();
    }
}
