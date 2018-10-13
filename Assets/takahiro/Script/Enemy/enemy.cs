using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class enemy : MonoBehaviour {


	[SerializeField]
	private EnemyInfo enemycsvInfo;                     // csv必要な情報
	[SerializeField]
	private enemyTypeManager enemyTypeManager;          // タイプ情報管理

	private int damege;                                 // 総合ダメージ
	private enemyTypeManager.EnemyTypeInfo typeInfo;    // タイプ情報
	private enemyState enemyState;                      // 状態
	private enemyAnimation enemyAnimation;              // アニメーション管理

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

	// Use this for initialization
	void Start () {

		damege = 0;

		// タイプ管理
		typeInit();
	}
	
	// Update is called once per frame
	void Update () {
		// タイプ管理
		typeUpdate();

		// 向き修正
		lookAt(Camera.main.transform.position);
	}

	private void typeInit()
	{
		// 情報の読み込み
		typeInfo = enemyTypeManager.getEnemyTypeInfo(enemycsvInfo.MODEL_NUMBER);
		changeState(new enemyStateMove(this));
		enemyState.initState();
	}

	private void typeUpdate()
	{
		// 状態遷移
		enemyState.updateState();
	}

	// 状態遷移
	public void changeState(enemyState newState)
	{
		if (enemyState != null)
			Destroy(enemyState);

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
}
