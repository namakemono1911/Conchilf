using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ResultData
{
    public scoreType type;      //データタイプ
    public Text scoreText;      //データを書き込むテキスト
}

public class ResultTextData : MonoBehaviour
{
    [SerializeField]
    private int playerNum;                  //プレイヤー番号

    [SerializeField]
    private int[] basicValue = new int[(int)scoreType.TYPE_MAX];                 //基礎値

    [SerializeField]
    private ResultData[] resultDatas;       //データ

    private void Start()
    {
        //データ読み込み
        for (int i = 0; i < resultDatas.Length; i++)
        {
            var score = PlayerPrefs.GetInt(resultDatas[i].type.ToString() + playerNum.ToString());
            score *= basicValue[i];
            resultDatas[i].scoreText.text = score.ToString();
        }
    }
}
