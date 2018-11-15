﻿///////////////////////////////////////////////
//
//  Title   : ボス    同時打ち
//  Auther  : Shun Sakai 
//  Date    : 2018/10/25
//  Update  : 
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 両プレイヤーに撃つ
public class BossStateDubleAtk : bossState {

	public BossStateDubleAtk(boss b) : base(b) { }

    // ステート初期化
	public override void initState()
	{
		// 発砲
		shot();

        // モーション再生
		boss.myAnimation.playAnimation(bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_FORWARD);
	}

    // 更新
	public override void updateState()
	{
        // モーションが終了したらインターバルに遷移
        if(boss.myAnimation.isPlayingAnimation() )
        {
            boss.ChangeState(new BossStateWaitToTimelag(boss), bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_FORWARD);
        }
	}

    // ヒット時
	public override void hitBullet(int damege, bool critical)
	{
        hitBoss(damege, critical, bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_ROTATE);
    }

    // 発砲
	private void shot()
	{
        // 変数
        int     HitProbability  = (int)Random.Range(1, 100);
        Vector3 Pos0 = new Vector3(0, 0, 0);
        Vector3 Pos1 = new Vector3(0, 0, 0);


        // 弾数を減算
        boss.bullet.shotBullet();

        // ヒット確率計算
        if (HitProbability <= boss.BossStatus.hitProbability)
        {
            // プレイヤー取得
            playerController[] p = boss.players;

            // プレイヤー1&2の座標取得
            //  Pos0 = p[0].getHitPos();
            //  Pos1 = p[1].getHitPos();

            
        }
        else
        {
            // 非ヒット時の演出用
            Vector3 camera = Camera.main.transform.position;
            Vector3 bossPos = boss.transform.position;
            Pos0 = new Vector3(camera.x + 10.0f, camera.y + 10.0f, camera.z);
            Pos1 = new Vector3(camera.x - 10.0f, camera.y + 10.0f, camera.z);

        }

        // 発砲
        boss.bulletInstance_Right.SetBullet(Pos0, 5.0f);
        boss.bulletInstance_Left.SetBullet (Pos1, 5.0f);

    }

}