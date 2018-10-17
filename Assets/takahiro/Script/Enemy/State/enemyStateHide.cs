using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class enemyStateHide : enemyState
{
	public enemyStateHide(enemy p) : base(p) {}

	public override void initState()
	{
		//	隠れる
		Vector3 posHide = this.transform.position;
		posHide.y -= 1.5f;
		enemy.transform.DOMove(posHide, 2);

		enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RELOAD);
	}
	public override void updateState()
	{
		// モーションが終わったら射撃待機状態へ
		if (enemy.myAnimation.isPlayingAnimation())
		{
			enemy.changeState(new enemyStateWaitToShot(enemy, true), enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RELOAD);
		}
	}
	public override void hitBullet(int damege, bool critical)
	{
		// 撃たれてるモーションの時も被弾するか
		hitEnemy(damege, critical , enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RELOAD);
	}
}