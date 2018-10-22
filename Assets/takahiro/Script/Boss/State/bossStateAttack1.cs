using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 両プレイヤーに撃つ
public class bossStateAttack1 : bossState {

	public bossStateAttack1(boss b) : base(b) { }

	public override void initState()
	{
		// 撃つ
		shot();

		boss.myAnimation.playAnimation(bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_FORWARD);
	}
	public override void updateState()
	{
	}

	public override void hitBullet(int damege, bool critical)
	{
	}

	private void shot()
	{

	}

}
