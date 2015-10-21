using UnityEngine;

public class SkillFactory {
    public Skill CreateSkill(string txtName) {
        TextAsset txt = Resources.Load("txt/Skill/" + txtName) as TextAsset;
        if (txt == null) {
            return new SimpleSkill(txtName);
        }

        string dialogText;
        string[] lines;
        int txtCounter = 0;
        dialogText = txt.text;
        lines = dialogText.Split('\n');
        string type = lines[txtCounter].Trim();

        Skill skill = null;
        switch (type) {
            case "Physic":
                skill = new PhysicSkill(txtName);
                break;
            case "Magic":
                skill = new MagicSkill(txtName);
                break;
            case "Heal":
                skill = new HealSkill(txtName);
                break;
        }
        return skill;
    }
}

