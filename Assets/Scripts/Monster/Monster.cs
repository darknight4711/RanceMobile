using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster {
    private string name;
    private int currentHP;
    private int currentPlace;

    private bool dead;

    private MonsterInfo info;

    private MonsterSkill currentUsedSkill;

    public string Name {
        get {
            return name;
        }
    }

    public Monster(string txtName) {
        name = txtName;

        Info = new MonsterInfo(txtName);
        CurrentHP = HP;
    }

    public int HP {
        get {
            return Info.HP;
        }
    }

    public int ATK {
        get {
            return Info.ATK;
        }
    }

    public int DEF {
        get {
            return Info.DEF;
        }
    }

    public int CurrentHP {
        get {
            return currentHP;
        }

        set {
            currentHP = value;
        }
    }

    public int CurrentPlace {
        get {
            return currentPlace;
        }

        set {
            currentPlace = value;
        }
    }

    public MonsterInfo Info {
        get {
            return info;
        }

        set {
            info = value;
        }
    }

    public bool Dead {
        get {
            return dead;
        }
    }

    public bool checkDead() {
        if (currentHP == 0) {
            dead = true;
        }
        return dead;
    }

    public int useStrategy() {
        return info.Strategy.strategy(info.getSkills());
    }

    public void startAttack(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index) {
        int strategySkill = useStrategy();
        currentUsedSkill = info.getSkill(strategySkill);
        currentUsedSkill.startAttack(charaList, monsterList, index);
    }

    public bool IsAttackFinish(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index) {
        return currentUsedSkill.IsAttackFinish(charaList, monsterList, index);
    }
}
