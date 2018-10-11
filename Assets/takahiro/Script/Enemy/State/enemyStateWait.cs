using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateWait : enemyState
{
	private int waitTimer;	// 待機時間

	public enemyStateWait(enemy p) : base(p) { }

	public override void initState()
	{
		waitTimer = 0;
	}

	public override void updateState()
	{
		// 待機時間
		++waitTimer;
		if(waitTimer >= enemy.enemyTypeInfo.shotInterval)
		{
			// 撃つに遷移
			enemy.changeState(new enemyStateShot(enemy));
		}
	}

	public override void hitBullet(int damege, bool critical)
	{
		hitEnemy(damege , critical);
	}
}
