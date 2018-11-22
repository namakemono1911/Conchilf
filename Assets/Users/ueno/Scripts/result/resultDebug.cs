using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultDebug : MonoBehaviour
{
    private ScoreManager[] manager;

	// Use this for initialization
	void Start ()
    {
        manager[0] = new ScoreManager(1);
        manager[1] = new ScoreManager(2);

        for (int i = 0; i < 100; i++)
        {
            manager[0].Score.addCount(ScoreCount.SHOT_CNT);
            manager[1].Score.addCount(ScoreCount.SHOT_CNT);
            if (i % 2 == 0)
            {
                manager[0].Score.addCount(ScoreCount.HIT_CNT);
                manager[0].Score.addCount(ScoreCount.HIT_CNT);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
