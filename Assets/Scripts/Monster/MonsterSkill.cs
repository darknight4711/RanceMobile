using System;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class MonsterSkill : ScriptableObject {
    [SerializeField]
    private string skillName;
    [SerializeField]
    private int rate;

    public MonsterSkill(string[] text) {
        ParseSkill(text);
    }

    public MonsterSkill(MonsterSkillInfo info) {
        ParseSkill(info);
    }

    public string Name {
        get {
            return skillName;
        }
    }

    public int Rate {
        get {
            return rate;
        }
    }

    public abstract void startAttack(List<Chara> charaList, List<Monster> monsterList, int index);

    public abstract bool IsAttackFinish(List<Chara> charaList, List<Monster> monsterList, int index);

    public void ParseSkill(MonsterSkillInfo info) {
        skillName = info.skillName;
        rate = info.intParam1;
    }

    public void ParseSkill(string[] text) {
        skillName = text[0];
        rate = int.Parse(text[1]);
    }
}
