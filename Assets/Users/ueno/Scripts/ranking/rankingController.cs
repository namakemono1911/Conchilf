using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rankingController : MonoBehaviour {
    [SerializeField]
    private rankingManager manager;

    [SerializeField]
    private GameObject[] rankers;

	// Use this for initialization
	void Start ()
    {
        var rankData = manager.getRanking();
        int num = 0;

        foreach (var element in rankers)
        {
            for (int i = 0; i < element.transform.childCount; i++)
            {
                var rankinPlayer = element.transform.GetChild(i);
                if (!rankinPlayer.gameObject.activeInHierarchy)
                    continue;

                var texts = rankinPlayer.GetComponentsInChildren<Text>(true);

                if (rankData.Count <= num)
                {
                    texts[0].text = "";
                    texts[1].text = "";
                    texts[2].text = "";
                }
                else
                {
                    texts[0].text = (num + 1).ToString();
                    texts[1].text = rankData[num].name;
                    texts[2].text = rankData[num].score.ToString();
                }
                num++;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
