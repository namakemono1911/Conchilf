using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class enemyStateMove : enemyState
{

	public enemyStateMove(enemy p) : base(p) { }

	public override void initState()
	{
		enemy.timerStart();

		// 移動開始
		enemy.transform.DOMove(enemy.enemyCSVInfo.ENEMY_MOVE_POS, enemy.enemyCSVInfo.MOVE_SECOND);

		// 移動モーション開始
		enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RUN);

	}
	public override void updateState()
	{
		// 目標地点に到着(目標地点との距離が近い or 時間経過、今は時間で取ってる)
		if (enemy.timer >= enemy.enemyCSVInfo.MOVE_SECOND)
		{
			// 到着
			// 待機へ移行 ← 他の状態に修正できる
			enemy.timerReset();
			enemy.changeState(new enemyStateShotInterval(enemy) , enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RUN);
		}
	}

	public override void hitBullet(int damege, bool critical)
	{
		hitEnemy(damege , critical , enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RUN);
	}

}
