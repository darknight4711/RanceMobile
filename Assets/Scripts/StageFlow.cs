using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageFlow : MonoBehaviour {
    public enum StageState { Start, ShowStageText, ShowEnemy, Battle, FinishThisStage, PlayerDead }

    public string stageText;
    public string charaData;

    public Canvas canvas;
    public Text stageNum;
    public FinalTextControl finalText;

    public BattleFlow battleFlow;

    public StageInfo stageInfo;

    public StageState stageState;

    private int stageCount;

    public List<CharaPanelControl> charaPanelList = new List<CharaPanelControl>();

    float showStageNumTime;
    void Start() {
        stageInfo = new StageInfo(stageText);

        TextAsset txt = Resources.Load("Stage/"+charaData) as TextAsset;
        string dialogText;
        string[] lines;
        int txtCounter = 0;
        dialogText = txt.text;
        lines = dialogText.Split('\n');

        List<Chara> charaList = new List<Chara>();

        while (txtCounter < lines.Length) {
            string name = lines[txtCounter].Trim();
            txtCounter++;
            int currentLevel = int.Parse(lines[txtCounter].Trim());
            txtCounter++;
            Chara chara = new Chara(name, currentLevel);

            for (int i = 0; i < 4 && lines[txtCounter].Trim() != ""; i++) {
                chara.setSkill(chara.info.getSkill(lines[txtCounter].Trim()), i);
                txtCounter++;
            }

            charaList.Add(chara);
            txtCounter++;
        }

        GameObject charaPanel = GameObject.Find("CharaPanel");
        CharaPanelControl firstcpc = charaPanel.GetComponentInChildren<CharaPanelControl>();
        initiateCharaPanel(firstcpc, 0, charaList);
        for (int i = 1; i < charaList.Count; i++) {
            GameObject newCharaPanel = GameObject.Instantiate(charaPanel);
            newCharaPanel.transform.SetParent(canvas.transform, false);
            CharaPanelControl cpc = newCharaPanel.GetComponentInChildren<CharaPanelControl>();
            initiateCharaPanel(cpc, i, charaList);
        }

        battleFlow.CharaPanelList = charaPanelList;

        /*for (int i = 0; i < 3; i++) {
            monsterPanelList[i].setMonster(null);
        }*/

        stageState = StageState.Start;
        changeStageState();
    }

    private void initiateCharaPanel(CharaPanelControl cpc, int i, List<Chara> charaList) {
        charaPanelList.Add(cpc);
        cpc.Place = i;
        cpc.OrderManager = battleFlow;
        cpc.ResponseChara = charaList[i];
    }

    private void battleEnd(BattleFlow.BattleEndType endType) {
        if (endType == BattleFlow.BattleEndType.AllMonsterDead) {
            stageState = StageState.FinishThisStage;
            changeStageState();
        } else if (endType == BattleFlow.BattleEndType.AllPlayerDead) {
            stageState = StageState.PlayerDead;
            changeStageState();
        }
    } 

    public void changeStageState() {
        switch (stageState) {
            case StageState.Start:
                stageState = StageState.ShowStageText;
                changeStageState();
                break;
            case StageState.ShowStageText:
                stageNum.transform.SetAsLastSibling();
                stageNum.text = (stageCount + 1) + "/" + stageInfo.StageNum;
                stageNum.enabled = true;
                showStageNumTime = Time.time;
                break;
            case StageState.ShowEnemy:
                stageNum.enabled = false;
                List<Monster> mInfo = stageInfo.MonsterList(stageCount);
                battleFlow.setMonsters(mInfo);
                break;
            case StageState.Battle:
                battleFlow.setBattleEndCallBack(battleEnd);
                battleFlow.setBattleState(BattleFlow.BattleState.InitialAttack);
                break;
            case StageState.FinishThisStage:
                stageCount++;
                if (stageCount < stageInfo.StageNum) {
                    stageState = StageState.ShowStageText;
                    changeStageState();
                } else {
                    showFinalText("Mission Complete!!");
                }
                break;
            case StageState.PlayerDead:
                showFinalText("You Dead");
                break;
        }
    }

    private void showFinalText(string text) {
        finalText.setText(text);
        finalText.showText();
    }

    // Update is called once per frame
    void Update() {
        switch (stageState) {
            case StageState.ShowStageText:
                if (Time.time - showStageNumTime > 2) {
                    stageState = StageState.ShowEnemy;
                    changeStageState();
                }
                break;
            case StageState.ShowEnemy:
                if (battleFlow.isMonsterShowUpFinish()) {
                    stageState = StageState.Battle;
                    changeStageState();
                }
                break;
            case StageState.FinishThisStage:
            case StageState.PlayerDead:
                if (Input.touchCount > 0) {
                    Application.LoadLevel("Start");
                }
                break;
        }
    }
}
