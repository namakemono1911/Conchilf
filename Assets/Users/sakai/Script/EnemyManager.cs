///////////////////////////////////////////////
//
//  Title   : エネミーマネージャ
//  Auther  : Shun Sakai 
//  Date    : 2018/10/10
//  Update  : 2018/11/12
//  Memo    : シーン事に生成する機能を追加、ウェーブ単位でのアクティブ切り替え追加
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
        public GameObject          Enemy00;            // エネミーオブジェクト
        public CsvManager          Csvmanager;         // csvマネージャ
        public GameObject          enemyCreater;       // エネミークリエイター
    }

    [SerializeField]
    private Option                  OptionInfo;         // オプション情報

    private List<string[]>          CsvDate;            // csvデータ
    private List<GameObject>        WaveDate;           // ウェーブ毎のデータ
    private List<GameObject>        SceneDate;           // ウェーブ毎のデータ

    // インスペクター入力忘れ防止
    private void Awake()
    {
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
        SceneDate = new List<GameObject>();
    }

    // 初回処理
    void Start () {

        // csvデータリストの初期化
        CsvDate.Clear();        
    }
	
	// 更新処理
	void Update () {

	}

    // シーンデータの作成
    public bool SceneDataMake(int SceneNum)
    {
        // エネミー情報構造体
        enemy.EnemyInfo buf = new enemy.EnemyInfo();

        // EnemyManager以下のオブジェクトを全削除
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        // csvデータリストの初期化
        CsvDate.Clear();
        // csvデータの取得
        CsvDate = OptionInfo.Csvmanager.CsvDataGet();

        // リスト上のシーンデータを削除
        SceneDate.Clear();

        // エネミー生成時に付与するマネージャを取得
        enemyTypeManager E_Type = OptionInfo.enemyCreater.GetComponent<EnemyCreater>().GetEnemyTypeManager();
        enemyAnimationManager E_Anim = OptionInfo.enemyCreater.GetComponent<EnemyCreater>().GetEnemyAnimationManager();

        // 解析（Csvデータの末尾まで）
        for (int index = 0; index < CsvDate.Count; index++)
        {
            // シーン番号識別
            string Scene = CsvDate[index][(int)EnemyAnalyze.Enemy_Scene];

            // シーン数確認
            if (int.Parse(Scene) == SceneNum)
            {
                GameObject obj = null;

                // 識別番号取得
                int id = int.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_Type]);
                buf.MODEL_NUMBER = (enemyTypeManager.ENEMY_TYPE)Enum.ToObject(typeof(enemyTypeManager.ENEMY_TYPE), id);

                // 移動間秒数取得
                buf.MOVE_SECOND = float.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_MoveSec]);

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

                // ウェーブ数を取得
                buf.WAVE_NUMBER = int.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_Wave]);

                // エネミー生成
                obj = Instantiate(OptionInfo.Enemy00, pos, Quaternion.identity);

                // 生成フラグをたてる
                obj.GetComponent<enemy>().createMeFrag();

                // エネミー情報セット
                obj.GetComponent<enemy>().setEnemyInfo(buf);

                // エネミータイプマネージャー
                obj.GetComponent<enemy>().setEnemyTypeManager(E_Type);
                // アニメマネージャをセット
                obj.GetComponent<enemyAnimation>().setEnemyAnimManager(E_Anim);

                // エネミーの追加
                SceneDate.Add(obj);

                // 子供として追加
                obj.transform.parent = transform;

                // Activeをfalseに
                obj.SetActive(false);
            }

        }

        if (SceneDate.Count == 0) { return false; }
        return true;
    }

    // 指定ウェーブのエネミーアクティブ化
    public bool WaveEnemyActiveOnTrue(int WaveIndex)
    {
        if(SceneDate == null){return false;}

        // 変数
        int WaveNum = 0;
        int Count   = 0;

        // 現在読み込んでいるシーンデータから指定したウェーブのエネミーを生成
        for(int i = 0; i < SceneDate.Count;i++)
        {
            // ウェーブデータの確認
            WaveNum = SceneDate[i].GetComponent<enemy>().getEnemyInfo().WAVE_NUMBER;
            
            // 指定したウェーブ数か
            if(WaveNum == WaveIndex)
            {
                // カウントアップ
                Count++;
                // 該当ゲームオブジェクトをActiveにする？
                SceneDate[i].SetActive(true);

            }

        }

        if( Count == 0) { return false; }
        return true;
        
    }

    // 指定シーンのエネミーに生き残りがいるか
    public bool SceneEnemyAllDead()
    {
        // シーンデータの確認
        if (SceneDate == null) { return　false; }

        // シーンデータリストが空だったら全滅
        if (SceneDate.Count == 0) { return true; }
        
        return false;
    }

    // 現在の敵を全削除
    public void EnemyAllDelete()
    {
        // EnemyManager以下のオブジェクトを全削除
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    
    }

    // ウェーブデータ作成（旧メソッドのため使用禁止）
    public void WaveDateMake(int WaveNum)
    {
        // ゲームオブジェクト用
        
        enemy.EnemyInfo   buf = new enemy.EnemyInfo();
        
        // ゲームオブジェクトの全削除
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        // csvデータリストの初期化
        CsvDate.Clear();
        // csvデータの取得
        CsvDate = OptionInfo.Csvmanager.CsvDataGet();
        
        enemyTypeManager        E_Type = OptionInfo.enemyCreater.GetComponent<EnemyCreater>().GetEnemyTypeManager();
        enemyAnimationManager   E_Anim = OptionInfo.enemyCreater.GetComponent<EnemyCreater>().GetEnemyAnimationManager();


        // 解析（Csvデータの末尾まで）
        for (int index = 0; index < CsvDate.Count; index++)
        {
            // ウェーブ番号識別
            string wave = CsvDate[index][(int)EnemyAnalyze.Enemy_Wave];

            // ウェーブ数確認
            if (int.Parse(wave) == WaveNum)
            {
                GameObject obj = null;

                // 識別番号取得
                int id = int.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_Type]);
                buf.MODEL_NUMBER = (enemyTypeManager.ENEMY_TYPE)Enum.ToObject(typeof(enemyTypeManager.ENEMY_TYPE), id);

                // 移動間秒数取得
                buf.MOVE_SECOND = float.Parse(CsvDate[index][(int)EnemyAnalyze.Enemy_MoveSec] );

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
                obj = Instantiate(OptionInfo.Enemy00, pos, Quaternion.identity);

				// 生成フラグをたてる
				obj.GetComponent<enemy>().createMeFrag();

                // エネミー情報セット
                obj.GetComponent<enemy>().setEnemyInfo(buf);

                // エネミータイプマネージャー
                obj.GetComponent<enemy>().setEnemyTypeManager(E_Type);
                // アニメマネージャをセット
                obj.GetComponent<enemyAnimation>().setEnemyAnimManager(E_Anim);

                // エネミーの追加
                WaveDate.Add(obj);

                // 子供として追加
                obj.transform.parent = transform;
            }

        }

        // 全オブジェクトをnonactiveに
    }
    
}
