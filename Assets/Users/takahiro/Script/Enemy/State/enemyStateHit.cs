using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateHit : enemyState
{
	private bool bCritical;

	public enemyStateHit(enemy p, bool critical) : base(p) { bCritical = critical; }

	public override void initState()
	{
		enemy.timerStop();

		// 隠れている時に当たった時のために座標調整
		enemy.transform.position = enemy.enemyCSVInfo.ENEMY_MOVE_POS;

		//	撃たれるモーション開始(クリティカルで分岐)
		if(bCritical)
		{
			enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_DAMEGE_CRITICAL);
		}
		else
		{
			enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_DAMEGE_NORMAL);
		}
	}
	public override void updateState()
	{
		// モーションが終わったら直前状態へ
		if(enemy.myAnimation.isPlayingAnimation())
		{
			enemy.changeStateBefor();
		}
	}
	public override void hitBullet(int damege, bool critical)
	{
		// 撃たれてるモーションの時も被弾するか
		hitEnemy(damege, critical , enemy.befor);
	}
}

