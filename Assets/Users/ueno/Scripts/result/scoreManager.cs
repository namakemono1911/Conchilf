using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum scoreType
{
    DEFEAT_NUM = 0,
    ARREST_NUM,
    SHOT_NUM,
    HIT_NUM,
    ACCURACY,
    DOWN_NUM,
    SUM_SCORE,
    TYPE_MAX
};

public class playerScore
{
    public playerScore(int num)
    {
        numPlayer = num;
        for (int i = 0; i < (int)scoreType.TYPE_MAX; i++)
            scoreDict[(scoreType)i] = 0;
    }
    private Dictionary<scoreType, int> scoreDict = new Dictionary<scoreType, int>();
    private int numPlayer;
    public static int[] baseScore = new int[]
        {
            50,
            100,
            0,
            0,
            30,
            -1000
        };

    public void setScore(scoreType type, int score)
    {
        scoreDict[type] = score;
    }

    public void addScore(scoreType type)
    {
        scoreDict[type]++;
    }

    public void calcResult()
    {
        float parcent = (float)scoreDict[scoreType.HIT_NUM] / scoreDict[scoreType.SHOT_NUM];
        scoreDict[scoreType.ACCURACY] = (int)(parcent * 100.0f);

        int sum = 0;
        for (int i = 0; i < (int)scoreType.SUM_SCORE; i++)
            sum += scoreDict[(scoreType)i] * baseScore[i];
        scoreDict[scoreType.SUM_SCORE] = sum;
    }

    public void save()
    {
        calcResult();

        foreach (var element in scoreDict)
            PlayerPrefs.SetInt(element.Key.ToString() + numPlayer.ToString(), element.Value);
    }

    public void load()
    {
        for (int i = 0; i < (int)scoreType.TYPE_MAX; i++)
            scoreDict[(scoreType)i] = PlayerPrefs.GetInt(((scoreType)i).ToString() + numPlayer.ToString());
    }
}