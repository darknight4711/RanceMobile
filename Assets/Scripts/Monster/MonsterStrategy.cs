using System.Collections.Generic;

public abstract class MonsterStrategy {
    public abstract int strategy(List<MonsterSkill> skillList);
    public void ParseStrategy(string[] text) {}
}
