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

public class BossStateShotuUp : bossState
{
    public BossStateShotuUp(boss b) : base(b) { }

    // ステート初期化
    public override void initState()
    {
        // 発砲
        shot();

        // モーション再生
        boss.myAnimation.playAnimation(bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_UP);
    }

    // 更新
    public override void updateState()
    {
        // モーションが終了したらインターバルに遷移
        if (boss.myAnimation.isPlayingAnimation())
        {
            boss.ChangeState(new BossStateWaitToDubleAtk(boss), bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_UP);
        }
    }

    // ヒット時
    public override void hitBullet(int damege, bool critical)
    {
        hitBoss(damege, critical, bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_UP);
    }

    // 発砲
    private void shot()
    {
        // 変数
        Vector3 Pos0 = new Vector3(0, 0, 0);
        Vector3 Pos1 = new Vector3(0, 0, 0);

        // 弾数を減算
        boss.bullet.shotBullet();

        // 非ヒット時の演出用
        Vector3 camera = Camera.main.transform.position;
        Vector3 bossPos = boss.transform.position;
        Pos0 = new Vector3(camera.x, camera.y + 10.0f, camera.z);
        Pos1 = new Vector3(camera.x, camera.y + 10.0f, camera.z);
        
        // 発砲
        boss.bulletInstance_Right.SetBullet(Pos0, 5.0f);
        boss.bulletInstance_Left.SetBullet(Pos1, 5.0f);

    }
}
