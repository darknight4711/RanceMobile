using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster {
    private string name;
    private int currentHP;
    private int currentPlace;

    private bool dead;

    private MonsterPanelControl view;
    private MonsterInfo info;

    private MonsterSkill currentUsedSkill;

    public void attackHP(int damage, bool guard = true) {
        if (guard) {
            CurrentHP -= damage - DEF;
        } else {
            CurrentHP -= damage;
        }

        View.updateHP();
    }

    public bool checkDead() {
        if (currentHP == 0) {
            dead = true;
            View.playDeadAnimation();
        }
        return dead;
    }

    private int useStrategy() {
        return info.Strategy.strategy(info.getSkills());
    }

    public void startAttack(List<Chara> charaList, List<Monster> monsterList, int index) {
        int strategySkill = useStrategy();
        currentUsedSkill = info.getSkill(strategySkill);
        currentUsedSkill.startAttack(charaList, monsterList, index);
    }

    public bool IsAttackFinish(List<Chara> charaList, List<Monster> monsterList, int index) {
        return currentUsedSkill.IsAttackFinish(charaList, monsterList, index);
    }

    public Monster(string txtName) {
        name = txtName;

        info = new MonsterInfo(txtName);
        currentHP = HP;
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

    /*variable property*/

    public string Name {
        get {
            return name;
        }
    }

    public int CurrentHP {
        get {
            return currentHP;
        }
        set {
            currentHP = value;
            if (currentHP < 0)
                currentHP = 0;
            else if (currentHP > HP)
                currentHP = HP;
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

    public bool Dead {
        get {
            return dead;
        }
    }

    public MonsterPanelControl View {
        get {
            return view;
        }

        set {
            view = value;
        }
    }

    public MonsterInfo Info {
        get {
            return info;
        }
    }

    

    
}
