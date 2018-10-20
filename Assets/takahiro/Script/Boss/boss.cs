using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour {

	[System.Serializable]
	public struct BossInfo
	{
		public enemyTypeManager.EnemyTypeInfo standardInfo;	// 情報
		// 他にボスに必要な情報
	}


	// 状態
	private bossState bossState;
	// アニメーション管理
	private bossAnimation bossAnimation;
	// プレイヤー情報
	private playerController[] playerControllers;
	// 攻撃手段
	// ボス情報
	[SerializeField]
	private BossInfo bossInfo;

	// ゲッター
	public bossState state
	{
		get { return bossState; }
	}
	public BossInfo info
	{
		get { return bossInfo; }
	}
	public bossAnimation myAnimation
	{
		get { return bossAnimation; }
	}
	public playerController[] players
	{
		get { return playerControllers; }
	}


	private void Awake()
	{
		playerControllers = GameObject.Find("UICanvasHight").transform.GetComponentsInChildren<playerController>();
	}

	// Use this for initialization
	void Start () {
		bossAnimation = this.GetComponent<bossAnimation>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 状態遷移
	public void changeState(bossState newState)
	{
		if (bossState != null)
			Destroy(bossState);


		bossState = newState;
		bossState.initState();
	}

	// 生死判定
	public bool isDeth()
	{
		if(bossInfo.standardInfo.hp <= 0)
		{
			return true;
		}


		return false;
	}
}
