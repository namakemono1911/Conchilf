using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingBGM : MonoBehaviour
{
    private AudioSource bgm;

	// Use this for initialization
	void Start ()
    {
        bgm = ResultBGM.bgm;
	}
	
	// Update is called once per frame
	void Update ()
    {
		// エラー処理追加 石井
		if (bgm == null)
			return;

        if (!sceneManager.Instance.isFadeIn() && sceneManager.Instance.isFade())
            bgm.volume -= 0.01f;

        if (bgm.volume <= 0)
        {
            ResultBGMManager.destoryBGM();
            Destroy(this.gameObject);
        }
    }
}
