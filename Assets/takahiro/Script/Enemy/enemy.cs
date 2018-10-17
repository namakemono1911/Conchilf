﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class enemy : MonoBehaviour {


	[SerializeField]
	private EnemyInfo enemycsvInfo;                     // csv必要な情報
	[SerializeField]
	private enemyTypeManager enemyTypeManager;          // タイプ情報管理
	[SerializeField]
	private Transform[] playersPos;                     // プレイヤーの位置
	[SerializeField]
	private Enemy_gun gun;								// 敵の弾

	private int damege;                                 // 総合ダメージ
	private enemyTypeManager.EnemyTypeInfo typeInfo;    // タイプ情報
	private enemyState enemyState;                      // 状態
	private enemyAnimation enemyAnimation;              // アニメーション管理
	private float enemyTimer;                           // タイマー
	private enemyAnimationManager.ENEMY_ANIMATION_TYPE beforAnimation;	// 以前のアニメーション種類
	private bool isCount;                                                   // カウントしているか否か
	private enemyBullet enemyBullet;                                        // 敵の弾処理
	private playerController[] playerControllers;										// プレイヤーの情報

	// getter
	// タイプ情報
	public enemyTypeManager.EnemyTypeInfo enemyTypeInfo
	{
		get { return typeInfo; }
	}
	// 個体情報
	public EnemyInfo enemyCSVInfo
	{
		get { return enemycsvInfo; }
	}
	// アニメーション管理
	public enemyAnimation myAnimation
	{
		get { return enemyAnimation; }
	}
	// タイマー
	public float timer
	{
		get { return enemyTimer; }
	}
	// 以前のアニメーション
	public enemyAnimationManager.ENEMY_ANIMATION_TYPE befor
	{
		get { return beforAnimation; }
	}
	// 敵の弾
	public enemyBullet bullet
	{
		get { return enemyBullet; }
	}
	//ステートのゲッター
	public enemyState State
	{
		get { return enemyState; }
	}
	// プレイヤー
	public playerController[] players
	{
		get { return playerControllers; }
	}
	// 弾
	public Enemy_gun bulletInstance
	{
		get { return gun; }
	}
	private void Awake()
	{
		playerControllers = GameObject.Find("UICanvasHight").transform.GetComponentsInChildren<playerController>();
	}

	// Use this for initialization
	void Start () {

		damege = 0;
		isCount = false;

		// タイプ管理
		typeInit();
	}

	private void FixedUpdate()
	{
		// タイプ管理
		typeUpdate();

		// 向き修正
		lookAt(Camera.main.transform.position);

		// タイマー処理
		if(!isCount)
		{
			return;
		}

		enemyTimer += Time.deltaTime;
	}

	private void typeInit()
	{
		// 情報の読み込み
		typeInfo = enemyTypeManager.getEnemyTypeInfo(enemycsvInfo.MODEL_NUMBER);
		enemyAnimation = GetComponent<enemyAnimation>();
		enemyBullet = GetComponent<enemyBullet>();
		enemyBullet.setMaxBullet(enemyTypeInfo.bulletNum);
		enemyBullet.reloadBullet();

		changeState(new enemyStateMove(this) , enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RUN);
		enemyState.initState();
	}

	private void typeUpdate()
	{
		// 状態遷移
		enemyState.updateState();
	}

	// 状態遷移
	public void changeState(enemyState newState , enemyAnimationManager.ENEMY_ANIMATION_TYPE befor)
	{
		if (enemyState != null)
			Destroy(enemyState);

		beforAnimation = befor;

		enemyState = newState;
		enemyState.initState();
	}

	// 敵情報を取得
	public EnemyInfo getEnemyInfo()
	{
		return enemycsvInfo;
	}

	// 敵情報をセット(マネージャーから)
	public void setEnemyInfo(EnemyInfo ei)
	{
		enemycsvInfo = ei;

		// 情報による操作
	}

	// 生死判定
	public bool isDeth()
	{
		if(typeInfo.hp <= damege)
		{
			return true;
		}

		return false;
	}

	// 目標がいる向きを向く　今はカメラ
	public void lookAt(Vector3 vec)
	{
		Vector3 p = vec;
		p.y = transform.position.y;
		transform.LookAt(p);

	}

	// 被弾
	public void addDamege(int n)
	{
		damege += n;
	}

	// プレイヤーの弾と当たったか
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "playerBullet")
		{
			enemyState.hitBullet(-1 , false);
		}
	}

	// タイマーリセット
	public void timerReset()
	{
		enemyTimer = 0.0f;
	}

	// タイマースタート
	public void timerStart()
	{
		isCount = true;
	}

	// タイマーストップ
	public void timerStop()
	{
		isCount = false;
	}

	// 一つ前の状態に移行
	public void changeStateBefor()
	{
		// 条件分岐
		if (beforAnimation == enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RELOAD)
		{
			// リロード中はリロードへ
			changeState(new enemyStateReload(this), beforAnimation);
		}
		else if (beforAnimation == enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RUN)
		{
			// 移動中は移動へ
			changeState(new enemyStateMove(this), beforAnimation);
		}
		else if (beforAnimation == enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT || beforAnimation == enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT_INTERVAL)
		{
			// 射撃or射撃間隔中は移動へ
			changeState(new enemyStateShotInterval(this), beforAnimation);
		}
		else if (beforAnimation == enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT_TO_RELOAD)
		{
			// 待機移行中は待機へ
			if(isFailure())
			{
				changeState(new enemyStateHide(this), beforAnimation);
			}
			else
			{
				changeState(new enemyStateWait(this), beforAnimation);
			}
		}
		else if (beforAnimation == enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RELOAD_TO_SHOT)
		{
			// 射撃移行中は射撃体勢へ
			changeState(new enemyStateShotInterval(this), beforAnimation);
		}
	}

	// 前に障害物があるかどうか
	public bool isFailure()
	{
		return false;
	}

}

