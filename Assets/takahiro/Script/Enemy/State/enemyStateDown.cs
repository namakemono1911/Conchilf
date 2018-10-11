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
	}
	public override void updateState()
	{
		// モーションが終わったらデストロイ
	}

	public override void hitBullet(int damege, bool critical)
	{
	}
}
