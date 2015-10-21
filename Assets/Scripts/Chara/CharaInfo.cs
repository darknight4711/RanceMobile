using System;
using System.Collections.Generic;
using UnityEngine;

public class CharaInfo : Parser {
    public class treeNode {
        public Skill skill;
        public List<treeNode> childNode;

        public treeNode(Skill s) {
            skill = s;
            childNode = new List<treeNode>();
        }
    }

    private string name;
    private int maxHP;
    private int maxATK;
    private int maxDEF;

    public string Name {
        get {
            return name;
        }
    }

    private treeNode skillTree;

    public CharaInfo(string name) {
        ParseTXT(name);
    }

    public treeNode SkillTree {
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

    private Skill findSkill(string name, treeNode tree) {
        if (tree.childNode == null)
            return null;
        foreach (treeNode node in tree.childNode) {
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
        name = lines[txtCounter].Trim();
        txtCounter++;
        maxHP = int.Parse(lines[txtCounter].Trim());
        txtCounter++;
        maxATK = int.Parse(lines[txtCounter].Trim());
        txtCounter++;
        maxDEF = int.Parse(lines[txtCounter].Trim());
        txtCounter++;
        txtCounter++;

        skillTree = new treeNode(null);
        SkillFactory sf = new SkillFactory();

        List<treeNode> currentPath = new List<treeNode>();
        currentPath.Add(skillTree);
        while (txtCounter < lines.Length && lines[txtCounter].Trim() != "") {
            int counter = 0;
            while (lines[txtCounter][counter] == '>')
                counter++;
            if (counter <= currentPath.Count - 1) {
                while (counter < currentPath.Count - 1) {
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
                treeNode cNode = new treeNode(sf.CreateSkill(lines[txtCounter].Trim().Substring(counter)));
                currentPath[counter].childNode.Add(cNode);
                currentPath.Add(cNode);
            }
            txtCounter++;
        }
    }
}
