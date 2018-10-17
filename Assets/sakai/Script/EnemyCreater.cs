///////////////////////////////////////////////
//
//  Title   : エネミークリエイター
//  Auther  : Shun Sakai 
//  Date    : 2018/10/17
//  Update  : クリエイター関連の統合オブジェクト
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// エネミークリエイター
public class EnemyCreater : MonoBehaviour {

    // マネージャスクリプｔｐ
    private enemyTypeManager        E_TypeManager;  // エネミータイプマネージャー
    private enemyAnimationManager   E_AnimManager;  // エネミーアニメーションマネージャー

   [RuntimeInitializeOnLoadMethod()]
    void Init()
    {
        // マネージャー
        E_TypeManager = this.gameObject.GetComponent<enemyTypeManager>();
        E_AnimManager = this.gameObject.GetComponent<enemyAnimationManager>();
    }

    // 初回処理
    void Awake()
    {
    }
    
    // スタート
    void Start ()
    {
		
	}


    // エネミータイプマネージャ　ゲッタ
    public enemyTypeManager GetEnemyTypeManager()
    {
        return E_TypeManager;
    }

    // アニメーションマネージャ　ゲッタ
    public enemyAnimationManager GetEnemyAnimationManager()
    {
        return E_AnimManager;
    }
}
