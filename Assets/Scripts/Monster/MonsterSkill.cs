using System;
using System.Collections.Generic;
public abstract class MonsterSkill {
    private string name;
    private int rate;

    public MonsterSkill(string[] text) {
        ParseSkill(text);
    }

    public string Name {
        get {
            return name;
        }
    }

    public int Rate {
        get {
            return rate;
        }
    }

    public abstract void startAttack(List<Chara> charaList, List<Monster> monsterList, int index);

    public abstract bool IsAttackFinish(List<Chara> charaList, List<Monster> monsterList, int index);

    public void ParseSkill(string[] text) {
        name = text[0];
        rate = int.Parse(text[1]);
    }
}
