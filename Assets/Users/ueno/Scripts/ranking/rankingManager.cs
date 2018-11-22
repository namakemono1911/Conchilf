using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankData
{
    public RankData() { }
    public RankData(string n, int s) { name = n; score = s; }
    public string name;
    public int score;
}

public class rankingManager : MonoBehaviour
{
    List<RankData> rankDatas = new List<RankData>();

    // Use this for initialization
    private void Awake()
    {
        //ランキングデータ読み込み
        for (int i = 0; ; i++)
        {
            if (PlayerPrefs.HasKey(i.ToString()))
            {
                var a = PlayerPrefs.GetString(i.ToString());
                var b = PlayerPrefs.GetInt(i.ToString() + a);
                var data = new RankData(a, b);

                rankDatas.Add(data);
            }
            else
            {
                break;
            }
        }
    }

    public void saveRanking()
    {
        int num = 0;
        foreach (var rank in rankDatas)
        {
            PlayerPrefs.SetString(num.ToString(), rank.name);
            PlayerPrefs.SetInt(num.ToString() + rank.name, rank.score);
            num++;
        }
    }

    //ランクインしてるかどうか
    public bool whetherRankin(int data, int range)
    {
        for (int i = 0; i < range; i++)
        {
            if (rankDatas.Count < 3 ||rankDatas[i].score <= data)
            {
                PlayerPrefs.SetInt("rankin", data);
                return true;
            }
        }

        return false;
    }

    //ランキングデータ取得
    public List<RankData> getRanking()
    {
        return rankDatas;
    }

    //ランキングデータ設定
    public bool setRankData(RankData data)
    {
        if (data == null)
            return false;

        rankDatas.Add(data);
        rankDatas.Sort((a, b) => b.score - a.score);

        return true;
    }
}
