using System;
using System.Collections.Generic;

public class SimpleSkill : Skill {
    public SimpleSkill (string name) : base(name) { }

    public override bool checkPlaySkillFinish(List<Chara> charaList, List<Monster> monsterList, int index) {
        return true;
    }

    public override void playSkill(List<Chara> charaList, List<Monster> monsterList, int index) {}
}
