using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateReload : enemyState
{
	public enemyStateReload(enemy p) : base(p) { }

	public override void initState()
	{
		enemy.timerStart();
		enemy.bullet.reloadBullet();
	}
	public override void updateState()
	{
		// 目標地点に到着(目標地点との距離が近い or 時間経過、今は時間で取ってる)
		if (enemy.timer >= enemy.enemyTypeInfo.reloadFrame)
		{
			enemy.timerReset();
			// 撃つへ移行 ← 他の状態に修正できる
			enemy.changeState(new enemyStateWaitToShot(enemy , false) , enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RELOAD);
		}
	}

	public override void hitBullet(int damege, bool critical)
	{
		hitEnemy(damege, critical , enemyAnimationManager.ENEMY_ANIMATION_TYPE.ANIMATION_RELOAD);
	}

}
