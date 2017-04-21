using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DrawSkillTree : MonoBehaviour {
    [SerializeField]
    RectTransform skillPanel;
    [SerializeField]
    GameObject skillButton;
    [SerializeField]
    GameObject lineObject;
    // Start is called just before any of the
    // Update methods is called the first time.
    void Start () {
        CharaInfo info = new CharaInfo("蘭斯");
        DrawTree(info.SkillTree);
        //DrawLine(new Vector2(0, 0), new Vector2(0, 200));
    }

    void DrawTree(SkillTreeNode skillTree) {
        List<int> widthPerLayer = skillTree.getWidthPerLayer();
        List<List<Vector2>> allLink = skillTree.getAllLink();
        for (int i = 1; i < allLink.Count; i++) {
            for (int j = 0; j < allLink[i].Count; j++) {
                Vector2 link = allLink[i][j];
                Vector2 pointA = calculateCenter(widthPerLayer[i], (int)link.x, i);
                Vector2 pointB = calculateCenter(widthPerLayer[i+1], (int)link.y, i+1);
                DrawLine(pointA, pointB);
            }
        }

        for (int i = 1; i < skillTree.getHeight(); i++) {
            List<Skill> skills = skillTree.getSkillsOfLayer(i);
            for (int j = 0; j < widthPerLayer[i]; j++) {
                Vector2 vec = calculateCenter(widthPerLayer[i], j, i);
                DrawSkillButton(vec, skills[j]);
            }
        }
    }

    private Vector2 calculateCenter(int layerCount, int layerNo, int layer) {
        float averageSpace = (skillPanel.rect.width - 40) / layerCount;
        return new Vector2(averageSpace*(layerNo+0.5F) - skillPanel.rect.width/2, layer*80);
    }

    Button DrawSkillButton(Vector2 center, Skill skill) {
        GameObject button = (GameObject)Instantiate(skillButton, new Vector3(0, 0, 0), Quaternion.identity);
        button.transform.SetParent(skillPanel, false);
        RectTransform buttonRectTransform = button.transform as RectTransform;
        buttonRectTransform.localPosition = new Vector2(center.x, skillPanel.rect.height/2 - center.y);

        button.GetComponentInChildren<Text>().text = skill.Name;

        return button.GetComponent<Button>();
    }

    void DrawLine(Vector2 pointA, Vector2 pointB) {
        Vector2 turnPointA = new Vector2(pointA.x, skillPanel.rect.height / 2 - pointA.y);
        Vector2 turnPointB = new Vector2(pointB.x, skillPanel.rect.height / 2 - pointB.y);
        Vector3 differenceVector = turnPointB - turnPointA;

        GameObject line = (GameObject)Instantiate(lineObject, new Vector3(0, 0, 0), Quaternion.identity);
        line.transform.SetParent(skillPanel.transform, false);
        RectTransform imageRectTransform = line.transform as RectTransform;

        imageRectTransform.sizeDelta = new Vector2(differenceVector.magnitude, 3);
        imageRectTransform.pivot = new Vector2(0, 0.5f);
        imageRectTransform.localPosition = turnPointA;
        float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
        imageRectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
