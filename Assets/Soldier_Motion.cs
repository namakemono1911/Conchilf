//-----------------------------------------------------
//
//  Title   :   エネミー　モーション処理
//  Auther  :   Shun Sakai
//  Date    :   2018/09/14   
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
    class option
    {
        public Animator     Soldier_Anim;   // アニメータ
        public Text         text;           // テキスト
        public Transform    My_transform;   // 自身のトランスフォーム情報
    }

    [SerializeField]
    private option OptionInfo;              // シリアライズ系統の情報

    // メンバ
    private AnimatorStateInfo   AnimInfo;           // アニメータの状態
    private Transform           Target_Transform;   // ターゲットトランスフォーム
    
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
    }
	
	// 更新処理
	void Update () {

        // ショットモーションの終了検知
        End_ShotMotion();

        // アイドル状態の時
        if( AnimInfo.IsName("Idle"))
        {
            // ターゲットに向かって回転したり
            // SlerpToTarget();

            
            // スペース押下時
            if (Input.GetKeyDown("space"))
            {
                // ショットモーションに移行
                OptionInfo.Soldier_Anim.SetBool("is_shot", true);
            }   
        }
    }

    // 発砲モーション終了検知
    private void End_ShotMotion()
    {
        AnimInfo = OptionInfo.Soldier_Anim.GetCurrentAnimatorStateInfo(0);

        // 現在の再生モーションが"shot"である && モーション進行が9割以上である
        if(AnimInfo.IsName("shot") && AnimInfo.normalizedTime >= 0.9f)
        {
            OptionInfo.Soldier_Anim.SetBool("is_shot", false);
        }
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