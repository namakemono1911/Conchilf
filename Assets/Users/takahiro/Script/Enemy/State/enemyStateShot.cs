using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateShot : enemyState
{
	public enemyStateShot(enemy p , bool hit) : base(p) { bhit = hit; }

	private bool bhit;

	public override void initState()
	{
		// se
		enemy.playSE(enemy.enumSE.SHOT);

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
		Vector3 pos = new Vector3(0,0,0);
		enemy.bullet.shotBullet();	// 弾減らす

		// 当たる確率
		if(bhit == true)
		{
            Debug.Log("プレイヤー : " + enemy.numPlayer);
			Debug.Log("bb");

			// 1p or 2p
			if (enemy.numPlayer == 1)
            {
                playerController[] p = enemy.players;
                // 1p
                pos = p[0].getHitPos();
            }
            else
            {
                // 2pPlay
                // ランダム
                int hitPlayer = (int)Random.Range(0.1f, 1.9f);
                Debug.Log("敵 : " + hitPlayer);
                playerController[] p = enemy.players;
                if (hitPlayer == 0)
                {
                    // 1p
                    pos = p[0].getHitPos();
                }
                else
                {
                    // 2p
                    pos = p[1].getHitPos();
                }
            }
        }
		else
		{
			// プレイヤーに当たらないけど撃つとき
			Vector3 camera = Camera.main.transform.position;
			Vector3 enemyPos = enemy.transform.position;
			pos = new Vector3(camera.x, camera.y + 10.0f, camera.z);

		}

		// 発射!!!!!!!!!!!!!
		enemy.bulletInstance.SetBullet(pos , 1.0f);
	}

}
