///////////////////////////////////////////////
//
//  Title   : エネミーマネージャ
//  Auther  : Shun Sakai 
//  Date    : 2018/10/10
//  Update  : リファクタリング
//  Memo    : 
//
///////////////////////////////////////////////
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using Common;

public class EnemyManager : MonoBehaviour {

    // シリアライズ
    [System.Serializable]
    class Option
    {
        public int                  Parametor_AllNum;   // 1オブジェクト当たりのデータ総数
        public GameObject           Enemy00;            // エネミーオブジェクト
        public CsvManager           Csvmanager;         // csvマネージャ
    }

    [SerializeField]
    private Option                  OptionInfo;         // オプション情報

    private List<string[]>          CsvDate;            // csvデータ
    private List<GameObject>        WaveDate;           // ウェーブ毎のデータ

    private enemyTypeManager        E_TypeManager;      // エネミータイプマネージャー
    private enemyAnimationManager   E_AnimManager;      // エネミーアニメーションマネージャー



    // インスペクター入力忘れ防止
    private void Awake()
    {
        if (OptionInfo.Parametor_AllNum < 0)
        {
            OptionInfo.Parametor_AllNum = 0;
        }

        if(OptionInfo.Enemy00 == null)
        {
            OptionInfo.Enemy00 = (GameObject)Resources.Load(Edit.ENEMY_00);
            if(OptionInfo.Enemy00 == null) {
                Debug.LogError("Error [PrefubLoad] : EnemyManager.cs");
                return;
            }
        }

        // リスト作成
        CsvDate  = new List<string[]>();
        WaveDate = new List<GameObject>();

        // コンポーネント取得
        E_TypeManager = gameObject.GetComponent<enemyTypeManager>();
        E_AnimManager = gameObject.GetComponent<enemyAnimationManager>();

    }

    // Use this for initialization
    void Start () {

        // csvデータリストの初期化
        CsvDate.Clear();        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // ウェーブデータ作成
    public void WaveDateMake(int WaveNum)
    {
        // ゲームオブジェクト用
        GameObject  obj;
        EnemyInfo   buf = new EnemyInfo();
        
        // ゲームオブジェクトの全削除
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        // csvデータリストの初期化
        CsvDate.Clear();
        // csvデータの取得
        CsvDate = OptionInfo.Csvmanager.CsvDataGet();

        // 解析（Csvデータの末尾まで）
        for (int index = 0; index < CsvDate.Count; index++)
        {
            // ウェーブ番号識別
            string wave = CsvDate[index][(int)EnemyAnalyze.Enemy_Wave];

            // ウェーブ数確認
            if (int.Parse(wave) == WaveNum)
            {
                // 識別番号取得
                int id = int.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_Type]);
                buf.MODEL_NUMBER = (enemyTypeManager.ENEMY_TYPE)Enum.ToObject(typeof(enemyTypeManager.ENEMY_TYPE), id);

                // 移動間秒数取得
                buf.MOVE_SECOND = int.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_MoveSec] );

                // 生成座標を取得
                Vector3 pos;
                pos.x = float.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_Pos_x]);
                pos.y = float.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_Pos_y]);
                pos.z = float.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_Pos_z]);
                buf.ENEMY_POS = pos;

                // 移動先情報を取得
                Vector3 Mpos;
                Mpos.x = float.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_Move_x]);
                Mpos.y = float.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_Move_y]);
                Mpos.z = float.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_Move_z]);
                buf.ENEMY_MOVE_POS = Mpos;
                
                // エネミー生成
                obj = Instantiate(OptionInfo.Enemy00);

                // エネミー情報セット
                obj.GetComponent<enemy>().setEnemyInfo(buf);

                // エネミータイプマネージャー
                obj.GetComponent<enemy>().setEnemyTypeManager(E_TypeManager);
                // アニメマネージャをセット
                obj.GetComponent<enemyAnimation>().setEnemyAnimManager(E_AnimManager);

                // エネミーの追加
                WaveDate.Add(obj);
            }

        }

        // 全オブジェクトをnonactiveに
    }
}
