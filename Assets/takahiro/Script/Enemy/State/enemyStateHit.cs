using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateHit : enemyState
{
	private bool bCritical;

	public enemyStateHit(enemy p, bool critical) : base(p) { bCritical = critical; }

	public override void initState()
	{
		//	撃たれるモーション開始(クリティカルで分岐)
	}
	public override void updateState()
	{
		// モーションが終わったら次の状態へ
	}
	public override void hitBullet(int damege, bool critical)
	{
		// 撃たれてるモーションの時も被弾するか
	}
}

