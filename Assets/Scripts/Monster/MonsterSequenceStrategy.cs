using System;
using System.Collections.Generic;
public class MonsterSequenceStrategy : MonsterStrategy {
    private int nextSkillNum = 0;

    public override int strategy(List<MonsterSkill> skillList) {
        int returnValue = nextSkillNum;
        nextSkillNum++;
        nextSkillNum %= skillList.Count;
        return returnValue;
    }
}
