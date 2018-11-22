using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEvaluation : MonoBehaviour
{
    [SerializeField]
    private int playerNum;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private int[] evalutionValue;

    private string evaluation = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private ScoreManager scoreManager;

    private int score;

	// Use this for initialization
	void Start ()
    {
        scoreText.text = "";
        scoreManager = new ScoreManager(playerNum);
        score = scoreManager.Score.Scores[(int)scoreType.SUM_SCORE];

        for (int i = 0; i < evalutionValue.Length; i++)
        {
            if (scoreEval(i))
            {
                scoreText.text = "";
                scoreText.text += evaluation[i];
                break;
            }
        }
	}

    bool scoreEval(int num)
    {
        if (evalutionValue[num] <= score)
            return true;

        return false;
    }
}
