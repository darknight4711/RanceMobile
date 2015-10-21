using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRandomStrategy : MonsterStrategy {
    public override int strategy(List<MonsterSkill> skillList) {
        UnityEngine.Random.seed = System.Guid.NewGuid().GetHashCode();
        return UnityEngine.Random.Range(0, skillList.Count-1);
    }
}
