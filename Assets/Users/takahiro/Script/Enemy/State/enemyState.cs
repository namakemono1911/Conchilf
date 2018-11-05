using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class enemyState : MonoBehaviour {

	protected enemy enemy;

	//コンストラクタ
	public enemyState(enemy e) { enemy = e; }

	//初期化
	abstract public void initState();

	//更新
	abstract public void updateState();

	//ヒット処理
	abstract public void hitBullet(int damege , bool critical);

	// 被弾
	protected void hitEnemy(int damege , bool critical , enemyAnimationManager.ENEMY_ANIMATION_TYPE type)
	{
		// クリティカル判定
		if(critical)
		{
			damege *= (int)enemy.enemyTypeInfo.criticalMagnification;
		}

		// ダメージ
		enemy.addDamege(damege);

		// 生死判定
		if(enemy.isDeth())
		{
			// 死んでたら
			enemy.changeState(new enemyStateDown(enemy , critical) , type);
			
		}
		else
		{
			// 生きてたら
			enemy.changeState(new enemyStateHit(enemy, critical) , type);
		}
	}
}
