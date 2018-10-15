using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateDown : enemyState
{

	private bool bCritical;

	public enemyStateDown(enemy p , bool critical) : base(p) { bCritical = critical; }

	public override void initState()
	{
		//	倒れるモーション開始(クリティカルで分岐)
		if(bCritical)
		{
			enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_DETH_CRITICAL);
		}
		else
		{
			enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_DETH_NORMAL);
		}
	}
	public override void updateState()
	{
		// モーションが終わったらデストロイ
		if(enemy.myAnimation.isPlayingAnimation())
		{
			Destroy(this.gameObject);
		}
	}

	public override void hitBullet(int damege, bool critical)
	{
	}
}
