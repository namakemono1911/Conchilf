using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    public ScoreManager(int player) { playerNum = player; }

    private int playerNum;
    private static ScoreData[] data = new ScoreData[2];

    public ScoreData Score
    {
        get { return data[playerNum]; }
    }
}
