///////////////////////////////////////////////
//
//  Title   : 基礎クラス継承型シングルトンクラス
//  Auther  : Shun Sakai 
//  Date    : 2018/08/06
//  Memo    : MonoBehaviourを継承したシングルトンクラス
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviourWithInit where T : MonoBehaviourWithInit
{

    //インスタンス
    private static T _instance;

    //インスタンスを外部から参照する用(getter)
    public static T Instance
    {
        get
        {
            //インスタンスがまだ作られていない
            if (_instance == null)
            {

                //シーン内からインスタンスを取得
                _instance = (T)FindObjectOfType(typeof(T));

                //シーン内に存在しない場合はエラー
                if (_instance == null)
                {
                    Debug.LogError(typeof(T) + " is nothing");
                }
                //発見した場合は初期化
                else
                {
                    _instance.InitIfNeeded();
                }
            }
            return _instance;
        }
    }

    //=================================================================================
    //初期化
    //=================================================================================

    protected sealed override void Awake()
    {
        //存在しているインスタンスが自分であれば問題なし
        if (this == Instance)
        {
            return;
        }

        //自分じゃない場合は重複して存在しているので、エラー
        Debug.LogError(typeof(T) + " is duplicated");
    }

}