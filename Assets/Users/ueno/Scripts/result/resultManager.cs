using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultManager : MonoBehaviour
{
    [SerializeField]
    rankingManager ranking;             //ランキング

    [SerializeField]
    textAnimationController animation;  //アニメーション

    private string nextScene;           //次のシーン名

    private void Start()
    {
        RankData data = new RankData();

        if (ranking.whetherRankin(data))
            nextScene = "inputName";
        else
            nextScene = "ranking";
    }

    private void Update()
    {
        if (!animation.IsAnimation)
        {

        }
    }
}
