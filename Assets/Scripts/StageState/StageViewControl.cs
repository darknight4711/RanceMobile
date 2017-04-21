using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StageViewControl : MonoBehaviour {
    public const string MainCanvasName = "Canvas";
    public const string SkillCanvasName = "SkillCanvas";

    StageStateManager stageManager;

    [SerializeField]
    string stageText;
    [SerializeField]
    string charaData;
    [SerializeField]
    string nextScene;

    [SerializeField]
    Canvas canvas;
    [SerializeField]
    Text stageNum;
    [SerializeField]
    FinalTextControl finalText;
    [SerializeField]
    BattleViewControl battleViewControl;
    [SerializeField]
    RectTransform[] charaPanelsPosition = new RectTransform[5];

    void Start() {
        stageManager = new StageStateManager();
        stageManager.initialStage(this, stageText, charaData);
        createCharaPanels();
    }

    private void createCharaPanels() {
        for (int i = 0; i < stageManager.CharaList.Count; i++) {
            GameObject newCharaPanel = (GameObject)Instantiate(Resources.Load("CharaPanel"), new Vector3(0, 0, 0), Quaternion.identity);
            newCharaPanel.transform.SetParent(canvas.transform, false);
            CharaPanelControl cpc = newCharaPanel.GetComponentInChildren<CharaPanelControl>();
            cpc.initiate(charaPanelsPosition, i, BattleView.Manager, stageManager.CharaList[i]);
        }

        BattleView.Manager.setChara(stageManager.CharaList);
    }

    public void showFinalText(string text) {
        finalText.setText(text);
        finalText.showText();
    }

    public virtual void showOrHideStageNumText(bool showOrHide, string text=null) {
        if (text != null) {
            StageNum.text = text;
        }
        if (showOrHide) {
            StageNum.transform.SetAsLastSibling();
            StageNum.enabled = true;
        } else {
            StageNum.enabled = false;
        }
    }

    // Update is called once per frame
    void Update() {
        stageManager.update();
    }



    /*getter*/
    public string NextScene {
        get {
            return nextScene;
        }
    }

    public BattleViewControl BattleView {
        get {
            return battleViewControl;
        }
    }

    public Text StageNum {
        get {
            return stageNum;
        }
    }

    
}
