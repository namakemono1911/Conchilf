///////////////////////////////////////////////
//
//  Title   : ボスシーンマネージャ
//  Auther  : Shun Sakai 
//  Date    : 2018/11/19
//  Update  : ボスシーン管理用
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボスシーンマネージャ
public class BossSceneManager : MonoBehaviour {

    // シリアライズ
    [System.Serializable]
    class Option
    {
        public List< GameObject>                    EnemyList;      // エネミーリスト
        public List<BossSceneDramaPart.DramaInfo>   EndingList;     // EDドラマリスト
        public AudioSource                          SceneMusic;     // シーンBGM
        public BossSceneDramaPart                   DramaManager;   // ドラママネージャ
    }

    // シリアリズメンバ
    [SerializeField]
    private Option OptionInfo;  // オプション情報
    
    // 通常メンバ
    private bool DramaEnable        = false;    // ボス撃破フラグ
    private bool BossUpdate_Enable  = true;
    
    private void Awake()
    {
        // フラグ初期化
        DramaEnable         = false;
        BossUpdate_Enable   = true;

    }

    // Use this for initialization
    void Start ()
    {

        
    }
	
	// 更新処理
	void Update ()
    {
        // 更新フラグ
        if (BossUpdate_Enable == true)
        {
            // ドラマ再生中に終了フラグ確認
            if (DramaEnable == true)
            {
                if (OptionInfo.DramaManager.Get_DramaPlayEnable() == false)
                {
                    // シーン遷移
                    GotoNextScene();
                    BossUpdate_Enable = false;
                }
            }
            
            // ボス全滅確認
            if (EnemyAllDeth() && DramaEnable == false)
            {
                if (OptionInfo.SceneMusic.volume >= 0.2f)
                {
                    OptionInfo.SceneMusic.volume -= 0.02f;
                }
                else
                {
                    // エンディングドラマ再生
                    EndScenePlay_Enable();

                    // ドラマ再生フラグをtrueに
                    DramaEnable = true;
                }
            }
        }

        else if (BossUpdate_Enable == false)
        {
            // ボリュームを下げるんだよぉ！
            if (!sceneManager.Instance.isFadeIn())
            {
                if (sceneManager.Instance.isFade())
                {
                    OptionInfo.SceneMusic.volume -= 0.01f;
                }
            }
        }        
	}

    // スタートシーン処理
    private void EndScenePlay_Enable()
    {
        // エンディングリスト再生開始
        OptionInfo.DramaManager.SetDramaDate_And_Play( OptionInfo.EndingList);
        
    }


    // ボスが全滅したか確認
    private bool EnemyAllDeth()
    {
        for(int i = 0; i <  OptionInfo.EnemyList.Count; i++)
        {
            if( OptionInfo.EnemyList[i] != null)
            {
                return false;
            }
            
        }
        return true;
    }

    // シーン遷移
    private void GotoNextScene()
    {
        //プレイヤースコア保存
        var players = GameObject.Find("UICanvasHight").GetComponent<havePlayerNum>().player;
        foreach (var player in players)
            player.saveScore();

        // プレイヤー数取得
        int PlayerNum = GameObject.Find("UICanvasHight").GetComponent<havePlayerNum>().numPlayer;


        if (PlayerNum == 1)
        {
            // シーンマネージャ 一人用
            sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_RESULT_1);
        }
        else
        {
            // シーンマネージャ　二人用
            sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_RESULT_2);
        }
    }

}
