using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateManager : OrderManager<Chara> {
    public enum BattleEndType { None, AllMonsterDead, AllPlayerDead }
    BattleState battleStart;
    BattleState playerTurnPre;
    BattleState playerInteraction;
    BattleState playerAttck;
    BattleState playerTurnPost;
    BattleState enemyAttack;
    BattleState checkEnemyDead;
    BattleState checkPlayerDead;
    BattleState checkBattleEnd;
    BattleState monsterMoveForward;

    BattleViewControl view;

    private List<Chara> charaOrder = new List<Chara>();
    private List<Chara> charaList = new List<Chara>();
    private List<Monster> monsterList = new List<Monster>();

    private BattleState currentState;
    private bool combat = false;

    public BattleStateManager() {
        battleStart = new BattleStart();
        playerTurnPre = new PlayerTurnPre();
        playerInteraction = new PlayerInteraction();
        playerAttck = new PlayerAttack();
        playerTurnPost = new PlayerTurnPost();
        enemyAttack = new EnemyAttack();
        checkEnemyDead = new CheckEnemyDead();
        checkPlayerDead = new CheckPlayerDead();
        checkBattleEnd = new CheckBattleEnd();
        monsterMoveForward = new MonsterMoveForward();
    }

    public void initialBattle(BattleViewControl view) {
        this.view = view;
    }

    public void update() {
        if (combat) {
            CurrentState.update(this, view);
        }
    }

    public void setMonsters(List<Monster> monsterList) {
        this.monsterList = monsterList;
        view.initiateMonsterPanel(monsterList);
    }

    public void setChara(List<Chara> charaList) {
        this.charaList = charaList;
    }

    /*BattleEndCallBack*/

    public delegate void battleEndCallBack(BattleEndType endType);

    battleEndCallBack endCallBack;

    public battleEndCallBack EndCallBack {
        get {
            return endCallBack;
        }

        set {
            endCallBack = value;
        }
    }

    /*Back State*/

    private BattleState backState;

    public void back() {
        currentState = backState;
        backState.back(this, view);
    }

    /*private variable property*/

    public List<Monster> MonsterList {
        get {
            return monsterList;
        }
    }

    public List<Chara> CharaList {
        get {
            return charaList;
        }
    }

    public BattleState CurrentState {
        get {
            return currentState;
        }

        set {
            currentState = value;
            currentState.StateStart(this, view);
        }
    }

    public BattleState BackState {
        get {
            return backState;
        }

        set {
            backState = value;
        }
    }

    public bool Combat {
        get {
            return combat;
        }

        set {
            combat = value;
        }
    }

    /*BattleState getter*/

    public BattleState BattleStart {
        get {
            return battleStart;
        }
    }

    public BattleState PlayerTurnPre {
        get {
            return playerTurnPre;
        }
    }

    public BattleState PlayerInteraction {
        get {
            return playerInteraction;
        }
    }

    public BattleState PlayerAttck {
        get {
            return playerAttck;
        }
    }

    public BattleState PlayerTurnPost {
        get {
            return playerTurnPost;
        }
    }

    public BattleState EnemyAttack {
        get {
            return enemyAttack;
        }
    }

    public BattleState CheckEnemyDead {
        get {
            return checkEnemyDead;
        }
    }

    public BattleState CheckPlayerDead {
        get {
            return checkPlayerDead;
        }
    }

    public BattleState MonsterMoveForward {
        get {
            return monsterMoveForward;
        }
    }

    public BattleState CheckBattleEnd {
        get {
            return checkBattleEnd;
        }
    }




    /*OrderManager*/

    public void addOrder(Chara obj) {
        charaOrder.Add(obj);
    }

    public int getOrder(Chara obj) {
        return charaOrder.IndexOf(obj);
    }

    public void removeOrder(Chara obj) {
        charaOrder.Remove(obj);
        for (int i = 0; i < charaOrder.Count; i++) {
            charaOrder[i].Order = i + 1;
        }
        obj.Order = 0;
    }

    public int getOrderedCount() {
        return charaOrder.Count;
    }

    public Chara getOrderedObject(int i) {
        return charaOrder[i];
    }

    public void clearOrder() {
        charaOrder.Clear();
    }
}
