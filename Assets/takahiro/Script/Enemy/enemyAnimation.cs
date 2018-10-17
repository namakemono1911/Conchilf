using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimation : MonoBehaviour {

	private enemyAnimationManager animationManager;
	private Dictionary<int, string> animationNames;
	private Animator myAnimator;

	// Use this for initialization
	void Start () {
		// 辞書データの取得
		animationManager = GameObject.Find("enemyAnimationManager").GetComponent<enemyAnimationManager>();
		if(animationManager == null)
		{
			Debug.Log(this.gameObject.name + " : アニメーションの取得に失敗");
		}

		animationNames = animationManager.getEnemyAnimationDictonary();

		// アニメーションデータの取得
		myAnimator = this.GetComponent<Animator>();

	}

	// 指定アニメーションの再生
	public void playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE type)
	{
		Debug.Log("モーション変更 : " + type);

		myAnimator.Play(animationNames[(int)type]);
	}

	// アニメーションが再生中か
	public bool isPlayingAnimation()
	{
		if(myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
		{
			return true;
		}

		return false;
	}
}
