///////////////////////////////////////////////
//
//  Title   : エネミーシーンマネージャ
//  Auther  : Shun Sakai 
//  Date    : 2018/11/12
//  Update  : 
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// エネミーシーン管理マネージャ
public class EnemySceneManager : MonoBehaviour {

    // シリアライズ
    [System.Serializable]
    class Option
    {
        public EnemyManager Enemy_manager;  // エネミーマネージャ
    }

    [SerializeField]
    private Option SerializeMember; // インスペクタ上のメンバ

    // 内部メンバ
    private int CurrentSceneNum;    // 現在のシーン番号
    private int CurrentWaveNum;     // 現在のウェーブ番号

    // 初期処理
    private void Awake()
    {
        if (SerializeMember.Enemy_manager == null)
        {
            // エネミーマネージャを探索
            SerializeMember.Enemy_manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
            if (SerializeMember.Enemy_manager == null)
            {
                Debug.LogError("Error [GameObject.Find] : EnemyManager");
                return;
            }

            // 初期化
            CurrentSceneNum = 0;
            CurrentWaveNum  = 0;

        }
    }


    // 初回処理
    void Start () {

        // 初期化
        CurrentSceneNum = 0;
        CurrentWaveNum = 0;
        
    }

    // 更新処理
    void Update () {
		
    }

    /////////////////////////////
    //  外部用メソッド
    ////////////////////////////

    // ボタン用
    public void DebugTest()
    {
        StartEnemyScene();
    }

    public void DebugNextWave()
    {
        EnemyWaveNext();
    }

    public void DebugNextScene()
    {
        EnemySceneNext();
    }


    // エネミーシーンの再生開始
    public bool StartEnemyScene()
    {
        // 初期化
        CurrentSceneNum = 0;
        CurrentWaveNum = 0;

        // シーンデータの作成
        if (SceneDateMake() == false) { return false; }

        // ウェーブのアクティブ化
        if (WaveActive() == false) { return false; }

        return true;
    }

    // シーン番号の移行(次回シーンデータが存在しない場合は false　を返す)
    public bool EnemySceneNext()
    {
        // ウェーブ番号の初期化
        CurrentWaveNum = 0;

        // 次シーンへ移行
        CurrentSceneNum++;

        // シーンデータの作成
        if(SceneDateMake() == false) { return false; }

        // ウェーブのアクティブ化
        if(WaveActive() == false) { return false; }

        return true;
    }

    // ウェーブ番号の移行( 次回ウェーブが存在しない場合は false を返す)
    public bool EnemyWaveNext()
    {
        // 次ウェーブへ移行
        CurrentWaveNum++;

        // ウェーブのアクティブ化
        if (WaveActive() == false) { return false; }

        return true;
    }

    // シーン番号の指定をして再生
    public void EnemySceneNextToIndex(int SceneIndex)
    {
        // ウェーブ番号の初期化
        CurrentWaveNum = 0;

        // 次シーンへ移行
        CurrentSceneNum = SceneIndex;

        // シーンデータの作成
        SceneDateMake();

        // ウェーブのアクティブ化
        WaveActive();
    }

    // シーン上のエネミーを全削除
    public void EnemyAllDelete()
    {
        // エネミーの全削除
        SerializeMember.Enemy_manager.EnemyAllDelete();
    }

    // 敵が全滅したか
    public bool EnemyAllDead()
    {
        return SerializeMember.Enemy_manager.SceneEnemyAllDead();
    }

    /////////////////////////////
    //  クラス内メソッド
    ////////////////////////////

    // シーンデータの作成
    private bool SceneDateMake()
    {
        // シーンデータの作成
        if (SerializeMember.Enemy_manager.SceneDataMake(CurrentSceneNum) == false)
        {
            return false;
        }

        return true;
    }
    
    // ウェーブのアクティブ化
    private bool WaveActive()
    {
        if( SerializeMember.Enemy_manager.WaveEnemyActiveOnTrue(CurrentWaveNum) == false )
        {
            return false;
        }

        return true;
    }


}
