using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnimation : MonoBehaviour {

	public enum BOSS_ANIMATION_TYPE
	{
		ANIMATION_WAIT = 0,
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
				{(int)BOSS_ANIMATION_TYPE.ANIMATION_WAIT , "wait"},

			};

	}

	// Use this for initialization
	void Start()
	{
	}

	// 指定アニメーションの再生
	public void playAnimation(BOSS_ANIMATION_TYPE type)
	{
		Debug.Log("bossモーション変更 : " + type);

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
}
