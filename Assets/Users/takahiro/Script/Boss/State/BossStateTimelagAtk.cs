///////////////////////////////////////////////
//
//  Title   : ボス 時間差攻撃
//  Auther  : Shun Sakai 
//  Date    : 2018/10/25
//  Update  : 2018/11/26
//  Memo    : 効果音処理を追加
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 時間差攻撃
public class BossStateTimelagAtk : bossState
{
    public BossStateTimelagAtk(boss b) : base(b) { }

    // 変数
    bool ShotEnable = false;
    bool HitEnable  = false;

    // 初期化処理
    public override void initState()
    {
        // 初期化
        ShotEnable = true;

        // 確率計算
        int HitProbability = (int)Random.Range(1, 100);
        if(HitProbability <= boss.BossStatus.hitProbability)
        {
            HitEnable = true;
        }

        // 発砲
        shot00();
        // モーション再生
        boss.myAnimation.playAnimation(bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_ROTATE);
    }

    // 更新
    public override void updateState()
    {
        // 時間差で更に発砲
        if(boss.myAnimation.GetNormalizedTime() >= 0.5f && ShotEnable == true)
        {
            // フラグ切り替え
            ShotEnable = false;
            // 発砲
            shot01();
        }
        
        // モーションが終了したらインターバルに遷移
        if (boss.myAnimation.isPlayingAnimation())
        {
            // ループカウントアップ
            boss.RoopCountUp();
            // ステート切り替え
            boss.ChangeState(new BossStateWaitToDubleAtk(boss), bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_ROTATE);
        }
    }

    // 被弾時
    public override void hitBullet(int damege, bool critical)
    {
        hitBoss(damege, critical, bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_SHOT_ROTATE);
    }

    // 発砲処理1
    private void shot00()
    {
        // 変数
        Vector3 Pos = Vector3.zero;

        if (HitEnable == true)
        {
            // プレイヤー1の座標取得
            Pos = boss.GetPlayerPos(0);
        }
        else
        {
            // 非ヒット時の演出用
            Vector3 camera = Camera.main.transform.position;
            Pos = new Vector3(camera.x + 50.0f, camera.y + 50.0f, camera.z);
        }

        // 弾をセット
        boss.bulletInstance_Right.SetBullet(Pos, boss.bulletspeed);
        boss.bulletInstance_Left.SetBullet (Pos, boss.bulletspeed);
        // 弾数を減算
        boss.bullet.shotBullet();
        // SE再生
        boss.SE.shotSE0.Play();
    }

    // 発砲処理2
    private void shot01()
    {
        // 変数
        Vector3 Pos = Vector3.zero;

        if (HitEnable == true)
        {

            // プレイヤー1の座標取得
            Pos = boss.GetPlayerPos(0);

            // プレイヤー人数が二人以上
            if (boss.GetPlayerNum() >= 2)
            {
                Pos = boss.GetPlayerPos(1);
            }

        }
        else
        {
            // 非ヒット時の演出用
            Vector3 camera = Camera.main.transform.position;
            Pos = new Vector3(camera.x - 50.0f, camera.y + 50.0f, camera.z);
        }

        // 弾をセット
        boss.bulletInstance_Right.SetBullet(Pos, boss.bulletspeed);
        boss.bulletInstance_Left.SetBullet (Pos, boss.bulletspeed);
        // 弾数を減算
        boss.bullet.shotBullet();
        // SE再生
        boss.SE.shotSE0.Play();

    }
}
