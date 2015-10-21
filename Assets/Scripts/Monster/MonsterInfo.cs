using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : Parser {
    public enum MonsterRace { Normal, Hony }
    private string name;
    private MonsterRace race;
    private int hp;
    private int atk;
    private int def;

    private MonsterStrategy strategy;
    private List<MonsterSkill> skills;
   

    public MonsterInfo (string txtName) {
        ParseTXT(txtName);
    }

    public string Name {
        get {
            return name;
        }
    }

    public MonsterRace Race {
        get {
            return race;
        }
    }

    public int HP {
        get {
            return hp;
        }
    }

    public int ATK {
        get {
            return atk;
        }
    }

    public int DEF {
        get {
            return def;
        }
    }

    public MonsterStrategy Strategy {
        get {
            return strategy;
        }
    }

    public int skillCount() {
        return skills.Count;
    }

    public MonsterSkill getSkill (int i) {
        return skills[i];
    }
    public List<MonsterSkill> getSkills() {
        return new List<MonsterSkill>(skills);
    }

    public void ParseTXT(string txtName) {
        TextAsset txt = Resources.Load("txt/Monster/" + txtName) as TextAsset;

        string dialogText;
        string[] lines;
        int txtCounter = 0;
        dialogText = txt.text;
        lines = dialogText.Split('\n');
        name = lines[txtCounter].Trim();
        txtCounter++;
        race = (MonsterRace) Enum.Parse(typeof(MonsterRace), lines[txtCounter].Trim());
        txtCounter++;
        hp = int.Parse(lines[txtCounter].Trim());
        txtCounter++;
        atk = int.Parse(lines[txtCounter].Trim());
        txtCounter++;
        def = int.Parse(lines[txtCounter].Trim());
        txtCounter++;
        switch (lines[txtCounter].Trim()) {
            case "Random":
                strategy = new MonsterRandomStrategy();
                break;
            case "Sequence":
                strategy = new MonsterSequenceStrategy();
                break;
        }
        txtCounter++;
        txtCounter++;

        skills = new List<MonsterSkill>();
        while (txtCounter < lines.Length) {
            string[] skillMsg = new string[5];
            skillMsg[0] = lines[txtCounter].Trim();
            txtCounter++;
            switch (lines[txtCounter].Trim()) {
                case "Normal":
                    txtCounter++;
                    skillMsg[1] = lines[txtCounter].Trim();
                    txtCounter++;
                    skills.Add(new NormalMonsterSkill(skillMsg));
                    break;
                case "Remote":
                    txtCounter++;
                    skillMsg[1] = lines[txtCounter].Trim();
                    txtCounter++;
                    skills.Add(new RemoteMonsterSkill(skillMsg));
                    break;
            }
            txtCounter++;
        }
    }
}
