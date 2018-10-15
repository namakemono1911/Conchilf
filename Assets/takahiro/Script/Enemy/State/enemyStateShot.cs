using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateShot : enemyState
{
	public enemyStateShot(enemy p) : base(p) { }

	public override void initState()
	{
		// 弾を撃つ
		shot();

		// ショットモーション開始
		enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT);
	}

	public override void updateState()
	{
		// モーションが終わり次第、待ちに遷移
		if(enemy.myAnimation.isPlayingAnimation())
		{
			enemy.changeState(new enemyStateShotInterval(enemy), enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT);
		}
	}

	public override void hitBullet(int damege, bool critical)
	{
		hitEnemy(damege , critical, enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_SHOT);
	}

	private void shot()
	{
		int hitProbability = (int)Random.Range(1, 100);

		enemy.bullet.shotBullet();

		// 当たる確率
		if(hitProbability <= enemy.enemyTypeInfo.hitProbability)
		{
			// 1p or 2p
			// ランダム
			int hitPlayer = (int)Random.Range(0, 1);

			if(hitPlayer == 0)
			{
				// 1p
			}
			else
			{
				// 2p
			}
		}
	}

}
