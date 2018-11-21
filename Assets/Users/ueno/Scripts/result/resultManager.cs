using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultManager : MonoBehaviour
{
    [SerializeField]
    rankingManager ranking;             //ランキング

    [SerializeField]
    textAnimationController animation;  //アニメーション

    private sceneManager.SCENE nextScene;           //次のシーン名

    private void Start()
    {
        var score = PlayerPrefs.GetInt(scoreType.SUM_SCORE.ToString() + "1");

        if (ranking.whetherRankin(score))
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
