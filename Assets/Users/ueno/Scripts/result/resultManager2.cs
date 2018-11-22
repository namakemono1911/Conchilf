using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultManager2 : MonoBehaviour
{
    [SerializeField]
    rankingManager ranking;                     //ランキング

    [SerializeField]
    textAnimationController animation;          //アニメーション

    private sceneManager.SCENE nextScene;       //次のシーン名

    private void Start()
    {
        int score;
        var score1 = PlayerPrefs.GetInt(scoreType.SUM_SCORE.ToString() + "1");
        var score2 = PlayerPrefs.GetInt(scoreType.SUM_SCORE.ToString() + "2");

        if (score1 > score2)
            score = score1;
        else
            score = score2;

        if (ranking.whetherRankin(score, 3))
            nextScene = sceneManager.SCENE.SCENE_INPUT_NAME;
        else
            nextScene = sceneManager.SCENE.SCENE_RANKING;
    }

    private void Update()
    {
        if (!animation.IsAnimation)
        {
            sceneManager.Instance.SceneChange(nextScene);
        }
    }
}
