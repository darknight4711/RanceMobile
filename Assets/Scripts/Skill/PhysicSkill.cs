using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicSkill : Skill {
    Sprite physicSprite;
    int targetIndex;
    public PhysicSkill (string name) : base(name) {
        physicSprite = Resources.Load<Sprite>("effect/"+name);
    }

    public override bool checkPlaySkillFinish(List<Chara> charaList, List<Monster> monsterList, int index) {
        Monster monster = monsterList[targetIndex];
        if (monster.View.isPlayAttackedFinish()) {
            monster.attackHP((int)(charaList[index].ATK * base.Rate / 100.0F));
            charaList[index].increaseSkillCD(base.Cd, charaList[index].CurrentSkill);
            return true;
        }
        return false;
    }

    public override void playSkill(List<Chara> charaList, List<Monster> monsterList, int index) {
        targetIndex = 0;
        while (monsterList[targetIndex].Dead)
            targetIndex++;
        Monster monster = monsterList[targetIndex];
        monster.View.AttackedImage.sprite = physicSprite;
        monster.View.playAttackedAnimation();
    }
}