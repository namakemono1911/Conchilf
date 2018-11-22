using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CountResultData
{
    public ScoreCount type;     //データタイプ
    public Text scoreText;      //データを書き込むテキスト
}

public class CountResult : MonoBehaviour
{
    [SerializeField]
    private int playerNum;

    [SerializeField]
    private CountResultData[] data;

    private ScoreManager score;
    
    private void Start()
    {
        score = new ScoreManager(playerNum);

        //データ読み込み
        for (int i = 0; i < data.Length; i++)
        {
            data[i].scoreText.text = score.Score.CountScore[(int)data[i].type].ToString();
        }
    }
}
