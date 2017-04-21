using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CustomMenuTest {

    //[MenuItem("Custom Editor/Create Data Asset")]
    //static void CreateDataAsset() {

    //    //資料 Asset 路徑
    //    string holderAssetPath = "Assets/Resources/";

    //    if (!Directory.Exists(holderAssetPath)) Directory.CreateDirectory(holderAssetPath);

    //    //建立實體
    //    MonsterInfo holder = ScriptableObject.CreateInstance<MonsterInfo>();

    //    //使用 holder 建立名為 dataHolder.asset 的資源
    //    AssetDatabase.CreateAsset(holder, holderAssetPath + "dataHolder.asset");
    //}


    //[MenuItem("Custom Editor/Create AssetBunldes")]
    //static void ExecCreateAssetBunldes() {

    //    // AssetBundle 的資料夾名稱及副檔名
    //    string targetDir = "_AssetBunldes";
    //    string extensionName = ".assetBunldes";

    //    //取得在 Project 視窗中選擇的資源(包含資料夾的子目錄中的資源)
    //    Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

    //    //建立存放 AssetBundle 的資料夾
    //    if (!Directory.Exists(targetDir)) Directory.CreateDirectory(targetDir);

    //    foreach (Object obj in SelectedAsset) {

    //        //資源檔案路徑
    //        string sourcePath = AssetDatabase.GetAssetPath(obj);

    //        // AssetBundle 儲存檔案路徑
    //        string targetPath = targetDir + Path.DirectorySeparatorChar + obj.name + extensionName;

    //        if (File.Exists(targetPath)) File.Delete(targetPath);

    //        if (!(obj is GameObject) && !(obj is Texture2D) && !(obj is Material)) continue;

    //        //建立 AssetBundle
    //        if (BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies)) {

    //            Debug.Log(obj.name + " 建立完成");

    //        } else {

    //            Debug.Log(obj.name + " 建立失敗");
    //        }
    //    }
    //}

}
