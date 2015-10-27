using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicSkill : Skill {
    Sprite magicSprite;
    Monster targetMonster;

    public MagicSkill(string name) : base(name) {
        magicSprite = Resources.Load<Sprite>("effect/"+name);
    }

    public override bool checkPlaySkillFinish(List<Chara> charaList, List<Monster> monsterList, int index) {

        if (targetMonster.View.isPlayAttackedFinish()) {
            if (targetMonster.Info.Race != MonsterInfo.MonsterRace.Hony) {
                targetMonster.attackHP((int)(charaList[index].ATK * base.Rate / 100.0F));
            }
            charaList[index].increaseSkillCD(base.Cd, charaList[index].CurrentSkill);
            return true;
        }
        return false;
    }

    public override void playSkill(List<Chara> charaList, List<Monster> monsterList, int index) {
        int lastAliveMonster = 0;
        int aliveMonsterCounter = 0;
        int counter = 0;
        while (aliveMonsterCounter < 3) {
            if (!monsterList[counter].Dead) {
                lastAliveMonster = counter;
                aliveMonsterCounter++;
            }
            counter++;
            if (counter >= monsterList.Count)
                break;
        }
        targetMonster = monsterList[lastAliveMonster];
        targetMonster.View.AttackedImage.sprite = magicSprite;
        targetMonster.View.playAttackedAnimation();
    }
}
