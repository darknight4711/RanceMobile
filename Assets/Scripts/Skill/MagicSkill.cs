using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicSkill : Skill {
    Sprite magicSprite;
    MonsterPanelControl targetMPC;

    public MagicSkill(string name) : base(name) {
        magicSprite = Resources.Load<Sprite>("effect/"+name);
    }

    public override bool checkPlaySkillFinish(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index) {

        if (targetMPC.IsPlayAttackedFinish()) {
            if (targetMPC.Monster.Info.Race != MonsterInfo.MonsterRace.Hony) {
                targetMPC.attackHP((int)(charaList[index].ResponseChara.ATK * base.Rate / 100.0F));
            }
            charaList[index].ResponseChara.skillCD[charaList[index].ResponseChara.currentSkill] += base.Cd;
            return true;
        }
        return false;
    }

    public override void playSkill(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index) {
        int lastMonster = monsterList.Count-1;
        targetMPC = monsterList[lastMonster];
        targetMPC.AttackedImage.sprite = magicSprite;
        targetMPC.playAttackedAnimation();
    }
}
