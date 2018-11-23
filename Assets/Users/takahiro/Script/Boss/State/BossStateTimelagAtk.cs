///////////////////////////////////////////////
//
//  Title   : ボス    時間差攻撃
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
public class BossStateTimelagAtk : bossState
{
    public BossStateTimelagAtk(boss b) : base(b) { }

    bool shotEnable;

    public override void initState()
    {
        // 初期化
        shotEnable = true;

        // 発砲
        shot00();

        // 両プレイヤーに時間差発砲
        boss.myAnimation.playAnimation(bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_ROTATE);
    }
    public override void updateState()
    {
        // モーションの途中で発砲
        if(boss.myAnimation.GetNormalizedTime() >= 0.5f && shotEnable == true)
        {
            shotEnable = false;
        
            // ショット１呼び出し
            shot01();
        }

        
        // モーションが終了したらインターバルに遷移
        if (boss.myAnimation.isPlayingAnimation())
        {
            // ループカウントアップ
            boss.RoopCountUp();

            boss.ChangeState(new BossStateWaitToDubleAtk(boss), bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_ROTATE);
        }

    }

    // ヒット時の処理
    public override void hitBullet(int damege, bool critical)
    {
        hitBoss(damege, critical, bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_ROTATE);
    }

    // 発砲0
    private void shot00()
    {
        // 変数
        Vector3 Pos = new Vector3(0, 0, 0);

        // 弾数を減算
        boss.bullet.shotBullet();

        // プレイヤー1&2の座標取得
        Pos = boss.GetPlayerPos(0);

        // 弾をセット
        boss.bulletInstance_Right.SetBullet(Pos, 4.0f);
        boss.bulletInstance_Left.SetBullet(Pos, 4.0f);

    }

    // 発砲1
    private void shot01()
    {
        // 変数
        Vector3 Pos = new Vector3(0, 0, 0);

        // プレイヤー1&2の座標取得
        if (boss.GetPlayerNum() >= 2)
        {
            Pos = boss.GetPlayerPos(1);
        }
        else
        {
            // プレイヤー1&2の座標取得
            Pos = boss.GetPlayerPos(0);
        }

        // 弾をセット
        boss.bulletInstance_Right.SetBullet(Pos, 4.0f);
        boss.bulletInstance_Left.SetBullet(Pos, 4.0f);

    }


}
