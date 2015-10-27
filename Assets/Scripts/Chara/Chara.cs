using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Chara{
    private string name;
    private int currentLevel;
    private int currentSkill = 0;
    private int currentHP = 0;
    private Skill[] skills = new Skill[4];
    private int[] skillCD = new int[4] { 0, 0, 0, 0 };
    private int order = 0;
    private bool dead = false;

    private CharaInfo info;
    private CharaPanelControl view;


    public Chara(string txtName, int level) {
        name = txtName;
        currentLevel = level;
        Info = new CharaInfo(txtName);
        CurrentHP = HP;
    }

    public void turnPre() {
        for (int i = 0; i < 4; i++) {
            skillCD[i] = skillCD[i] - 1 < 0 ? 0 : skillCD[i] - 1;
        }
        View.LightIcon();
        View.updateSkill();
    }

    public void startInteraction() {
        Order = 0;
        View.Interactive = true;
    }

    public void startBattle() {
        View.Interactive = false;
    }

    public void turnPost() {
        View.DelightIcon();
    }

    public void attackHP(int damage, bool guard = true) {
        if (guard)
            CurrentHP -= damage - DEF;
        else
            CurrentHP -= damage;
        View.updateHP();
    }

    public void setSkill(int index) {
        CurrentSkill = index;
        View.updateSkill();
        if (skillCD[CurrentSkill] == 0) {
            View.LightIcon();
        }
    }

    public bool checkDead() {
        if (CurrentHP == 0) {
            dead = true;
            Order = 0;
            View.DelightIcon();
        }
        return dead;
    }

    public void resetStatus() {
        Order = 0;
        View.Interactive = false;
        if (!Dead) {
            View.LightIcon();
        } else {
            View.DelightIcon();
        }
    }

    public int HP {
        get {
            return (int)(Info.MaxHP * CurrentLevel / 100.0F);
        }
    }

    public int ATK {
        get {
            return (int)(Info.MaxATK * CurrentLevel / 100.0F);
        }
    }

    public int DEF {
        get {
            return (int)(Info.MaxDEF * CurrentLevel / 100.0F);
        }
    }

    /*variable property*/

    public string Name {
        get {
            return name;
        }
    }

    public int CurrentLevel {
        get {
            return currentLevel;
        }
    }

    public int CurrentSkill {
        get {
            return currentSkill;
        }

        set {
            currentSkill = value;
        }
    }

    public void setSkill(Skill skill, int index) {
        skills[index] = skill;
    }

    public Skill getSkill(int index) {
        return skills[index];
    }

    public void increaseSkillCD(int cd, int index) {
        skillCD[index] += cd;
        if (skillCD[index] < 0)
            skillCD[index] = 0;
        else if (skillCD[index] > skills[index].Cd)
            skillCD[index] = skills[index].Cd;
    }

    public int getSkillCD(int index) {
        return skillCD[index];
    }

    public int CurrentHP {
        get {
            return currentHP;
        }

        set {
            currentHP = value;
            if (value > HP) {
                currentHP = HP;
            } else if (value < 0) {
                currentHP = 0;
            }
        }
    }

    public CharaInfo Info {
        get {
            return info;
        }

        set {
            info = value;
        }
    }

    public CharaPanelControl View {
        get {
            return view;
        }

        set {
            view = value;
        }
    }

    public int Order {
        get {
            return order;
        }

        set {
            order = value;
            View.updateOrder();
        }
    }

    public bool Dead {
        get {
            return dead;
        }

        set {
            dead = value;
        }
    }
}
