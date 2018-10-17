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
        public int      WaveNumber;             // Wave数   
    }

    [SerializeField]
    private Option OptionInfo;          // オプション情報

    private List<string[]> CsvDate;     // csvデータ

    // インスペクター入力忘れ防止
    private void Awake()
    {
        // CSVファイルネーム未入力防止
        if( OptionInfo.InputCsvName == null)
        {
            OptionInfo.InputCsvName = "EnemyData.csv";
        }
        if (OptionInfo.OutputCsvName == null)
        {
            OptionInfo.OutputCsvName = "EnemyData.csv";
        }

        // 規定値外対応
        if (OptionInfo.Parametor_AllNum < 0)
        {
            OptionInfo.Parametor_AllNum = 0;
        }
        if (OptionInfo.WaveNumber < 0)
        {
            OptionInfo.WaveNumber = 0;
        }

        // CSVデータの削除
        CsvDate = new List<string[]>();
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
            Vector3     pos = obj.transform.position;
            Quaternion  qua = obj.transform.rotation;

            // wave数情報を保存
            stringlist.Add((OptionInfo.WaveNumber).ToString());

            // 座標情報を追加
            stringlist.Add((pos.x).ToString());
            stringlist.Add((pos.y).ToString());
            stringlist.Add((pos.z).ToString());

            // 角度情報を追加
            stringlist.Add((qua.x).ToString());
            stringlist.Add((qua.y).ToString());
            stringlist.Add((qua.z).ToString());
            stringlist.Add((qua.w).ToString());

        }
    }

    // ログの読み込み
    public void CsvLoad()
    {
        // 読み込むCSVファイル名
        //string CSVFilePath = Application.dataPath + OptionInfo.OutputCsvName;

        // Csvデータのクリア
        CsvDate.Clear();

        
        TextAsset csvfile;
        csvfile = Resources.Load(OptionInfo.OutputCsvName) as TextAsset;
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