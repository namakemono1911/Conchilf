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
        public int Parametor_AllNum;        // 1オブジェクト当たりのデータ総数
        public GameObject   Enemy00;        // エネミーオブジェクト
        public CsvManager Csvmanager;       // csvマネージャ
    }

    [SerializeField]
    private Option OptionInfo;              // オプション情報

    private List<string[]>      CsvDate;    // csvデータ
    private List<GameObject>    WaveDate;   // ウェーブ毎のデータ


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
    }

    // Use this for initialization
    void Start () {

        // csvデータリストの初期化
        CsvDate.Clear();
        // csvデータの取得
        CsvDate = OptionInfo.Csvmanager.CsvDataGet();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // ウェーブデータ作成
    public void WaveDateMake(int WaveNum)
    {
        // ゲームオブジェクト用
        GameObject  buf;
        Vector3     pos;

        // ゲームオブジェクトの全削除
        // WaveDate. 
        
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        // csvデータリストの初期化
        CsvDate.Clear();
        // csvデータの取得
        CsvDate = OptionInfo.Csvmanager.CsvDataGet();

        // 解析
        for (int index = 0; index < CsvDate.Count; index++)
        {
            // 初期化
            buf = null;
            pos = Vector3.zero;

            string wave = CsvDate[index][0];

            // ウェーブ数確認
            if (int.Parse(wave) == WaveNum)
            {
                // エネミー生成
                buf = Instantiate(OptionInfo.Enemy00);

                // エネミーの座標データを追加
                pos.x = int.Parse( CsvDate[index][1]);
                pos.y = int.Parse( CsvDate[index][2]);
                pos.z = int.Parse( CsvDate[index][3]);

                buf.transform.position = pos;
                
                // エネミーの追加
                WaveDate.Add(buf);
            }

        }

        // 全オブジェクトをnonactiveに
    }
}
