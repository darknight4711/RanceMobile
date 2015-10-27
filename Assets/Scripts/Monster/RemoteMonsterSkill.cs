using System;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonsterSkill : MonsterSkill {
    private int target;
    private bool inPlayAttackAnimation;

    public RemoteMonsterSkill(string[] text) : base(text) { }

    public override bool IsAttackFinish(List<Chara> charaList, List<Monster> monsterList, int index) {
        if (monsterList[index].View.Animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterAttack")) {
            inPlayAttackAnimation = true;
        } else if (inPlayAttackAnimation) {
            inPlayAttackAnimation = false;
            charaList[target].attackHP(monsterList[index].Info.ATK * Rate/100);
            charaList[target].View.DelightIcon();
            return true;
        }
        return false;
    }

    public override void startAttack(List<Chara> charaList, List<Monster> monsterList, int index) {
        UnityEngine.Random.seed = System.Guid.NewGuid().GetHashCode();
        do {
            target = UnityEngine.Random.Range(0, charaList.Count);
        } while (charaList[target].Dead) ;
        charaList[target].View.LightIcon();
        monsterList[index].View.Animator.SetTrigger("monsterAttack");
    }
}
