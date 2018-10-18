///////////////////////////////////////////////
//
//  Title   : CSV読み書きマネージャ
//  Auther  : Shun Sakai 
//  Date    : 2018/10/05
//  Update  : リファクタリング
//  Memo    : CSVファイルの読み込み
//
///////////////////////////////////////////////
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using Common;

///////////////////////////////////////////////
// 子要素の全取得用クラス
///////////////////////////////////////////////
public static class GetAllChildren
{
    public static List<GameObject> GetAll(this GameObject obj)
    {
        List<GameObject> allChildren = new List<GameObject>();
        GetChildren(obj, ref allChildren);
        return allChildren;
    }

    //子要素を取得してリストに追加
    public static void GetChildren(GameObject obj, ref List<GameObject> allChildren)
    {
        // 子要素のトランスフォームを取得
        Transform children = obj.GetComponentInChildren<Transform>();
      
        // 子要素を全取得
        for( int i = 0; i < obj.transform.childCount; ++i)
        {
            allChildren.Add(obj.transform.GetChild( i ).gameObject);
        }

        return;
    }
}

///////////////////////////////////////////////
// CSV読み書きマネージャ
///////////////////////////////////////////////
public class CsvManager : MonoBehaviour {

    // シリアライズ
    [System.Serializable]
    class Option
    {
        public string       InputCsvName;           // 入力CSVファイルネーム
        public string       OutputCsvName;          // 出力CSVファイルネーム
        public GameObject   enemyCreater;           // エネミークリエイター
    }

    [SerializeField]
    private Option OptionInfo;                      // オプション情報

    private List<string[]>          CsvDate;        // csvデータ

    // インスペクター入力忘れ防止
    private void Awake()
    {
        // CSVファイルネーム未入力防止
        if (OptionInfo.InputCsvName == null)
        {
            OptionInfo.InputCsvName = "EnemyData";
        }
        if (OptionInfo.OutputCsvName == null)
        {
            OptionInfo.OutputCsvName = "EnemyData.csv";
        }

        // CSVデータの初期化作成
        CsvDate = new List<string[]>();
    }

    public void Start()
    {
    }

    // ログの書き込み
    public void CsvOutput()
    {
        // 出力先のファイルパスを作成
        string CSVFilePath = Application.dataPath +"/Resources/" +  OptionInfo.OutputCsvName;

        //　ストリームで書き込み
        using (StreamWriter streamWriter = new StreamWriter(CSVFilePath))
        {
            // カウンタ
            int count = 0;

            // リスト作成
            List<string> stringlist = new List<string>();
           
            // ここらで情報取得&文字列変換
            GetEnemyInfomation(ref stringlist);

            foreach (var list in stringlist)
            {
                // カンマで区切る
                streamWriter.Write(list.ToString() + ',');

                count++;

                // データの総数を書き込んだら改行
                if (count % (int)EnemyAnalyze.Enemy_Param_Max == 0)
                {
                    streamWriter.WriteLine();
                }
            }
        }
    }

    // エネミーの情報を取得
    public void GetEnemyInfomation(ref List<string> stringlist)
    {
        // 子要素をすべて取得
        List<GameObject> childlist = new List<GameObject>();

        childlist = GetAllChildren.GetAll(gameObject);

        enemy.EnemyInfo     buf;
        enemy               E_Script;

        // コンポーネントなど諸々の情報を取得
        foreach (GameObject obj in childlist)
        {
            // スクリプト取得
            E_Script= obj.GetComponent<enemy>();

            // エネミー情報構造体を取得
            buf = E_Script.enemyCSVInfo;

            // モデル識別番号追加
            stringlist.Add(( (int)buf.MODEL_NUMBER ).ToString());

            // ウェーブ番号追加
            stringlist.Add((buf.WAVE_NUMBER).ToString());

            // 移動間秒数追加
            stringlist.Add((buf.MOVE_SECOND).ToString());
            
            // 座標情報を追加
            Vector3 pos = buf.ENEMY_POS;
            stringlist.Add((pos.x).ToString());
            stringlist.Add((pos.y).ToString());
            stringlist.Add((pos.z).ToString());

            // 移動先情報を追加
            Vector3 mpos = buf.ENEMY_MOVE_POS;
            stringlist.Add((mpos.x).ToString());
            stringlist.Add((mpos.y).ToString());
            stringlist.Add((mpos.z).ToString());
        }
    }

    // ログの読み込み
    public void CsvLoad()
    { 
        // Csvデータのクリア
        CsvDate.Clear();
  
        // CSVファイルの読み込み
        TextAsset csvfile;
        csvfile = Resources.Load(OptionInfo.InputCsvName) as TextAsset;
        StringReader reader = new StringReader(csvfile.text);

        // 全取得用リスト
        List<string> stringlist = new List<string>();
        stringlist.Clear();

        while(reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            CsvDate.Add(line.Split(','));
        }
         
    }

    // オブジェクトの全削除
    public void Alldelete()
	{
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Enemyのリストゲッタ
    public List<string[]>CsvDataGet()
    {
        // csvデータの読み込み
        CsvLoad();

        // データが存在しない場合は空のリストを返す
        if(CsvDate == null)
        {
            List<string[]> brank = new List<string[]>();
            return brank;
        }

        return CsvDate;
    }

}