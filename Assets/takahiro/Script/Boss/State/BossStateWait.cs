///////////////////////////////////////////////
//
//  Title   : ボス    ウェイト処理
//  Auther  : Shun Sakai 
//  Date    : 2018/10/25
//  Update  : 
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateWait : bossState {

    public BossStateWait(boss b) : base(b) { }

    // ステート初期化
    public override void initState()
    {
        // ボス用タイマースタート
        boss.timerStart();
    
        // モーション再生
        boss.myAnimation.playAnimation(bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0);
    }

    // 更新
    public override void updateState()
    {
    }

    // ヒット時の処理
    public override void hitBullet(int damege, bool critical)
    {
        hitBoss(damege, critical, bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0);
    }
}
