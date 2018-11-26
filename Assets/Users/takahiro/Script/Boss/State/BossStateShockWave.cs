///////////////////////////////////////////////
//
//  Title   : ボス ショックウェーブ
//  Auther  : Shun Sakai 
//  Date    : 2018/10/25
//  Update  : 2018/11/26
//  Memo    : 効果音処理追加
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ショックウェーブ
public class BossStateShockWave : bossState
{

    public BossStateShockWave(boss b) : base(b) { }

    // ステート初期化
    public override void initState()
    {
        // 発砲
        shot();

        // モーション再生
        boss.myAnimation.playAnimation(bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOCKWAVE);
    }

    // 更新
    public override void updateState()
    {
        // モーションが終了したらインターバルに遷移
        if (boss.myAnimation.isPlayingAnimation())
        {
            boss.ChangeState(new BossStateWaitToDubleAtk(boss), bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOCKWAVE);
        }
    }

    // ヒット時
    public override void hitBullet(int damege, bool critical)
    {
        hitBoss(damege, critical, bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOCKWAVE);
    }

    // 発砲
    private void shot()
    {
       // ショックェーブ演出

    }
}
