using System;
using System.Collections.Generic;
using UnityEngine;

class CharaDataParser {
    public static List<Chara> ParseCharaData(string txtName) {
        TextAsset txt = Resources.Load("Stage/" + txtName) as TextAsset;
        string dialogText;
        string[] lines;
        int txtCounter = 0;
        dialogText = txt.text;
        lines = dialogText.Split('\n');

        List<Chara> charaList = new List<Chara>();

        while (txtCounter < lines.Length) {
            string name = lines[txtCounter].Trim();
            txtCounter++;
            int currentLevel = int.Parse(lines[txtCounter].Trim());
            txtCounter++;
            Chara chara = new Chara(name, currentLevel);

            for (int i = 0; i < 4 && lines[txtCounter].Trim() != ""; i++) {
                chara.setSkill(chara.info.getSkill(lines[txtCounter].Trim()), i);
                txtCounter++;
            }

            charaList.Add(chara);
            txtCounter++;
        }

        return charaList;
    }
}
