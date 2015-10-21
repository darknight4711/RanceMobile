using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

public class BattleFlow : MonoBehaviour, OrderManager<CharaPanelControl> {
    public enum BattleState { InitialAttack, PlayerTurnPre, PlayerTurn, PlayerAttack, PlayerTurnPost, EnemyTurnPre, EnemyTurn, EnemyTurnPost }
    public enum BattleEndType { None, AllMonsterDead, AllPlayerDead }

    public Canvas canvas;
    [SerializeField]
    private BattleState battleState;
    [SerializeField]
    private List<CharaPanelControl> charaOrder = new List<CharaPanelControl>();


    private List<CharaPanelControl> charaPanelList = new List<CharaPanelControl>();
    public List<Monster> monsterList = new List<Monster>();
    public List<MonsterPanelControl> monsterPanelList = new List<MonsterPanelControl>();

    public Animator animator;

    public Button startBattleButton;

    private int currentAttackMonster;

    private bool playMonsterDeadAnimation = false;

    private bool playMonsterMoveForwardAnimation = false;

    public List<CharaPanelControl> CharaPanelList {
        get {
            return charaPanelList;
        }

        set {
            charaPanelList = value;
        }
    }

    public void setBattleState(BattleState bs) {
        battleState = bs;
        changeBattleState();
    }

    public void setBattleState(string bs) {
        battleState = (BattleState) Enum.Parse(typeof(BattleState), bs);
        changeBattleState();
    }

    public delegate void battleEndCallBack(BattleEndType endType);

    battleEndCallBack callBack;

    public void setBattleEndCallBack(battleEndCallBack callBack) {
        this.callBack = callBack;
    }

    public void setMonsters(List<Monster> monsterList) {
        this.monsterList = monsterList;
        for (int i = 0; i < monsterList.Count; i++) {
            GameObject newMonsterPanel = (GameObject)Instantiate(Resources.Load("MonsterPanel"), new Vector3(0,0,0), Quaternion.identity);
            newMonsterPanel.transform.SetParent(canvas.transform, false);
            newMonsterPanel.transform.localPosition = new Vector3(0+i*30, 95+i*30);
            MonsterPanelControl mpc = newMonsterPanel.GetComponentInChildren<MonsterPanelControl>();
            initiateMonsterPanel(mpc, i);
        }
        for (int i = monsterPanelList.Count - 1; i >= 0; i--) {
            monsterPanelList[i].transform.SetAsLastSibling();
        }
    }

    private void initiateMonsterPanel(MonsterPanelControl mpc, int i) {
        monsterPanelList.Add(mpc);
        mpc.setMonster(monsterList[i]);
        mpc.setPlace(i);
    }

    //private void setMonsters() {
    //    int monsterCount=0, panelCount = 0;
    //    while (panelCount < 3) {
    //        if (monsterCount >= monsterList.Count) {
    //            if (monsterPanelList[panelCount].Monster != null) {
    //                monsterPanelList[panelCount].playDeadAnimation();
    //            }
    //            monsterPanelList[panelCount].setMonster(null);
    //            panelCount++;
    //        } else if (monsterList[monsterCount].Dead) {
    //            monsterCount++;
    //        } else if (monsterCount < monsterList.Count) {
    //            if (monsterPanelList[panelCount].Monster != null) {
    //                monsterPanelList[panelCount].playDeadAnimation();
    //            }
    //            monsterPanelList[panelCount].setMonster(monsterList[monsterCount], true);
    //            monsterCount++;
    //            panelCount++;
    //        }
    //    }
    //}

    public bool isMonsterShowUpFinish() {
        bool isShowEnemyFinish = true;
        for (int i = 0; i < monsterPanelList.Count; i++) {
            if (!monsterPanelList[i].IsShowUpFinish())
                isShowEnemyFinish = false;
        }
        return isShowEnemyFinish;

    }

    public bool isMonsterMoveForwardFinish() {
        bool isEnemyMoveFinish = true;
        for (int i = 0; i < monsterPanelList.Count; i++) {
            if (!monsterPanelList[i].isMoveForwardAnimationFinish())
                isEnemyMoveFinish = false;
        }
        return isEnemyMoveFinish;

    }

    public void changeBattleState() {
        StageFlow stageFlow = gameObject.GetComponentInChildren<StageFlow>();
        if (stageFlow.stageState != StageFlow.StageState.Battle)
            return;
        switch (battleState) {
            case BattleState.InitialAttack:
                setBattleState(BattleState.PlayerTurnPre);
                break;
            case BattleState.PlayerTurnPre:
                for (int i = 0; i < CharaPanelList.Count; i++) {
                    CharaPanelControl cpc = CharaPanelList[i];
                    if (cpc.Dead)
                        continue;
                    cpc.turnPre();
                }
                setBattleState(BattleState.PlayerTurn);
                break;
            case BattleState.PlayerTurn:
                startBattleButton.interactable = true;
                for (int i = 0; i < CharaPanelList.Count; i++) {
                    CharaPanelControl cpc = CharaPanelList[i];
                    if (cpc.Dead)
                        continue;
                    cpc.startInteraction();
                }
                break;
            case BattleState.PlayerAttack:
                startBattleButton.interactable = false;
                for (int i = 0; i < CharaPanelList.Count; i++) {
                    CharaPanelControl cpc = CharaPanelList[i];
                    if (cpc.Dead)
                        continue;
                    cpc.startBattle();
                }
                nextCharaAction();
                break;
            case BattleState.PlayerTurnPost:
                for (int i = 0; i < CharaPanelList.Count; i++) {
                    CharaPanelControl cpc = CharaPanelList[i];
                    if (cpc.Dead)
                        continue;
                    cpc.turnPost();
                }
                setBattleState(BattleState.EnemyTurnPre);
                break;
            case BattleState.EnemyTurnPre:
                setBattleState(BattleState.EnemyTurn);
                break;
            case BattleState.EnemyTurn:
                foreach (MonsterPanelControl mpc in monsterPanelList) {
                    mpc.DelightIcon();
                }
                currentAttackMonster = 0;
                nextMonsterAction();
                break;
            case BattleState.EnemyTurnPost:
                setBattleState(BattleState.PlayerTurnPre);
                break;
        }
    }

    void Update() {
        StageFlow stageFlow = gameObject.GetComponentInChildren<StageFlow>();
        if (stageFlow.stageState != StageFlow.StageState.Battle)
            return;
        switch (battleState) {
            case BattleState.PlayerAttack:
                if (playMonsterMoveForwardAnimation) {
                    if (isMonsterMoveForwardFinish()) {
                        playMonsterMoveForwardAnimation = false;
                        if (checkBattleEnd())
                            return;
                        removeCurrentOrderChara();
                        nextCharaAction();
                    }
                } else if (playMonsterDeadAnimation) {
                    bool allDeadAnimationFinish = isAllMonsterDeadAnimationFinish();
                    if (allDeadAnimationFinish) {
                        playMonsterDeadAnimation = false;
                        playMonsterMoveForwardAnimation = true;
                    }
                } else {
                    Chara chara = charaOrder[0].ResponseChara;
                    int index = CharaPanelList.IndexOf(charaOrder[0]);
                    Skill currentPlaySkill = chara.getSkill(chara.currentSkill);
                    if (currentPlaySkill.checkPlaySkillFinish(CharaPanelList, monsterPanelList, index)) {
                        checkCharaDead();
                        if (checkBattleEnd())
                            return;
                        if (checkMonsterDead()) {
                            playMonsterDeadAnimation = true;
                        } else {
                            removeCurrentOrderChara();
                            nextCharaAction();
                        }
                    }
                }
                break;
            case BattleState.EnemyTurn:
                if (playMonsterMoveForwardAnimation) {
                    if (isMonsterMoveForwardFinish()) {
                        playMonsterMoveForwardAnimation = false;
                        if (checkBattleEnd())
                            return;
                        currentAttackMonster++;
                        nextMonsterAction();
                    }
                }
                else if(playMonsterDeadAnimation) {
                    bool allDeadAnimationFinish = isAllMonsterDeadAnimationFinish();
                    if (allDeadAnimationFinish) {
                        playMonsterDeadAnimation = false;
                        playMonsterMoveForwardAnimation = true;
                    }
                } else {
                    if (monsterPanelList[currentAttackMonster].Monster.IsAttackFinish(CharaPanelList, monsterPanelList, currentAttackMonster)) {
                        checkCharaDead();
                        if(checkBattleEnd())
                            return;
                        if (checkMonsterDead()) {
                            playMonsterDeadAnimation = true;
                        } else {
                            monsterPanelList[currentAttackMonster].DelightIcon();
                            currentAttackMonster++;
                            nextMonsterAction();
                        }
                    }
                }
                
                break;
        }
    }

    private void nextMonsterAction() {
        if (currentAttackMonster < monsterPanelList.Count && currentAttackMonster < 3) {
            monsterPanelList[currentAttackMonster].LightIcon();
            MonsterPanelControl enemympc = monsterPanelList[currentAttackMonster];
            enemympc.Monster.startAttack(CharaPanelList, monsterPanelList, currentAttackMonster);
        } else {
            foreach (MonsterPanelControl mpc in monsterPanelList) {
                mpc.LightIcon();
            }
            setBattleState(BattleState.EnemyTurnPost);
        }
    }

    private bool isAllMonsterDeadAnimationFinish() {
        bool allDeadAnimationFinish = true;
        foreach (MonsterPanelControl mpc in monsterPanelList) {
            if (!mpc.IsPlayDeadFinish())
                allDeadAnimationFinish = false;
        }
        if (allDeadAnimationFinish) {
            int monsterCounter = 0;
            while (monsterCounter < monsterPanelList.Count) {
                if (monsterPanelList[monsterCounter].Monster.Dead) {
                    MonsterPanelControl mpc = monsterPanelList[monsterCounter];
                    monsterPanelList.RemoveAt(monsterCounter);
                    Destroy(mpc.gameObject);
                    if (monsterCounter <= currentAttackMonster) {
                        currentAttackMonster--;
                    }
                } else {
                    monsterCounter++;
                }
            }
            for (int i = 0; i < monsterPanelList.Count; i++) {
                monsterPanelList[i].playMoveForwardAnimation(i);
            }
        }
        return allDeadAnimationFinish;
    }

    public bool checkBattleEnd() {
        bool allMonsterDead = true;
        for (int i = 0; i < monsterList.Count; i++) {
            if (!monsterList[i].Dead)
                allMonsterDead = false;
        }
        if (allMonsterDead) {
            BattleEndAction();
            callBack(BattleEndType.AllMonsterDead);
            return true;
        }

        bool allCharaDead = true;
        for (int i = 0; i < CharaPanelList.Count; i++) {
            if (!CharaPanelList[i].Dead)
                allCharaDead = false;
        }
        if (allCharaDead) {
            BattleEndAction();
            callBack(BattleEndType.AllPlayerDead);
            return true;
        }
        return false;
    }

    private void BattleEndAction() {
        charaOrder.Clear();
        for (int i = 0; i < charaPanelList.Count; i++) {
            if (!charaPanelList[i].Dead) {
                charaPanelList[i].Interactive = false;
                charaPanelList[i].setOrder(0);
                charaPanelList[i].LightIcon();
            }
        }
        startBattleButton.interactable = false;
    }

    public void nextCharaAction() {
        if (charaOrder.Count > 0) {
            charaOrder[0].LightIcon();
            Chara chara = charaOrder[0].ResponseChara;
            int index = CharaPanelList.IndexOf(charaOrder[0]);
            Skill currentPlaySkill = chara.getSkill(chara.currentSkill);
            currentPlaySkill.playSkill(CharaPanelList, monsterPanelList, index);
        } else {
            setBattleState(BattleState.PlayerTurnPost);
        }
    }

    private void removeCurrentOrderChara() {
        if (charaOrder.Count <= 0)
            return;
        CharaPanelControl cpc = charaOrder[0];
        charaOrder[0].DelightIcon();
        charaOrder.RemoveAt(0);
        cpc.setOrder(0);
    }

    private bool checkMonsterDead() {
        bool hasMonsterDead = false;
        foreach (MonsterPanelControl mpc in monsterPanelList) {
            if (mpc.checkDead()) {
                hasMonsterDead = true;
            }
        }
        return hasMonsterDead;
    }

    private bool checkCharaDead() {
        bool hasCharaDead = false;
        foreach (CharaPanelControl cpc in charaPanelList) {
            if (!cpc.Dead && cpc.checkDead()) {
                charaOrder.Remove(cpc);
                hasCharaDead = true;
            }
        }
        return hasCharaDead;
    }

    public void addOrder(CharaPanelControl obj) {
        charaOrder.Add(obj);
    }

    public int getOrder(CharaPanelControl obj) {
        return charaOrder.IndexOf(obj);
    }

    public void removeOrder(CharaPanelControl obj) {
        charaOrder.Remove(obj);
        for (int i=0; i<charaOrder.Count; i++) {
            charaOrder[i].setOrder(i+1);
        }
    }
}
