using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateDown : enemyState
{

	private bool bCritical;

	public enemyStateDown(enemy p , bool critical) : base(p) { bCritical = critical; }

	public override void initState()
	{
		// 右手表示を消す
		Destroy(enemy.hand);

		// 後ろの敵に当たらないので、当たり判定を消す
		enemy.gameObject.GetComponent<CapsuleCollider>().enabled = false;


		//	倒れるモーション開始(クリティカルで分岐)
		if(bCritical)
		{
			enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_DETH_CRITICAL);
            enemy.Player.result.addScore(scoreType.DEFEAT_NUM);
		}
		else
		{
			enemy.myAnimation.playAnimation(enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_DETH_NORMAL);
            enemy.Player.result.addScore(scoreType.ARREST_NUM);
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
