using System;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonsterSkill : MonsterSkill {
    private int target;
    private bool inPlayAttackAnimation;

    public RemoteMonsterSkill(string[] text) : base(text) { }

    public override bool IsAttackFinish(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index) {
        if (monsterList[index].animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterAttack")) {
            inPlayAttackAnimation = true;
        } else if (inPlayAttackAnimation) {
            inPlayAttackAnimation = false;
            charaList[target].attackHP(monsterList[index].Monster.Info.ATK * Rate/100);
            charaList[target].DelightIcon();
            return true;
        }
        return false;
    }

    public override void startAttack(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index) {
        UnityEngine.Random.seed = System.Guid.NewGuid().GetHashCode();
        do {
            target = UnityEngine.Random.Range(0, charaList.Count);
        } while (charaList[target].Dead) ;
        charaList[target].LightIcon();
        monsterList[index].animator.SetTrigger("monsterAttack");
    }
}
