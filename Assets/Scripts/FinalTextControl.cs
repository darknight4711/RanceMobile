using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalTextControl : MonoBehaviour {
    public Image thisPanel;
    public Text finalText;

    public void setText(string str) {
        finalText.text = str;
    }

    public void hideText() {
        thisPanel.enabled = false;
        finalText.enabled = false;
    }

    public void showText() {
        transform.SetAsLastSibling();
        thisPanel.enabled = true;
        finalText.enabled = true;
    }
}
