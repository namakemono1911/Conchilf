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

		Vector3 pos = new Vector3(0,0,0);
		enemy.bullet.shotBullet();  // 弾減らす
		int player = 0;
		// 当たる確率
		if(hitProbability <= enemy.enemyTypeInfo.hitProbability)
		{
			// 1p or 2p
			// ランダム
			// 1pしかいない場合のも書いて
			//int hitPlayer = (int)Random.Range(0.1f, 1.9f);
			int hitPlayer = (int)Random.Range(0, 0.9f);
			playerController[] p = enemy.players;
			if(hitPlayer == 0)
			{
				// 1p
				pos = p[0].getHitPos();
				player = 1;
			}
			else
			{
				// 2p
				pos = p[1].getHitPos();
				player = 2;
			}
		}
		else
		{
			// プレイヤーに当たらないけど撃つとき
			Vector3 camera = Camera.main.transform.position;
			Vector3 enemyPos = enemy.transform.position;
			pos = new Vector3(camera.x, camera.y + 10.0f, camera.z);
			player = 0;

		}

		// 発射!!!!!!!!!!!!!
		enemy.bulletInstance.SetBullet(pos , 1.0f , player);
	}

}
