using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CreateCustomObject {

    [MenuItem("Assets/Create/MonsterInfo")]
    static void CreateMonsterInfo() {

        //資料 Asset 路徑
        string assetPath = "Assets/Resources/Monster/";

        if (!Directory.Exists(assetPath)) Directory.CreateDirectory(assetPath);

        //建立實體
        MonsterInfo info = ScriptableObject.CreateInstance<MonsterInfo>();

        //使用 holder 建立名為 dataHolder.asset 的資源
        AssetDatabase.CreateAsset(info, assetPath + "MonsterInfo.asset");
    }

    [MenuItem("Assets/Create/CharaInfo")]
    static void CreateCharaInfo() {

        //資料 Asset 路徑
        string assetPath = "Assets/Resources/Chara/";

        if (!Directory.Exists(assetPath)) Directory.CreateDirectory(assetPath);

        //建立實體
        CharaInfo info = ScriptableObject.CreateInstance<CharaInfo>();

        //使用 holder 建立名為 dataHolder.asset 的資源
        AssetDatabase.CreateAsset(info, assetPath + "CharaInfo.asset");
    }

    [MenuItem("Assets/Create/Story")]
    static void CreateStory() {

        //資料 Asset 路徑
        string assetPath = "Assets/Resources/Story/";

        if (!Directory.Exists(assetPath)) Directory.CreateDirectory(assetPath);

        //建立實體
        Story story = ScriptableObject.CreateInstance<Story>();

        //使用 holder 建立名為 dataHolder.asset 的資源
        AssetDatabase.CreateAsset(story, assetPath + "Story.asset");
    }


    [MenuItem("Assets/Create/Dictionary")]
    static void CreateDictionary() {

        //資料 Asset 路徑
        string assetPath = "Assets/Resources/Dictionary/";

        if (!Directory.Exists(assetPath)) Directory.CreateDirectory(assetPath);

        //建立實體
        SDictionary<string, Sprite> story = ScriptableObject.CreateInstance<SDictionary<string, Sprite>>();
        
        //使用 holder 建立名為 dataHolder.asset 的資源
        AssetDatabase.CreateAsset(story, assetPath + "Dictionary.asset");
    }
}
