using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreAnimation : resultAnimation
{
    private int[] playerScore;
    private List<int> scoreList;

    public scoreAnimation(resultController r, int[] score) : base(r)
    {
        foreach (var pair in result.ResultPair)
        {
            pair.scoreText.gameObject.SetActive(true);
            pair.scoreText.text = "0";
            scoreList.Add(0);
        }

        playerScore = score;
    }

    public override void animation()
    {
        for ( int i = 0; i < scoreList.Count; i++)
        {
            if (scoreList[i] >= playerScore[i])
                continue;

            //カウントアップ
            scoreList[i]++;
            result.ResultPair[i].scoreText.text = scoreList[i].ToString();
        }
    }

    public override void skipAnimation()
    {
        int i = 0;
        foreach (var pair in result.ResultPair)
            pair.scoreText.text = playerScore[i++].ToString();

        isAnime = false;
    }
}
