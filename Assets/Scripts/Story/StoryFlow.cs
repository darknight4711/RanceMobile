using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class StoryFlow : MonoBehaviour {
	public Text nametext;
	public Text sentensetext;
	public Image background;
	public Image chara1;
    public Image chara2;
    public LogPanelController LPC;
    public GameObject LogPanel;
    public Scrollbar sb;

    [SerializeField]
    private bool pause = false;

    private Sprite none;
    Story story;

    int sceneCounter = 0;
	int sentenceCounter = 0;

	float lastTouchTime;

	// Use this for initialization
	void Start () {
        none = Resources.Load<Sprite>("none");
        chara1.sprite = none;
        chara2.sprite = none;
        //logText.text = "";
        LPC.clearText();
        story = new Story("Scene1");

        background.sprite = story.Background[story.Scenes[0].background];
        
        while (story.Scenes[sceneCounter].sentences[sentenceCounter].sentence == null) {
            switch (story.Scenes[0].sentences[sentenceCounter].effect) {
                case Story.Sentence.SentenceEffect.CharaAppearLeft:
                    chara1.sprite = story.Chara[story.Scenes[0].sentences[sentenceCounter].effectParam1];
                    break;
                case Story.Sentence.SentenceEffect.CharaAppearRight:
                    chara2.sprite = story.Chara[story.Scenes[0].sentences[sentenceCounter].effectParam1];
                    break;
            }
            sentenceCounter++;
        }
        
        nametext.text = story.Scenes[sceneCounter].sentences[sentenceCounter].speaker;
        sentensetext.text = story.Scenes[sceneCounter].sentences[sentenceCounter].sentence;
        if (story.Scenes[0].sentences[sentenceCounter].effect == Story.Sentence.SentenceEffect.Loud) {
            sentensetext.color = Color.red;
            sentensetext.fontSize = 30;
        } else {
            sentensetext.color = Color.white;
            sentensetext.fontSize = 25;
        }
        LPC.addText(nametext.text + "\n" + sentensetext.text + "\n");
        //logText.text = logText.text + nametext.text + "\n" + sentensetext.text + "\n";

        sentenceCounter++;
        lastTouchTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetButtonDown("Fire1") && Time.time - lastTouchTime > 0.5 && !pause) {
		////if (Input.touchCount > 0 && Time.time - lastTouchTime > 1) {
		//	lastTouchTime = Time.time;
  //          if (sentenceCounter >= story.Scenes[sceneCounter].sentences.Count) {
  //              sceneCounter++;
  //              if (sceneCounter >= story.Scenes.Count) {
  //                  Application.LoadLevel("Battle");
  //              } else {
  //                  sentenceCounter = 0;
  //                  background.sprite = story.Background[story.Scenes[sceneCounter].background];
  //                  sentensetext.text = "";
  //                  nametext.text = "";
  //                  chara1.sprite = none;
  //                  chara2.sprite = none;
  //              }
  //          } else {
  //              Story.Sentence tempSentence = story.Scenes[sceneCounter].sentences[sentenceCounter];
  //              if (tempSentence.sentence == null) {
  //                  switch (tempSentence.effect) {
  //                      case Story.Sentence.SentenceEffect.CharaAppearLeft:
  //                          chara1.sprite = story.Chara[story.Scenes[sceneCounter].sentences[sentenceCounter].effectParam1];
  //                          break;
  //                      case Story.Sentence.SentenceEffect.CharaAppearRight:
  //                          chara2.sprite = story.Chara[story.Scenes[sceneCounter].sentences[sentenceCounter].effectParam1];
  //                          break;
  //                      case Story.Sentence.SentenceEffect.SwayLeft:
  //                          chara1.GetComponent<Animation>().Play();
  //                          break;
  //                      case Story.Sentence.SentenceEffect.SwayRight:
  //                          chara2.GetComponent<Animation>().Play();
  //                          break;
  //                  }
  //                  sentenceCounter++;
  //                  tempSentence = story.Scenes[sceneCounter].sentences[sentenceCounter];
  //              }

  //              if (nametext.text != tempSentence.speaker) {
  //                  LPC.addText("\n" + tempSentence.speaker + "\n" + tempSentence.sentence + "\n");
  //                  //logText.text = logText.text + "\n" + tempSentence.speaker + "\n" + tempSentence.sentence + "\n";
  //              } else {
  //                  LPC.addText(tempSentence.sentence + "\n");
  //                  //logText.text = logText.text + tempSentence.sentence + "\n";
  //              }
  //              sentensetext.text = tempSentence.sentence;
  //              nametext.text = tempSentence.speaker;
  //              if (tempSentence.effect == Story.Sentence.SentenceEffect.Loud) {
  //                  sentensetext.color = Color.red;
  //                  sentensetext.fontSize = 30;
  //              } else {
  //                  sentensetext.color = Color.white;
  //                  sentensetext.fontSize = 25;
  //              }
  //              if (tempSentence.index == 1) {
  //                  chara1.color = Color.white;
  //                  chara2.color = Color.gray;
  //              } else {
  //                  chara1.color = Color.gray;
  //                  chara2.color = Color.white;
  //              }
  //              sentenceCounter++;
  //          }
		//}
	}

    public  void OnLogButtonClicked() {
        pause = !pause;
    }


    public void Skip() {
        Application.LoadLevel("Battle");
    }

    public void updateText() {
        if (Time.time - lastTouchTime > 0.5 && !pause) {
            //if (Input.touchCount > 0 && Time.time - lastTouchTime > 1) {
            lastTouchTime = Time.time;
            if (sentenceCounter >= story.Scenes[sceneCounter].sentences.Count) {
                sceneCounter++;
                if (sceneCounter >= story.Scenes.Count) {
                    Application.LoadLevel("Battle");
                } else {
                    sentenceCounter = 0;
                    background.sprite = story.Background[story.Scenes[sceneCounter].background];
                    sentensetext.text = "";
                    nametext.text = "";
                    chara1.sprite = none;
                    chara2.sprite = none;
                }
            } else {
                Story.Sentence tempSentence = story.Scenes[sceneCounter].sentences[sentenceCounter];
                if (tempSentence.sentence == null) {
                    switch (tempSentence.effect) {
                        case Story.Sentence.SentenceEffect.CharaAppearLeft:
                            chara1.sprite = story.Chara[story.Scenes[sceneCounter].sentences[sentenceCounter].effectParam1];
                            break;
                        case Story.Sentence.SentenceEffect.CharaAppearRight:
                            chara2.sprite = story.Chara[story.Scenes[sceneCounter].sentences[sentenceCounter].effectParam1];
                            break;
                        case Story.Sentence.SentenceEffect.SwayLeft:
                            chara1.GetComponent<Animation>().Play();
                            break;
                        case Story.Sentence.SentenceEffect.SwayRight:
                            chara2.GetComponent<Animation>().Play();
                            break;
                    }
                    sentenceCounter++;
                    tempSentence = story.Scenes[sceneCounter].sentences[sentenceCounter];
                }

                if (nametext.text != tempSentence.speaker) {
                    LPC.addText("\n" + tempSentence.speaker + "\n" + tempSentence.sentence + "\n");
                    //logText.text = logText.text + "\n" + tempSentence.speaker + "\n" + tempSentence.sentence + "\n";
                } else {
                    LPC.addText(tempSentence.sentence + "\n");
                    //logText.text = logText.text + tempSentence.sentence + "\n";
                }
                sentensetext.text = tempSentence.sentence;
                nametext.text = tempSentence.speaker;
                if (tempSentence.effect == Story.Sentence.SentenceEffect.Loud) {
                    sentensetext.color = Color.red;
                    sentensetext.fontSize = 30;
                } else {
                    sentensetext.color = Color.white;
                    sentensetext.fontSize = 25;
                }
                if (tempSentence.index == 1) {
                    chara1.color = Color.white;
                    chara2.color = Color.gray;
                } else {
                    chara1.color = Color.gray;
                    chara2.color = Color.white;
                }
                sentenceCounter++;
            }
        }
    }
}
