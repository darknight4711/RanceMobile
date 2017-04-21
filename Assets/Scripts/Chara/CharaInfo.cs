using System;
using System.Collections.Generic;
using UnityEngine;

public class CharaInfo : ScriptableObject, Parser {
    [SerializeField]
    private string charaName;
    [SerializeField]
    private int maxHP;
    [SerializeField]
    private int maxATK;
    [SerializeField]
    private int maxDEF;

    public string Name {
        get {
            return charaName;
        }
    }

    [SerializeField]
    private SkillTreeNode skillTree;

    public CharaInfo(string name) {
        ParseTXT(name);
    }

    public SkillTreeNode SkillTree {
        get {
            return skillTree;
        }
    }

    public int MaxHP {
        get {
            return maxHP;
        }
    }

    public int MaxATK {
        get {
            return maxATK;
        }
    }

    public int MaxDEF {
        get {
            return maxDEF;
        }
    }

    public Skill getSkill(string name) {
        return findSkill(name, skillTree);
    }

    private Skill findSkill(string name, SkillTreeNode tree) {
        if (tree.childNode == null)
            return null;
        foreach (SkillTreeNode node in tree.childNode) {
            if (node.skill.Name == name) {
                return node.skill;
            } else {
                Skill s = findSkill(name, node);
                if (s != null)
                    return s;
            }
        }
        return null;
    }

    public void ParseTXT(string txtName) {
        TextAsset txt = Resources.Load("txt/Chara/" + txtName) as TextAsset;
        string dialogText;
        string[] lines;
        int txtCounter = 0;

        dialogText = txt.text;
        lines = dialogText.Split('\n');
        charaName = lines[txtCounter].Trim();
        txtCounter++;
        maxHP = int.Parse(lines[txtCounter].Trim());
        txtCounter++;
        maxATK = int.Parse(lines[txtCounter].Trim());
        txtCounter++;
        maxDEF = int.Parse(lines[txtCounter].Trim());
        txtCounter++;
        txtCounter++;

        skillTree = new SkillTreeNode(null);
        SkillFactory sf = new SkillFactory();

        List<SkillTreeNode> currentPath = new List<SkillTreeNode>();
        currentPath.Add(skillTree);
        while (txtCounter < lines.Length && lines[txtCounter].Trim() != "") {
            int counter = 0;
            while (lines[txtCounter][counter] == '>')
                counter++;
            if (counter <= currentPath.Count - 1) {
                while (counter < currentPath.Count - 1) {
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
                SkillTreeNode cNode = new SkillTreeNode(sf.CreateSkill(lines[txtCounter].Trim().Substring(counter)));
                currentPath[counter].childNode.Add(cNode);
                currentPath.Add(cNode);
            }
            txtCounter++;
        }
    }
}
