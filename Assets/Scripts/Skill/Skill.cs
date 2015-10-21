using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Skill : Parser {
	private string name;

	private int cd;
	private int rate;

    private string description;

    public Skill(string name) {
        ParseTXT(name);
    }

    public string Name {
		get {
			return name;
		}
	}

	public int Cd {
		get {
			return cd;
		}
	}

	public int Rate {
		get {
			return rate;
		}
	}

	public string Description {
		get {
			return description;
		}
	}

    public abstract void playSkill(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index);

    public abstract bool checkPlaySkillFinish(List<CharaPanelControl> charaList, List<MonsterPanelControl> monsterList, int index);
		
	public void ParseTXT (string txtName) {
        TextAsset txt = Resources.Load("txt/Skill/"+txtName) as TextAsset;
		if (txt == null) {
            name = txtName;
			return;
		}
		string dialogText;
		string[] lines;
		int txtCounter = 1;
		dialogText = txt.text;
		lines = dialogText.Split ('\n');
		name = txtName;
		cd = int.Parse (lines [txtCounter].Trim());
		txtCounter++;
		rate = int.Parse (lines [txtCounter].Trim());
		txtCounter++;
		txtCounter++;
		description = lines [txtCounter].Trim();
	} 
}
