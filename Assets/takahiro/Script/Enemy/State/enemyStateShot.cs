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
	}

	public override void updateState()
	{
		// 待機時間

		// モーションが終わり次第、待ちに遷移
	}

	public override void hitBullet(int damege, bool critical)
	{
		hitEnemy(damege , critical);
	}

	private void shot()
	{
		int hitProbability = (int)Random.Range(1, 100);

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
