using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRanking : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RankData[] rank = { new RankData("yunsu", 810), new RankData("aira", 114514), new RankData("syou", 1919) };
        var manager = GetComponent<rankingManager>();
        foreach (var a in rank)
            manager.setRankData(a);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
