using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class enemyStateWaitToShot : enemyState
{
	private bool bBuried;

	public enemyStateWaitToShot(enemy p, bool buried) : base(p) { bBuried = buried; }

	public override void initState()
	{
		//	構えるモーション開始(隠れてるかで分岐)
		if (bBuried)
		{
			// 地上へ戻る
			enemy.transform.DOMove(enemy.enemyCSVInfo.ENEMY_MOVE_POS, 2);

		}

		enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RELOAD_TO_SHOT);
	}
	public override void updateState()
	{
		// モーションが終わったら射撃待機状態へ
		if (enemy.myAnimation.isPlayingAnimation())
		{
			enemy.changeState(new enemyStateShotInterval(enemy) , enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RELOAD_TO_SHOT);
		}
	}
	public override void hitBullet(int damege, bool critical)
	{
		// 撃たれてるモーションの時も被弾するか
		hitEnemy(damege, critical, enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RELOAD_TO_SHOT);
	}
}