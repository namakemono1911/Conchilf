﻿///////////////////////////////////////////////
//
//  Title   : ボス    待機後に死亡
//  Auther  : Shun Sakai 
//  Date    : 2018/10/25
//  Update  : 
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateWaitoToDeath : bossState
{
    public BossStateWaitoToDeath(boss b) : base(b) { }

    // ステート初期化
    public override void initState()
    {
        // タイマーリセット
        boss.timerReset();

        // ボス用タイマースタート
        boss.timerStart();

        // モーション再生
        boss.myAnimation.playAnimation(bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0);

        // エクスプロージョン！
        boss.EfectMegumin();
        
    }

    // 更新
    public override void updateState()
    {
        // インターバル経過後
        if (boss.timer >= boss.BossStatus.shotInterval)
        {
            // タイマーストップ
            boss.timerStop();
            // タイマーリセット
            boss.timerReset();
            // 待機後に死亡モーション
            boss.ChangeState(new BossStateDeath(boss), bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0);
        }
    }

    // ヒット時
    public override void hitBullet(int damege, bool critical)
    {
        // hitBoss(damege, critical, bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0);
    }

}
