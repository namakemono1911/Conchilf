﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultDebug : MonoBehaviour
{
    private playerScore score = new playerScore(1);

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < 100; i++)
        {
            if (i % 2 == 0)
                score.shot(true);
            else
                score.shot(false);
        }

        for (int i = 0; i < (int)scoreType.SUM_SCORE; i++)
        {
            score.setScore((scoreType)i, i * 10);
        }

        score.save();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
