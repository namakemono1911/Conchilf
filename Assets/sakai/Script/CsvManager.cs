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
        public string   InputCsvName;           // 入力CSVファイルネーム
        public string   OutputCsvName;          // 出力CSVファイルネーム
        public int      Parametor_AllNum;       // 1オブジェクト当たりのデータ総数
    }

    [SerializeField]
    private Option OptionInfo;  // オプション情報

    // インスペクター入力忘れ防止
    private void Awake()
    {
        if( OptionInfo.InputCsvName == null)
        {
            OptionInfo.InputCsvName = "EnemyData.csv";
        }
        if (OptionInfo.OutputCsvName == null)
        {
            OptionInfo.OutputCsvName = "EnemyData.csv";
        }
        if (OptionInfo.Parametor_AllNum < 0)
        {
            OptionInfo.Parametor_AllNum = 0;
        }
    }


    // ログの書き込み
    public void CsvOutput()
    {
        // 出力先のファイルパスを作成
        string CSVFilePath = Application.dataPath +"/CSV/" +  OptionInfo.OutputCsvName;

        //　ストリームで書き込み
        using (StreamWriter streamWriter = new StreamWriter(CSVFilePath))
        {
            // カウンタ
            int count = 0;

            // リスト作成
            List<string>    stringlist = new List<string>();
           
            // ここらで情報取得&文字列変換
            GetEnemyInfomation(ref stringlist);

            foreach (var list in stringlist)
            {
                // カンマで区切る
                streamWriter.Write(list.ToString() + ',');

                count++;

                // データの総数を書き込んだら改行
                if (count % OptionInfo.Parametor_AllNum == 0)
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

        // コンポーネントなど諸々の情報を取得
        foreach (GameObject obj in childlist)
        {
            // 座標取得しますよ
            Vector3 pos = obj.transform.position;

            // ストリング型に変更しろ
            stringlist.Add((pos.x).ToString());
            stringlist.Add((pos.y).ToString());
            stringlist.Add((pos.z).ToString());
        }
    }

    // ログの読み込み
    public void CsvRoad()
    {
        // 読み込むCSVファイル名
        string CSVFilePath = Application.dataPath + OptionInfo.OutputCsvName;

        //　ストリームで読み込み
        using (StreamReader streamReader = new StreamReader(CSVFilePath))
        {
            // 文字列リスト読み込み
            List<string> lists = new List<string>();

            while (!streamReader.EndOfStream)
            {
                lists.AddRange(streamReader.ReadLine().Split(','));
            }

            // listsの解析（アナライズ）
        }
     }


    // オブジェクトの全削除

}