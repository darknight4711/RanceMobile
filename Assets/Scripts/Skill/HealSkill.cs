using System;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : Skill {
    CharaPanelControl thisChara, targetChara;
    ParticleSystem healEffect;
    float startTime;

    public HealSkill (string name) : base(name) {
        healEffect = GameObject.Find("HealEffect").GetComponent<ParticleSystem>();
    }

    public override bool checkPlaySkillFinish(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index) {
        if (Time.time - startTime > 0.5) {
            healEffect.Stop();
            //healEffect.enableEmission = false;
            targetChara.attackHP(-(int)(thisChara.ResponseChara.ATK * Rate/100), false);
            thisChara.ResponseChara.skillCD[thisChara.ResponseChara.currentSkill] += base.Cd;
            return true;
        }
        return false;
    }

    public override void playSkill(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index) {
        float leastHP = charaList[index].ResponseChara.CurrentHP/(float)charaList[index].ResponseChara.HP;
        int leastHPIndex = index;
        for (int i = 0; i < charaList.Count; i++) {
            if (charaList[i].Dead)
                continue;
            if (charaList[i].ResponseChara.CurrentHP/(float)charaList[i].ResponseChara.HP < leastHP) {
                leastHP = charaList[i].ResponseChara.CurrentHP/(float)charaList[i].ResponseChara.HP;
                leastHPIndex = i;
            }
        }
        targetChara = charaList[leastHPIndex];
        thisChara = charaList[index];
        healEffect.gameObject.transform.position = new Vector3(targetChara.transform.position.x, targetChara.transform.position.y, -15);
        healEffect.Play();
        //healEffect.enableEmission = true;
        startTime = Time.time;
    }
}
