using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateDown : enemyState
{

	private bool bCritical;

	public enemyStateDown(enemy p , bool critical) : base(p) { bCritical = critical; }

	public override void initState()
	{
		// 後ろの敵に当たらないので、当たり判定を消す
		enemy.gameObject.GetComponent<CapsuleCollider>().enabled = false;


		//	倒れるモーション開始(クリティカルで分岐)
		if(bCritical)
		{
			enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_DETH_CRITICAL);
            enemy.Player.score.Score.addCount(ScoreCount.DEFEAT_CNT);
		}
		else
		{
			enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_DETH_NORMAL);
            enemy.Player.score.Score.addCount(ScoreCount.ARREST_CNT);
        }
	}
	public override void updateState()
	{
		// モーションが終わったらデストロイするとウェーブマネージャーがおかしくなるので、非アクティブ
		if(enemy.myAnimation.isPlayingAnimation())
		{
			enemy.gameObject.SetActive(false);
		}
	}

	public override void hitBullet(int damege, bool critical)
	{
	}
}
