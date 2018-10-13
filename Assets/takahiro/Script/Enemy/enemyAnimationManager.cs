using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimationManager : MonoBehaviour {

	public enum ENEMY_ANIMATION_TYPE
	{
		ANIMATION_WAIT = 0,
		ANIMATION_RUN,
		ANIMATION_SHOT,
		ANIMATION_SHOT_INTERVAL,
		ANIMATION_RELOAD_TO_SHOT,
		ANIMATION_SHO_TO_RELOAD,
		ANIMATION_RELOAD,
		ANIMATION_DAMEGE_NORMAL,
		ANIMATION_DAMEGE_CRITICAL,
		ANIMATION_DETH_NORMAL,
		ANIMATION_DETH_CRITICAL,
		ANIMATION_MAX
	}


	private Dictionary<int, string> animationNames;

	private Animator enemyAnimator;

	// Use this for initialization
	void Awake() {

		// アニメーションのdictionary設定
		animationNames = new Dictionary<int, string>()
			{
				{(int)ENEMY_ANIMATION_TYPE.ANIMATION_DAMEGE_CRITICAL , "damegeCritical"},
				{(int)ENEMY_ANIMATION_TYPE.ANIMATION_DAMEGE_NORMAL , "damegeNormal"},
				{(int)ENEMY_ANIMATION_TYPE.ANIMATION_DETH_CRITICAL , "dethCritical"},
				{(int)ENEMY_ANIMATION_TYPE.ANIMATION_DETH_NORMAL , "dethNormal"},
				{(int)ENEMY_ANIMATION_TYPE.ANIMATION_RELOAD , "reload"},
				{(int)ENEMY_ANIMATION_TYPE.ANIMATION_RELOAD_TO_SHOT , "reloadToShot"},
				{(int)ENEMY_ANIMATION_TYPE.ANIMATION_RUN , "run"},
				{(int)ENEMY_ANIMATION_TYPE.ANIMATION_SHOT , "shot"},
				{(int)ENEMY_ANIMATION_TYPE.ANIMATION_SHOT_INTERVAL , "shotInterval"},
				{(int)ENEMY_ANIMATION_TYPE.ANIMATION_SHO_TO_RELOAD , "shotToReload"},
				{(int)ENEMY_ANIMATION_TYPE.ANIMATION_WAIT , "wait"}, 

			};
	}
	
	// 情報取得
	public Dictionary<int, string> getEnemyAnimationDictonary()
	{
		return animationNames;
	}
}
