using System;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterRace { Normal, Hony }
public enum MonsterStrategyType { Sequence, Random }
public enum MonsterSkillType { Normal, Remote }

[Serializable]
public class MonsterSkillInfo {
    public string skillName;
    public MonsterSkillType type;
    public int intParam1;
    public int intParam2;
}

public class MonsterInfo : ScriptableObject, Parser {

    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private string monsterName;
    [SerializeField]
    private MonsterRace race;
    [SerializeField]
    private int hp;
    [SerializeField]
    private int atk;
    [SerializeField]
    private int def;

    [SerializeField]
    private MonsterStrategy strategy;
    [SerializeField]
    private List<MonsterSkill> skills;

    [SerializeField]
    private MonsterStrategyType strategyType;
    [SerializeField]
    private List<MonsterSkillInfo> skillInfos;

    public MonsterInfo() {
        
    }

    public MonsterInfo (string txtName) {
        ParseTXT(txtName);
    }

    public string Name {
        get {
            return monsterName;
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

    public Sprite Sprite {
        get {
            return sprite;
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

    public void parseAsset() {
        switch (strategyType) {
            case MonsterStrategyType.Random:
                strategy = new MonsterRandomStrategy();
                break;
            case MonsterStrategyType.Sequence:
                strategy = new MonsterSequenceStrategy();
                break;
        }
        for (int i = 0; i < skillInfos.Count; i++) {
            switch (skillInfos[i].type) {
                case MonsterSkillType.Normal:
                    skills.Add(new NormalMonsterSkill(skillInfos[i]));
                    break;
                case MonsterSkillType.Remote:
                    skills.Add(new RemoteMonsterSkill(skillInfos[i]));
                    break;
            }
        }
    }

    public void ParseTXT(string txtName) {
        TextAsset txt = Resources.Load("txt/Monster/" + txtName) as TextAsset;

        string dialogText;
        string[] lines;
        int txtCounter = 0;
        dialogText = txt.text;
        lines = dialogText.Split('\n');
        monsterName = lines[txtCounter].Trim();
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
