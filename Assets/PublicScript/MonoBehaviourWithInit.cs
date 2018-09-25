///////////////////////////////////////////////
//
//  Title   : 初期化を備えたMonoBehaviourクラス
//  Auther  : Shun Sakai 
//  Date    : 2018/08/16
//  Update  : リファクタリング
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 初期化付の基礎クラス
public class MonoBehaviourWithInit : MonoBehaviour
{
    //初期化したかどうかのフラグ(一度しか初期化が走らないようにするため)
    private bool _isInitialized = false;

   // 初期化確認
    public void InitIfNeeded()
    {
        if (_isInitialized)
        {
            return;
        }
        Init();
        _isInitialized = true;
    }

    // 初期化処理
    protected virtual void Init() { }

    // sealed overrideするためにvirtualで作成
    protected virtual void Awake() { }

}
