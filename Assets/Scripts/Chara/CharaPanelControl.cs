using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharaPanelControl : MonoBehaviour, IPointerClickHandler {
	public Image icon;
	public Button skillButton;
	public Text order;
	public Text cd;
    public HPControl hp;
    private OrderManager<CharaPanelControl> orderManager;
    [SerializeField]
    private bool ordered = false;
    private bool dead = false;
    private int place;
    private bool interactive = false;

    private Chara responseChara;
	public RectTransform[] panels = new RectTransform[5];


    public Chara ResponseChara {
		get {
			return responseChara;
		}
		set {
			responseChara = value;
            SetHP(value.CurrentHP, value.HP);
			updateIcon ();
			updateSkill();
            updatePosition();
        }
	}

    public int Place {
        get {
            return place;
        }

        set {
            place = value;
            updatePosition();
        }
    }

    public OrderManager<CharaPanelControl> OrderManager {
        get {
            return orderManager;
        }

        set {
            orderManager = value;
        }
    }

    public bool Interactive {
        get {
            return interactive;
        }
        set {
            interactive = value;
            if (value) {
                skillButton.interactable = true;
            } else {
                skillButton.interactable = false;
                //setOrder(0);
            }
        }
    }

    public bool Dead {
        get {
            return dead;
        }
    }

    private void updateIcon () {
        Sprite sprite = Resources.Load<Sprite>(responseChara.Name + "_icon");
        icon.sprite = sprite;
        LightIcon();
    }

    private void updatePosition() {
        RectTransform panelTransform = transform as RectTransform;
        panelTransform.position = panels[Place].position;
    }

    public void updateSkill () {
		skillButton.GetComponentInChildren<Text> ().text = responseChara.getSkill(responseChara.currentSkill).Name;
		cd.text = responseChara.skillCD [responseChara.currentSkill].ToString();
        if (responseChara.skillCD[responseChara.currentSkill] != 0) {
            DelightIcon();
        }
	}

	public void setOrder (int value) {
		if (value == 0) {
			responseChara.state = false;
			order.enabled = false;
            ordered = false;
		} else {
			responseChara.state = true;
			order.enabled = true;
			order.text = value.ToString ();
            ordered = true;
        }
		updateSkill ();
	}

	public void turnPre() {
		for (int i=0; i<4; i++) {
			responseChara.skillCD [i] = responseChara.skillCD [i] - 1 < 0 ? 0 : responseChara.skillCD [i] - 1;
		}
        LightIcon();
        updateSkill ();
	}
	
	public void turnPost() {
        DelightIcon();
        updateSkill ();
	}

	public void startInteraction() {
        setOrder(0);
        Interactive = true;
	}

	public void startBattle() {
        Interactive = false;
	}

    public void OnPointerClick(PointerEventData eventData) {
        if (interactive == false || responseChara.skillCD[responseChara.currentSkill] != 0)
            return;
        if (ordered) {
            OrderManager.removeOrder(this);
            setOrder(0);
        } else {
            OrderManager.addOrder(this);
            int order = OrderManager.getOrder(this);
            setOrder(order + 1);
        }
    }

    public void LightIcon() {
        icon.color = Color.white;
    }

    public void DelightIcon() {
        icon.color = Color.gray;
    }

    private void SetHP(int currentHP, int maxHP) {
        if (currentHP > maxHP)
            currentHP = maxHP;
        hp.MaxHP = maxHP;
        hp.CurrentHP = currentHP;
        hp.updateHPLine();
    }

    public void attackHP(int damage, bool guard = true) {
        if (guard)
            hp.CurrentHP -= damage - responseChara.DEF;
        else
            hp.CurrentHP -= damage;
        responseChara.CurrentHP = hp.CurrentHP;
        hp.updateHPLine();
    }

    public void setSkill(int index) {
        responseChara.currentSkill = index;
        updateSkill();
        if (responseChara.skillCD[responseChara.currentSkill] == 0) {
            LightIcon();
        }
    }

    public bool checkDead() {
        if (ResponseChara.CurrentHP == 0) {
            print(ResponseChara.Name + " Dead");
            dead = true;
            setOrder(0);
            DelightIcon();
        }
        return dead;
    }
}
