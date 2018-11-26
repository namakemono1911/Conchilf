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

    private string evaluation = "SABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private int score;

	// Use this for initialization
	void Start ()
    {
        score = PlayerPrefs.GetInt(scoreType.SUM_SCORE.ToString() + playerNum.ToString());

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
