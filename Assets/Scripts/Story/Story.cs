using UnityEngine;
using System.Collections.Generic;
using System;

public class Story :  Parser
{
    public struct Sentence
    {
        public enum SentenceEffect { None, CharaAppearLeft, CharaAppearRight, Loud, SwayLeft, SwayRight }
        public SentenceEffect effect;
        public string effectParam1;
        public int index;
        public string speaker;
        public string sentence;
    }

    public struct StoryScene
    {
        public string background;
        public List<Sentence> sentences;
    }

    private List<StoryScene> scenes;
    private Dictionary<string, Sprite> background;
    private Dictionary<string, Sprite> chara;

    public Story(string v)
    {
        ParseTXT(v);
    }

    public List<StoryScene> Scenes
    {
        get
        {
            return scenes;
        }
    }

    public Dictionary<string, Sprite> Background
    {
        get
        {
            return background;
        }
    }

    public Dictionary<string, Sprite> Chara
    {
        get
        {
            return chara;
        }
    }

    public void ParseTXT(string txtName)
    {
        scenes = new List<StoryScene>();
        background = new Dictionary<string, Sprite>();
        chara = new Dictionary<string, Sprite>();

        TextAsset txt;
        string dialogText;
        string[] lines;
        int txtCounter = 0;

        txt = Resources.Load("Story/" + txtName) as TextAsset;
        dialogText = txt.text;
        lines = dialogText.Split('\n');

        while (lines[txtCounter] != "\r")
        {
            lines[txtCounter] = lines[txtCounter].Trim();
            background.Add(lines[txtCounter], Resources.Load<Sprite>(lines[txtCounter]));
            txtCounter++;
        }
        txtCounter++;
        while (lines[txtCounter] != "\r")
        {
            lines[txtCounter] = lines[txtCounter].Trim();
            chara.Add(lines[txtCounter], Resources.Load<Sprite>(lines[txtCounter]));
            txtCounter++;
        }
        txtCounter++;

        
        while (txtCounter < lines.Length)
        {
            lines[txtCounter] = lines[txtCounter].Trim();
            StoryScene tempScene = new StoryScene();
            tempScene.background = lines[txtCounter].Substring(2);
            tempScene.sentences = new List<Sentence>();
            scenes.Add(tempScene);

            txtCounter += 2;
            string currentSpeaker = "???";
            int currentIndex = 0;
            while (txtCounter < lines.Length && !lines[txtCounter].Contains("B:")) {
                lines[txtCounter] = lines[txtCounter].Trim();
                if (lines[txtCounter] == "") {
                    txtCounter++;
                    continue;
                }
                Sentence tempSentence = new Sentence();
                if (lines[txtCounter].Contains(">")) {
                    if (lines[txtCounter].Contains(":"))
                    {
                        switch (lines[txtCounter][1])
                        {
                            case 's':
                                tempSentence.effect = Sentence.SentenceEffect.Loud;
                                break;
                        }
                        tempSentence.sentence = lines[txtCounter].Substring(3);
                    }
                    else {
                        tempSentence.effect = Sentence.SentenceEffect.None;
                        tempSentence.sentence = lines[txtCounter].Substring(1);
                    }
                    tempSentence.speaker = currentSpeaker;
                    tempSentence.index = currentIndex;
                    tempScene.sentences.Add(tempSentence);
                } else {
                    switch (lines[txtCounter][0]) {
                        case 'L':
                            tempSentence.effect = Sentence.SentenceEffect.CharaAppearLeft;
                            tempSentence.effectParam1 = lines[txtCounter].Substring(2);
                            tempScene.sentences.Add(tempSentence);
                            break;
                        case 'R':
                            tempSentence.effect = Sentence.SentenceEffect.CharaAppearRight;
                            tempSentence.effectParam1 = lines[txtCounter].Substring(2);
                            tempScene.sentences.Add(tempSentence);
                            break;
                        case 'l':
                            tempSentence.effect = Sentence.SentenceEffect.SwayLeft;
                            tempSentence.effectParam1 = lines[txtCounter].Substring(2);
                            tempScene.sentences.Add(tempSentence);
                            break;
                        case 'r':
                            tempSentence.effect = Sentence.SentenceEffect.SwayRight;
                            tempSentence.effectParam1 = lines[txtCounter].Substring(2);
                            tempScene.sentences.Add(tempSentence);
                            break;
                        case '1':
                            tempSentence.effect = Sentence.SentenceEffect.None;
                            currentIndex = 1;
                            currentSpeaker = lines[txtCounter].Substring(2);
                            break;
                        case '2':
                            tempSentence.effect = Sentence.SentenceEffect.None;
                            currentIndex = 2;
                            currentSpeaker = lines[txtCounter].Substring(2);
                            break;
                    }
                }
                txtCounter++;
            }
        }
    }
}
