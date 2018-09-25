///////////////////////////////////////////////
//
//  Title   : マネージャシーン自動ロード
//  Auther  : Shun Sakai 
//  Date    : 2018/08/16
//  Update  : リファクタリング
//  Memo    : マネージャシーンをAwake処理前にロードします
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Common;

// マネージャーシーンの自動生成
public class ManagerSceneAutoLoader : MonoBehaviour {

    //ゲーム開始時(シーン読み込み前)に実行される
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    // マネージャーシーンを読み込み
    private static void LoadManagerScene()
    {
        string managerSceneName = SceneName.MANAGER_SCENE;

        //ManagerSceneが有効でない時(まだ読み込んでいない時)だけ追加ロードするように
        if (!SceneManager.GetSceneByName(managerSceneName).IsValid())
        {
            SceneManager.LoadScene(managerSceneName, LoadSceneMode.Additive);
        }
    }
}
