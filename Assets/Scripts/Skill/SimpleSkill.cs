using System;
using System.Collections.Generic;

public class SimpleSkill : Skill {
    public SimpleSkill (string name) : base(name) { }

    public override bool checkPlaySkillFinish(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index) {
        return true;
    }

    public override void playSkill(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index) {}
}
