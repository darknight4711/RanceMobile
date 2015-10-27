using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillCanvasController : MonoBehaviour {
    public Image icon;
    public Text value;
    public Button[] skillButtons = new Button[4];
    public Text[] skillDescription = new Text[4];

    public Canvas skillCanvas, battleCanvas;

    private CharaPanelControl currentCPC;

    public void setChara(CharaPanelControl cpc) {
        currentCPC = cpc;

        Chara chara = cpc.ResponseChara;
        Sprite sprite = Resources.Load<Sprite>(chara.Name + "_icon");
        icon.sprite = sprite;

        value.text = chara.Name + "\n" + chara.CurrentLevel + "\n" + chara.HP + "\n" + chara.ATK + "\n" + chara.DEF;

        for (int i = 0; i < 4; i++) {
            Skill s = chara.getSkill(i);
            if (s != null) {
                skillButtons[i].GetComponentInChildren<Text>().text = chara.getSkill(i).Name + " (" + chara.getSkillCD(i) + "/" + chara.getSkill(i).Cd + ")";
                skillDescription[i].text = s.Description;
            } else {
                skillButtons[i].GetComponentInChildren<Text>().text = "";
                skillButtons[i].interactable = false;
                skillDescription[i].text = "";
            }
        }
    }

    public void setSkill(int index) {
        currentCPC.ResponseChara.setSkill(index);
        skillCanvas.enabled = false;
        battleCanvas.enabled = true;
    }
}
