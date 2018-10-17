using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateShotToWait : enemyState
{

	public enemyStateShotToWait(enemy p) : base(p) {}

	public override void initState()
	{
		enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT_TO_RELOAD);
	}
	public override void updateState()
	{
		// モーションが終わったら
		if (enemy.myAnimation.isPlayingAnimation())
		{
			// 障害物のあるなしで分岐
			if(enemy.isFailure())
			{
				// 障害物あり
				enemy.changeState(new enemyStateHide(enemy), enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT_TO_RELOAD);
			}
			else
			{
				// 障害物なし
				enemy.changeState(new enemyStateReload(enemy), enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT_TO_RELOAD);
			}

		}
	}

	public override void hitBullet(int damege, bool critical)
	{
		hitEnemy(damege, critical, enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT_TO_RELOAD);
	}

}
