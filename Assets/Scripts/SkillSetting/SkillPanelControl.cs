using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillPanelControl : MonoBehaviour {
    private Text skillName;
    private Text cd;
    private Text description;
    private Toggle learned;

    public void setSkill(Skill skill) {
        skillName.text = skill.Name;
    }
}
