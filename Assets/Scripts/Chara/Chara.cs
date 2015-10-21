using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Chara{
    private string name;
    private int currentLevel;
    public int currentSkill = 0;
    private int currentHP = 0;
    private Skill[] skills = new Skill[4];
    public int[] skillCD = new int[4] { 0, 0, 0, 0 };
    public bool state = false; //false->not set order, true->set order

    public CharaInfo info;

    public Chara(string txtName, int level) {
        name = txtName;
        currentLevel = level;

        info = new CharaInfo(txtName);
        CurrentHP = HP;
    }

    public int HP {
        get {
            return (int)(info.MaxHP * CurrentLevel / 100.0F);
        }
    }

    public int ATK {
        get {
            return (int)(info.MaxATK * CurrentLevel / 100.0F);
        }
    }

    public int DEF {
        get {
            return (int)(info.MaxDEF * CurrentLevel / 100.0F);
        }
    }

    public int CurrentLevel {
        get {
            return currentLevel;
        }
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

    public string Name {
        get {
            return name;
        }
    }

    public void setSkill(Skill skill, int index) {
        skills[index] = skill;
    }

    public Skill getSkill(int index) {
        return skills[index];
    }
}
