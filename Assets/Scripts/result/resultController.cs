using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class resultPair
{
    public string resultName;
    public Text numText;
    public Text scoreText;
}

enum AnimationType
{
    numAnime = 0,
    scoreAnime,
    waitAnime,

};

public class resultController : MonoBehaviour
{
    [SerializeField]
    private resultPair[] pairs;                 //リザルト表示の情報

    [SerializeField]
    private AnimationType[] animationOrder;     //アニメーションの順番

    [SerializeField]
    private float waitTime;                     //待ち時間

    private resultAnimation anime = null;       //アニメーション処理

    private float startTime;                    //開始時間

    private int nowAnimationNum = 0;            //今再生してるアニメーションのインデックス

    public resultPair[] ResultPair
    {
        get { return pairs; }
    }

	// Use this for initialization
	void Start ()
    {
        startTime = Time.time;

        foreach (var pair in pairs)
        {
            pair.numText.gameObject.SetActive(false);
            pair.scoreText.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void nextAnimation()
    {
        switch (animationOrder[++nowAnimationNum])
        {
            case AnimationType.numAnime:
                anime = new numAnimation(this);
                break;

            case AnimationType.scoreAnime:
                //CSV読み込み
                break;

            case AnimationType.waitAnime:
                anime = new waitAnimation(this, waitTime);
                break;
        }
    }
}
