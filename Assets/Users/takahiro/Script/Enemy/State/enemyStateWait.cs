using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateWait : enemyState
{

	public enemyStateWait(enemy p) : base(p) { }

	public override void initState()
	{
		enemy.timerStart();

		// 待機モーション開始
		enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_WAIT);
	}

	public override void updateState()
	{
		// 待機時間
		if(enemy.timer >= enemy.enemyTypeInfo.shotInterval)
		{
			// 撃つ準備に遷移
			enemy.timerReset();
			enemy.changeState(new enemyStateWaitToShot(enemy, false), enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_WAIT);
		}
	}

	public override void hitBullet(int damege, bool critical)
	{
		hitEnemy(damege , critical, enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_WAIT);
	}
}
