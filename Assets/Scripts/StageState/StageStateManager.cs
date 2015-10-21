using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class StageStateManager {
    StageState showText;
    StageState showEnemy;
    StageState battle;
    StageState allMonsterDead;
    StageState allPlayerDead;

    Text stageNum;
    FinalTextControl finalText;
    BattleFlow battleFlow;

    private StageState currentState;
    private StageInfo info;
    private int stageCount;

    public StageStateManager() {
        showText = new ShowText();
        showEnemy = new ShowEnemy();
        battle = new Battle();
        allMonsterDead = new AllMonsterDead();
        allPlayerDead = new AllPlayerDead();
    }

    public void initialStage(string stageName, string charaData) {
        info = new StageInfo(stageName);

        List<Chara> charaList = CharaDataParser.ParseCharaData(charaData);

        GameObject charaPanel = GameObject.Find("CharaPanel");
        for (int i = 0; i < charaList.Count; i++) {
            GameObject newCharaPanel = GameObject.Instantiate(charaPanel);
            newCharaPanel.transform.SetParent(canvas.transform, false);
            CharaPanelControl cpc = newCharaPanel.GetComponentInChildren<CharaPanelControl>();
            initiateCharaPanel(cpc, i, charaList);
        }

        battleFlow.CharaPanelList = charaPanelList;

        CurrentState = ShowText;
    }

    public void update() {
        CurrentState.update(this);
    }

    public void showFinalText(string text) {
        finalText.setText(text);
        finalText.showText();
    }

    /*Component getter*/
    public Text StageNum {
        get {
            return stageNum;
        }
    }

    public BattleFlow BattleFlow {
        get {
            return battleFlow;
        }
    }

    /*private variable getter*/
    internal StageState CurrentState {
        get {
            return currentState;
        }

        set {
            currentState = value;
            currentState.StateStart(this);
        }
    }

    public StageInfo Info {
        get {
            return info;
        }
    }

    public int StageCount {
        get {
            return stageCount;
        }
    }

    public void stageCountPlusOne() {
        stageCount++;
    }

    /*Each StageState getter*/
    internal StageState ShowText {
        get {
            return showText;
        }
    }

    internal StageState ShowEnemy {
        get {
            return showEnemy;
        }
    }

    internal StageState Battle {
        get {
            return battle;
        }
    }

    internal StageState AllMonsterDead {
        get {
            return allMonsterDead;
        }
    }

    internal StageState AllPlayerDead {
        get {
            return allPlayerDead;
        }
    }
}
