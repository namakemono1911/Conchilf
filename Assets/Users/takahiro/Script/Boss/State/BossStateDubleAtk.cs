///////////////////////////////////////////////
//
//  Title   : ボス 同時打ち
//  Auther  : Shun Sakai 
//  Date    : 2018/10/25
//  Update  : 2018/11/26
//  Memo    : 効果音処理を追加
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 同時打ち
public class BossStateDubleAtk : bossState
{
    public BossStateDubleAtk(boss b) : base(b) { }

    // 初期化処理
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
        if (boss.myAnimation.isPlayingAnimation())
        {
            boss.ChangeState(new BossStateWaitToTimelag(boss), bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_FORWARD);
        }
    }

    // 被弾時
    public override void hitBullet(int damege, bool critical)
    {
        hitBoss(damege, critical, bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_ROTATE);
    }

    // 発砲
    private void shot()
    {
        // 変数
        int HitProbability = (int)Random.Range(1, 100);
        Vector3 Pos0 = Vector3.zero;
        Vector3 Pos1 = Vector3.zero;

        // ヒット確率計算
        if (HitProbability <= boss.BossStatus.hitProbability)
        {
            Pos1 = boss.GetPlayerPos(0);

            // プレイヤー人数が二人以上
            if (boss.GetPlayerNum() >= 2)
            {
                Pos1 = boss.GetPlayerPos(1);
            }

            Pos0 = boss.GetPlayerPos(0);
        }
        else
        {
            // 非ヒット時の演出用
            Vector3 camera  = Camera.main.transform.position;
            Pos0 = new Vector3(camera.x + 50.0f, camera.y + 50.0f, camera.z);
            Pos1 = new Vector3(camera.x - 50.0f, camera.y + 50.0f, camera.z);
        }

        // 発砲処理
        boss.bulletInstance_Right.SetBullet(Pos0, boss.bulletspeed);
        boss.bulletInstance_Left.SetBullet (Pos1, boss.bulletspeed);
        // 弾数処理
        boss.bullet.shotBullet();
        // SE再生
        boss.SE.shotSE0.Play();
    }
}

