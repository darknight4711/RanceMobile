using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicSkill : Skill {
    Sprite physicSprite;
    public PhysicSkill (string name) : base(name) {
        physicSprite = Resources.Load<Sprite>("effect/"+name);
    }

    public override bool checkPlaySkillFinish(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index) {
        MonsterPanelControl mpc = monsterList[0];
        if (mpc.IsPlayAttackedFinish()) {
            mpc.attackHP((int)(charaList[index].ResponseChara.ATK * base.Rate / 100.0F));
            charaList[index].ResponseChara.skillCD[charaList[index].ResponseChara.currentSkill] += base.Cd;
            return true;
        }
        return false;
    }

    public override void playSkill(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index) {
        MonsterPanelControl mpc = monsterList[0];
        mpc.AttackedImage.sprite = physicSprite;
        mpc.playAttackedAnimation();
    }
}