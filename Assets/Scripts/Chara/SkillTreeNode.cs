using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class SkillTreeNode {
    public Skill skill;
    public List<SkillTreeNode> childNode;

    public SkillTreeNode(Skill s) {
        skill = s;
        childNode = new List<SkillTreeNode>();
    }

    public List<Skill> getSkillsOfLayer(int layer) {
        List<Skill> skills = new List<Skill>();
        if (layer != 0 && childNode.Count == 0) {
            Assert.IsFalse(false, "No that layer!");
        }
        if (layer == 1) {
            for (int i = 0; i < childNode.Count; i++) {
                skills.Add(childNode[i].skill);
            }
        } else if (layer >= 1) {
            for (int i = 0; i < childNode.Count; i++) {
                skills.AddRange(childNode[i].getSkillsOfLayer(layer - 1));
            }
        }
        return skills;
    }

    public int getHeight() {
        if (childNode.Count != 0) {
            int maxHeight = 0;
            foreach (SkillTreeNode node in childNode) {
                if (node.getHeight() > maxHeight)
                    maxHeight = node.getHeight();
            }
            return maxHeight + 1;
        } else {
            return 1;
        }
    }

    public List<int> getWidthPerLayer() {
        List<int> widthPerLayer = new List<int>();
        widthPerLayer.Add(1);
        if (childNode.Count != 0) {
            foreach (SkillTreeNode node in childNode) {
                List<int> subWidthPerLayer = node.getWidthPerLayer();
                for (int i = 0; i < subWidthPerLayer.Count; i++) {
                    if (widthPerLayer.Count - 1 == i) {
                        widthPerLayer.Add(subWidthPerLayer[i]);
                    } else {
                        widthPerLayer[i + 1] += subWidthPerLayer[i];
                    }
                }
            }
        }
        return widthPerLayer;
    }

    public int getWidth() {
        List<int> widthPerLayer = getWidthPerLayer();
        int max = 0;
        foreach (int i in widthPerLayer) {
            if (i > max)
                max = i;
        }
        return max;
    }

    public List<List<Vector2>> getAllLink() {
        List<List<Vector2>> allLink = new List<List<Vector2>>();
        if (childNode.Count != 0) {
            allLink.Add(new List<Vector2>());
            for (int i = 0; i < childNode.Count; i++) {
                allLink[0].Add(new Vector2(0, i));
            }
            for (int i = 0; i < childNode.Count; i++) {
                List<List<Vector2>> subLink = childNode[i].getAllLink();
                float maxa = 0, maxb = 0;
                maxa = i;
                for (int j = 0; j < subLink.Count; j++) {
                    if (allLink.Count - 1 == j) {
                        allLink.Add(new List<Vector2>());
                        foreach (Vector2 vec in subLink[j]) {
                            allLink[j + 1].Add(new Vector2(vec.x + maxa, vec.y + maxb));
                        }
                    } else {
                        foreach (Vector2 vec in allLink[j + 1]) {
                            if (vec.y > maxb) {
                                maxb = vec.y;
                            }
                        }
                        maxb++;
                        foreach (Vector2 vec in subLink[j]) {
                            allLink[j + 1].Add(new Vector2(vec.x + maxa, vec.y + maxb));
                        }
                    }
                    maxa = maxb;
                    maxb = 0;
                }
            }
        }
        return allLink;
    }
}
