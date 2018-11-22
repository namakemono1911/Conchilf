using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum scoreType
{
    DEFEAT_SCORE = 0,
    ARREST_SCORE,
    ACCURACY_SCORE,
    DOWN_SCORE,
    SUM_SCORE,
    TYPE_MAX
};

public enum ScoreCount
{
    DEFEAT_CNT = 0,
    ARREST_CNT,
    SHOT_CNT,
    HIT_CNT,
    ACCURACY,
    DOWN_CNT,
    CNT_MAX
}

public class ScoreData
{
    private int numPlayer;
    private int[] cntScore = new int[(int)ScoreCount.CNT_MAX];
    private int[] scores = new int[(int)scoreType.TYPE_MAX];
    public int[] scoreBase = new int[(int)scoreType.TYPE_MAX];

    public int[] CountScore
    {
        get { return cntScore; }
    }

    public int[] Scores
    {
        get { return scores; }
    }

    //加算
    public void addCount(ScoreCount countType)
    {
        cntScore[(int)countType]++;
    }

    //計算
    public void calcResult()
    {
        //命中率計算
        float parcent = (float)cntScore[(int)ScoreCount.HIT_CNT] / cntScore[(int)ScoreCount.SHOT_CNT];
        cntScore[(int)ScoreCount.ACCURACY] = (int)(parcent * 100.0f);

        //スコア計算
        scores[(int)scoreType.DEFEAT_SCORE] = cntScore[(int)ScoreCount.DEFEAT_CNT] * scoreBase[(int)scoreType.DEFEAT_SCORE];
        scores[(int)scoreType.ARREST_SCORE] = cntScore[(int)ScoreCount.ARREST_CNT] * scoreBase[(int)scoreType.ARREST_SCORE];
        scores[(int)scoreType.ACCURACY_SCORE] = cntScore[(int)ScoreCount.ACCURACY] * scoreBase[(int)scoreType.ACCURACY_SCORE];
        scores[(int)scoreType.DOWN_SCORE] = cntScore[(int)ScoreCount.DOWN_CNT] * scoreBase[(int)scoreType.DOWN_SCORE];

        //総合スコア
        scores[(int)scoreType.SUM_SCORE] = 0;
        for (int i = 0; i < (int)scoreType.SUM_SCORE; i++)
            scores[(int)scoreType.SUM_SCORE] += scores[i];
    }
}