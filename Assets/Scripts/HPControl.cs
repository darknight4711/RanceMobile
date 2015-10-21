using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class HPControl : MonoBehaviour {
	public RectTransform HPLine;
	public Text HPText;

	private int maxHP;
	private int currentHP;

	public int MaxHP {
		get {
			return maxHP;
		}
		set {
			maxHP = value;
		}
	}

	public int CurrentHP {
		get {
			return currentHP;
		}
		set {
			if (value > maxHP)
				currentHP = maxHP;
			else if (value < 0)
				currentHP = 0;
			else
				currentHP = value;
			updateHPLine();
		}
	}

	public void updateHPLine () {
        if (HPText != null) {
            HPText.text = currentHP + "/" + maxHP;
        }
		float newWidth = currentHP / (float)maxHP;
		HPLine.localScale = new Vector3(newWidth,1,1);
	}

    public void setVisible(bool visible) {
        gameObject.SetActive(visible);
    }
}
