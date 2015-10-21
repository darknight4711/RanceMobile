using System;
using UnityEngine;
using UnityEngine.UI;

public class LogPanelController : MonoBehaviour {
    public Text logText;
    public Scrollbar sb;

    public void addText(string text) {
        logText.text += text;
    }

    public void clearText() {
        logText.text = "";
    }

    public void OnLogClicked() {
        gameObject.SetActive(!gameObject.activeInHierarchy);
        Canvas.ForceUpdateCanvases();
        sb.value = 0;
        Canvas.ForceUpdateCanvases();
    }
}
