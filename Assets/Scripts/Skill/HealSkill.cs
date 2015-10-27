using System;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : Skill {
    Chara thisChara, targetChara;
    ParticleSystem healEffect;
    float startTime;

    public HealSkill (string name) : base(name) {
        healEffect = GameObject.Find("HealEffect").GetComponent<ParticleSystem>();
    }

    public override bool checkPlaySkillFinish(List<Chara> charaList, List<Monster> monsterList, int index) {
        if (Time.time - startTime > 0.5) {
            healEffect.Stop();
            targetChara.attackHP(-(int)(thisChara.ATK * Rate/100), false);
            charaList[index].increaseSkillCD(base.Cd, charaList[index].CurrentSkill);
            return true;
        }
        return false;
    }

    public override void playSkill(List<Chara> charaList, List<Monster> monsterList, int index) {
        float leastHP = charaList[index].CurrentHP/(float)charaList[index].HP;
        int leastHPIndex = index;
        for (int i = 0; i < charaList.Count; i++) {
            if (charaList[i].Dead)
                continue;
            if (charaList[i].CurrentHP/(float)charaList[i].HP < leastHP) {
                leastHP = charaList[i].CurrentHP/(float)charaList[i].HP;
                leastHPIndex = i;
            }
        }
        targetChara = charaList[leastHPIndex];
        thisChara = charaList[index];
        healEffect.gameObject.transform.position = new Vector3(targetChara.View.transform.position.x, targetChara.View.transform.position.y, -15);
        healEffect.Play();
        startTime = Time.time;
    }
}
