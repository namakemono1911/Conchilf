///////////////////////////////////////////////
//
//  Title   : ボス    天井打ち
//  Auther  : Shun Sakai 
//  Date    : 2018/10/25
//  Update  : 
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 待機後天井打ち
public class BossStateWaitToShotUp : bossState
{

    public BossStateWaitToShotUp(boss b) : base(b) { }

    // ステート初期化
    public override void initState()
    {
        // タイマーリセット
        boss.timerReset();

        // ボス用タイマースタート
        boss.timerStart();

        // モーション再生
        boss.myAnimation.playAnimation(bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0);
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
            // 待機後に天井打ち
            boss.ChangeState(new BossStateShotuUp(boss), bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0);
        }
    }

    // ヒット時
    public override void hitBullet(int damege, bool critical)
    {
        hitBoss(damege, critical, bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0);
    }
}
