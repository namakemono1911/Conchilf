//-----------------------------------------------------
//
//  Title   :   エネミー　モーション処理
//  Auther  :   Shun Sakai
//  Date    :   2018/09/26   
//  Update  :   
//  Memo    : Animatorの再生や管理は Runtime Animator Controller を経由して管理されています
//
//-----------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

// 兵士　モーション制御
public class Soldier_Motion : MonoBehaviour {

    // シリアライズ
    [System.Serializable]
    class Option
    {
        public Enemy_gun    GunScript;      // 銃のスクリプト
        public Animator     Soldier_Anim;   // アニメータ
        public GameObject   Target;         // ターゲット
        public GameObject   Player;        // プレイヤー1
        public GameObject   Player2;        // プレイヤー2
        public Transform    My_transform;   // 自身のトランスフォーム情報
        public float        Shot_Time;      // 着弾までの時間
    }

    [SerializeField]
    private Option OptionInfo;              // シリアライズ系統の情報

    // メンバ
    private AnimatorStateInfo   AnimInfo;           // アニメータの状態
    private Transform           Target_Transform;   // ターゲットトランスフォーム
    private bool                Shot_Enable;
    
    // アニメータ識別子
    private static readonly int hashIdle    = Animator.StringToHash("Idle");
    private static readonly int hashAttack  = Animator.StringToHash("shot");
    
    // 初回処理
    private void Awake()
    {
        // インスペクタ上でアタッチされていない場合は取得
        if (OptionInfo.Soldier_Anim == null)
        {
            OptionInfo.Soldier_Anim = GetComponent<Animator>();
        }

        if (OptionInfo.My_transform == null)
        {
            OptionInfo.My_transform = GetComponent<Transform>();
        }
    }

    // 初期処理
    void Start ()
    {
        // アニメータ内のベースレイヤー情報取得
        AnimInfo = OptionInfo.Soldier_Anim.GetCurrentAnimatorStateInfo(0);

        // 初期化
        Shot_Enable = false;
    }
	
	// 更新処理
	void Update () {

        // ターゲットのトランスフォーム取得
        Target_Transform = OptionInfo.Target.transform;

        // ショットモーションの終了検知
        End_ShotMotion();

        // 目標座標に向けて回転処理
        SlerpToTarget();

        // ショット状態ではない
        if (Shot_Enable == false)
        {
            // アイドル状態の時
            if (AnimInfo.IsName("Idle"))
            {
                // ショット状態に移行
                Shot_Enable = true;
            }
        }
        // ショット状態
        else if(Shot_Enable == true)
        {

            // ショットモーションの終了検知
            if (End_ShotMotion())
            {
                Shot_Enable = false;
            }
        }
        

    }

    // ショットスタート
    private void ShotStart(int targetindex)
    {
        // ショット可能状態か
        if (Shot_Enable == true)
        {
            // ショットモーションに移行
            OptionInfo.Soldier_Anim.SetBool("is_shot", true);

            if (targetindex == 0)
            {
                // なんかいい感じの時に打ちたい
                OptionInfo.GunScript.SetBullet(OptionInfo.Player.transform.position, OptionInfo.Shot_Time);
            }
            else if (targetindex == 1)
            {
                // なんかいい感じの時に打ちたい
                OptionInfo.GunScript.SetBullet(OptionInfo.Player2.transform.position, OptionInfo.Shot_Time);
            }
        }
    }


    // 発砲モーション終了検知
    private bool End_ShotMotion()
    {
        AnimInfo = OptionInfo.Soldier_Anim.GetCurrentAnimatorStateInfo(0);

        // 現在の再生モーションが"shot"である && モーション進行が9割以上である
        if(AnimInfo.IsName("shot") && AnimInfo.normalizedTime >= 0.9f)
        {
            OptionInfo.Soldier_Anim.SetBool("is_shot", false);
            return true;
        }

        return false;
    }
    
    // 目標への回転角度を算出
    private void SlerpToTarget()
    {
        // ターゲット情報からクォータニオンを算出
        Vector3 Target = Target_Transform.position;
        
        // Y軸を統一
        if (transform.position.y != Target.y)
        {
            Target = new Vector3(Target_Transform.position.x, transform.position.y, Target_Transform.position.z);
        }

        // クォータニオンを求める
        Quaternion targetRotation = Quaternion.LookRotation(Target - transform.position);

        // 回転角度増加
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

    }
}