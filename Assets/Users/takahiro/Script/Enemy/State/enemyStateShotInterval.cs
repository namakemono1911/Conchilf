using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateShotInterval : enemyState
{
	public enemyStateShotInterval(enemy p) : base(p) { }

	public override void initState()
	{
		if(enemy.bullet.isBullet())
		{
			//enemy.shotDanger.flashStart();
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
				enemy.changeState(new enemyStateShot(enemy), enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT);
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
