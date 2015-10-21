using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageInfo : Parser
{
	private string name;
	private int stageNum;
	private List<List<Monster>> monsterInfo;

	public StageInfo (string txtName) {
        ParseTXT(txtName);
	}

	public string Name {
		get {
			return name;
		}
	}

	public int StageNum {
		get {
			return stageNum;
		}
	}

	public List<Monster> MonsterList (int stageNum) {
		return monsterInfo[stageNum];
	}

	public void ParseTXT (string txtName) {
		TextAsset txt = Resources.Load("Stage/" + txtName) as TextAsset;
		
		string dialogText;
		string[] lines;
		int txtCounter = 0;
		dialogText = txt.text;
		lines = dialogText.Split ('\n');
		name = lines [txtCounter].Trim();
		txtCounter++;
		stageNum = int.Parse (lines [txtCounter].Trim());
		txtCounter++;
		txtCounter++;

        monsterInfo = new List<List<Monster>>();
		for (int i=0; i < stageNum; i++) {
            List<Monster> perStage = new List<Monster>();
			while (txtCounter < lines.Length && lines [txtCounter].Trim() != "") {
				Monster m = new Monster(lines [txtCounter].Trim());
				perStage.Add(m);
				txtCounter++;
			}
            monsterInfo.Add(perStage);
			txtCounter++;
		}

	} 
}
