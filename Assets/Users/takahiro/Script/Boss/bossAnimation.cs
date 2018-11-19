using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnimation : MonoBehaviour {

	public enum BOSS_ANIMATION_TYPE
	{
		ANIMATION_WAIT_0 = 0,
		ANIMATION_WAIT_1,
		ANIMATION_WAIK,
		ANIMATION_SHOT_FORWARD,
		ANIMATION_SHOT_UP,
		ANIMATION_SHOT_ROTATE,
		ANIMATION_RELOAD,
		ANIMATION_DETH,
        ANIMATION_SHOCKWAVE,
        ANIMATION_MAX
	}

	private Dictionary<int, string> animationNames;
	private Animator myAnimator;

	private void Awake()
	{
		// アニメーションデータの取得
		myAnimator = this.GetComponent<Animator>();

		// アニメーションのdictionary設定
		animationNames = new Dictionary<int, string>()
			{
				{(int)BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0 , "wait1"},
				{(int)BOSS_ANIMATION_TYPE.ANIMATION_WAIT_1 , "wait2"},
				{(int)BOSS_ANIMATION_TYPE.ANIMATION_WAIK , "walk"},
				{(int)BOSS_ANIMATION_TYPE.ANIMATION_SHOT_FORWARD , "shotForward"},
				{(int)BOSS_ANIMATION_TYPE.ANIMATION_SHOT_UP , "shotUp"},
				{(int)BOSS_ANIMATION_TYPE.ANIMATION_SHOT_ROTATE , "shotRotate"},
				{(int)BOSS_ANIMATION_TYPE.ANIMATION_RELOAD , "reload"},
				{(int)BOSS_ANIMATION_TYPE.ANIMATION_DETH , "deth"},
                {(int)BOSS_ANIMATION_TYPE.ANIMATION_SHOCKWAVE , "jamp"},
            };
	}

	// Use this for initialization
	void Start()
	{
	}

	// 指定アニメーションの再生
	public void playAnimation(BOSS_ANIMATION_TYPE type)
	{
		myAnimator.Play(animationNames[(int)type]);
	}

	// アニメーションが再生中か
	public bool isPlayingAnimation()
	{
		if (myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
		{
			return true;
		}

		return false;
	}
    
    
    // ノーマライズタイム
    public float GetNormalizedTime()
    {
        return myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}