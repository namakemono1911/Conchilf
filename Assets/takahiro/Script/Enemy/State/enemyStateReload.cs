using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateReload : enemyState
{

	private int moveTimer;

	public enemyStateReload(enemy p) : base(p) { }

	public override void initState()
	{
		moveTimer = 0;
	}
	public override void updateState()
	{
		// 目標地点に到着(目標地点との距離が近い or 時間経過、今は時間で取ってる)
		++moveTimer;
		if (moveTimer >= enemy.enemyTypeInfo.reloadFrame)
		{
			// 撃つへ移行 ← 他の状態に修正できる
			enemy.changeState(new enemyStateShot(enemy));
		}
	}

	public override void hitBullet(int damege, bool critical)
	{
		hitEnemy(damege, critical);
	}

}
