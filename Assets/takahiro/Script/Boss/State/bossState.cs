using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class bossState : MonoBehaviour
{

	protected boss boss;

	//コンストラクタ
	public bossState(boss b) { boss = b; }

	//初期化
	abstract public void initState();

	//更新
	abstract public void updateState();

	//ヒット処理
	abstract public void hitBullet(int damege, bool critical);

	// 被弾
	protected void hitBoss(int damege, bool critical)
	{
		// クリティカル判定
		if (critical)
		{
		}

		// ダメージ

		// 生死判定
		if(boss.isDeth())
		{
			// 死んだ
		}
		else
		{
			// 真でない
		}
	}
}
