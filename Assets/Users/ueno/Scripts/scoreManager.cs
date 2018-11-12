using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum scoreType
{
    DEFEAT_NUM = 0,
    ARREST_NUM,
    PORICE_POW,
    ACCURACY,
    DOWN_NUM,
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
    private int totalShot;
    private int hitShot;
    private int numPlayer;

    public void setScore(scoreType type, int score)
    {
        scoreDict[type] = score;
    }

    public void addScore(scoreType type)
    {
        scoreDict[type]++;
    }

    public void shot(bool isHit)
    {
        totalShot++;
        if (isHit)
            hitShot++;
    }

    public void calcResult()
    {
        float parcent = (float)hitShot / totalShot;
        scoreDict[scoreType.ACCURACY] = (int)(parcent * 100.0f);
    }

    public void save()
    {
        calcResult();

        foreach (var element in scoreDict)
            PlayerPrefs.SetInt(element.Key.ToString() + numPlayer.ToString(), element.Value);
    }

    public void load()
    {
        foreach (var element in scoreDict)
            scoreDict[element.Key] = PlayerPrefs.GetInt(element.Key.ToString() + numPlayer);
    }
}