///////////////////////////////////////////////
//
//  Title   : ボスステート管理
//  Auther  : Shun Sakai
//  Date    : 2018/10/22
//  Update  :
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///////////////////////////////////////////////
// ボスステート管理
///////////////////////////////////////////////
abstract public class bossState : MonoBehaviour
{
    // 本体
	protected boss boss;

	//コンストラクタ
	public bossState(boss b) { boss = b; }

	//初期化
	abstract public void initState();

	//更新
	abstract public void updateState();

	//ヒット処理
	abstract public void hitBullet(int damege, bool critical);

	// 被弾
	protected void hitBoss(int damege, bool critical, bossAnimation.BOSS_ANIMATION_TYPE type)
	{
		// クリティカル判定
		if (critical)
		{
            damege *= 2;
		}

        // ダメージ
        boss.Add_Damage(damege);
        
		// 生死判定
		if(boss.isDeth())
		{
            // 死んだ
            boss.ChangeState(new BossStateWaitoToDeath(boss), bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0);
        }
		else
		{
			// 真でない
		}
	}
}
