using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankData
{
    public string name;
    public uint score;
}

public class rankingManager : MonoBehaviour
{
    private static List<RankData> rankDatas;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //ランクインしてるかどうか
    public bool whetherRankin(RankData data)
    {
        foreach (var rank in rankDatas)
        {
            if (rank.score >= data.score)
                return true;
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
        int num = 0;
        foreach (var rank in rankDatas)
        {
            if (rank.score >= data.score)
            {
                rankDatas[num] = data;
                return true;
            }
            num++;
        }

        return false;
    }

}
