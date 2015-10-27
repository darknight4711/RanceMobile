using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageStateManager {
    StageState showText;
    StageState showEnemy;
    StageState battle;
    StageState allMonsterDead;
    StageState allPlayerDead;

    StageViewControl view;

    private StageState currentState;
    private StageInfo info;
    private int stageCount;

    private List<Chara> charaList = new List<Chara>();

    public StageStateManager() {
        showText = new ShowText();
        showEnemy = new ShowEnemy();
        battle = new Battle();
        allMonsterDead = new AllMonsterDead();
        allPlayerDead = new AllPlayerDead();
    }

    public void initialStage(StageViewControl view, string stageName, string charaData) {
        charaList = CharaDataParser.ParseCharaData(charaData);
        this.view = view;
        info = new StageInfo(stageName);
        CurrentState = ShowText;
    }

    public void update() {
        CurrentState.update(this, view);
    }

    /*private variable getter*/
    public virtual StageState CurrentState {
        get {
            return currentState;
        }

        set {
            currentState = value;
            currentState.StateStart(this, view);
        }
    }

    public virtual StageInfo Info {
        get {
            return info;
        }
    }

    public virtual int StageCount {
        get {
            return stageCount;
        }
    }

    public void stageCountPlusOne() {
        stageCount++;
    }

    /*Each StageState getter*/
    public virtual StageState ShowText {
        get {
            return showText;
        }
    }

    public virtual StageState ShowEnemy {
        get {
            return showEnemy;
        }
    }

    public virtual StageState Battle {
        get {
            return battle;
        }
    }

    public virtual StageState AllMonsterDead {
        get {
            return allMonsterDead;
        }
    }

    public virtual StageState AllPlayerDead {
        get {
            return allPlayerDead;
        }
    }

    public List<Chara> CharaList {
        get {
            return charaList;
        }
    }
}
