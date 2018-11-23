using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateShotInterval : enemyState
{
	public enemyStateShotInterval(enemy p) : base(p) { }

	private bool hit;

	public override void initState()
	{
		// 当たる確率
		int hitProbability = (int)Random.Range(1, 100);

		hit = false;

		// 当たる確率
		if (hitProbability <= enemy.enemyTypeInfo.hitProbability)
		{
			if (enemy.bullet.isBullet())
			{
				enemy.shotDanger.flashStart();
				hit = true;
				Debug.Log("aa");
			}
		}


		enemy.timerStart();
		enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT_INTERVAL);
	}
	public override void updateState()
	{
		// 射撃間隔
		if(enemy.timer >= enemy.enemyTypeInfo.shotInterval)
		{
			enemy.timerReset();
			enemy.shotDanger.flashEnd();

			// 残弾によって変化
			if (enemy.bullet.isBullet())
			{
				// 残弾あり
				enemy.changeState(new enemyStateShot(enemy , hit), enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT);
			}
			else
			{
				// 残弾なし
				enemy.changeState(new enemyStateShotToWait(enemy), enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT);
			}
		}
	}

	public override void hitBullet(int damege, bool critical)
	{
		hitEnemy(damege, critical, enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT_INTERVAL);
	}

}
