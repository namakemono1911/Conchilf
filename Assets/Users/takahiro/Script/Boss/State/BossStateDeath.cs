///////////////////////////////////////////////
//
//  Title   : ボス    死亡
//  Auther  : Shun Sakai 
//  Date    : 2018/10/25
//  Update  : 
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 死亡
public class BossStateDeath : bossState
{
    public BossStateDeath(boss b) : base(b) { }


    // ステート初期化
    public override void initState()
    {
        // モーション再生
        boss.myAnimation.playAnimation(bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_DETH);
        
        // めぐみん
        boss.EfectMegumin2();

        // ハブプレイヤー取得
        havePlayerNum HavePlayer = boss.Get_HavePlayer();
        foreach (var p in HavePlayer.player)
            p.result.addScore(scoreType.ARREST_NUM);

    }

    // 更新
    public override void updateState()
    {
        // モーションが終了したらインターバルに遷移
        if (boss.myAnimation.isPlayingAnimation() == true)
        {
            // モクモク
            boss.EffectSmokeMegumin();

            // 消す
            Destroy(boss.gameObject);

        }
    }

    // ヒット時
    public override void hitBullet(int damege, bool critical)
    {
       // hitBoss(damege, critical, bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_ROTATE);
    }
}
