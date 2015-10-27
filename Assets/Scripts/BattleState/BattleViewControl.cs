using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

public class BattleViewControl : MonoBehaviour {

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Button startBattleButton;

    private BattleStateManager manager;
    private List<MonsterPanelControl> monsterPanelList = new List<MonsterPanelControl>();

    void Awake() {
        manager = new BattleStateManager();
        manager.initialBattle(this);
    }

    void Update() {
        manager.update();
    }

    public void startBattle() {
        Manager.CurrentState = Manager.PlayerAttck;
    }


    public void initiateMonsterPanel(List<Monster> monsterList) {
        for (int i = 0; i < monsterList.Count; i++) {
            GameObject newMonsterPanel = (GameObject)Instantiate(Resources.Load("MonsterPanel"), new Vector3(0, 0, 0), Quaternion.identity);
            newMonsterPanel.transform.SetParent(canvas.transform, false);
            newMonsterPanel.transform.localPosition = new Vector3(0 + i * 30, 95 + i * 30);
            MonsterPanelControl mpc = newMonsterPanel.GetComponentInChildren<MonsterPanelControl>();
            MonsterPanelList.Add(mpc);
            mpc.setMonster(monsterList[i]);
        }
        for (int i = MonsterPanelList.Count - 1; i >= 0; i--) {
            MonsterPanelList[i].transform.SetAsLastSibling();
        }
    }


    public bool isMonsterShowUpFinish() {
        bool isShowEnemyFinish = true;
        for (int i = 0; i < MonsterPanelList.Count; i++) {
            if (!MonsterPanelList[i].isShowUpFinish())
                isShowEnemyFinish = false;
        }
        return isShowEnemyFinish;

    }

    public void DestroyDeadEnemy() {
        int monsterCounter = 0;
        while (monsterCounter < MonsterPanelList.Count) {
            if (MonsterPanelList[monsterCounter].Monster.Dead) {
                MonsterPanelControl mpc = MonsterPanelList[monsterCounter];
                MonsterPanelList.RemoveAt(monsterCounter);
                mpc.Monster.View = null;
                Destroy(mpc.gameObject);
            } else {
                monsterCounter++;
            }
        }
    }

    /*UI property*/

    public Button StartBattleButton {
        get {
            return startBattleButton;
        }

        set {
            startBattleButton = value;
        }
    }

    /*private variable getter*/

    public BattleStateManager Manager {
        get {
            return manager;
        }
    }

    public List<MonsterPanelControl> MonsterPanelList {
        get {
            return monsterPanelList;
        }

        set {
            monsterPanelList = value;
        }
    }
}
