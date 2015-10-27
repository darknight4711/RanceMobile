using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class CharaPanelControl : MonoBehaviour, IPointerClickHandler {
    [SerializeField]
	private Image icon;
    [SerializeField]
    private Button skillButton;
    [SerializeField]
    private Text order;
    [SerializeField]
    private Text cd;
    [SerializeField]
    private HPControl hp;

    private int place;
    private bool interactive = false;

    private Chara responseChara;
    private OrderManager<Chara> orderManager;

    public void initiate(RectTransform[] rt, int i, OrderManager<Chara> manager, Chara c) {
        place = i;
        RectTransform panelTransform = transform as RectTransform;
        panelTransform.position = rt[Place].position;
        OrderManager = manager;
        ResponseChara = c;
        c.View = this;

        skillButton.onClick.AddListener(skillBuuttonAction);
    }

    private void skillBuuttonAction() {
        Canvas skillCanvas = GameObject.Find(StageViewControl.SkillCanvasName).GetComponent<Canvas>();
        Canvas mainCanvas = GameObject.Find(StageViewControl.MainCanvasName).GetComponent<Canvas>();
        Assert.IsNotNull(skillCanvas, "Can't not find SkillCanvas!");
        Assert.IsNotNull(mainCanvas, "Can't not find MainCanvas!");
        skillCanvas.GetComponent<SkillCanvasController>().setChara(this);
        skillCanvas.enabled = true;
        mainCanvas.enabled = false;
    }

    private void updateIcon () {
        Sprite sprite = Resources.Load<Sprite>(responseChara.Name + "_icon");
        icon.sprite = sprite;
        LightIcon();
    }

    public void updateSkill () {
		skillButton.GetComponentInChildren<Text> ().text = responseChara.getSkill(responseChara.CurrentSkill).Name;
		cd.text = responseChara.getSkillCD (responseChara.CurrentSkill).ToString();
        if (responseChara.getSkillCD(responseChara.CurrentSkill) != 0) {
            DelightIcon();
        }
	}

    public void updateOrder() {
        if (responseChara.Order == 0) {
            order.enabled = false;
        } else {
            order.enabled = true;
            order.text = responseChara.Order.ToString();
        }
        updateSkill();
    }

    public void updateHP() {
        hp.CurrentHP = responseChara.CurrentHP;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (interactive == false || responseChara.getSkillCD(responseChara.CurrentSkill) != 0)
            return;
        if (ResponseChara.Order != 0) {
            OrderManager.removeOrder(responseChara);
            ResponseChara.Order = 0;
        } else {
            OrderManager.addOrder(responseChara);
            int order = OrderManager.getOrder(responseChara);
            ResponseChara.Order = order + 1;
        }
    }

    public void LightIcon() {
        icon.color = Color.white;
    }

    public void DelightIcon() {
        icon.color = Color.gray;
    }

    /*some variable property*/

    public int Place {
        get {
            return place;
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
            }
        }
    }

    public Chara ResponseChara {
        get {
            return responseChara;
        }
        set {
            responseChara = value;
            SetHP(value.CurrentHP, value.HP);
            updateIcon();
            updateSkill();
        }
    }

    private void SetHP(int currentHP, int maxHP) {
        if (currentHP > maxHP)
            currentHP = maxHP;
        hp.MaxHP = maxHP;
        hp.CurrentHP = currentHP;
    }

    public OrderManager<Chara> OrderManager {
        get {
            return orderManager;
        }

        set {
            orderManager = value;
        }
    }

    
}
