﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WiimoteApi;

//SE
[System.Serializable]
public class PlayerSE
{
	public AudioSource shotSE;          //射撃時
	public AudioSource reloadSE;        //リロード時
	public AudioSource guardSE;         //ガード時
	public AudioSource guardHitSE;      //ガード中ヒット
	public AudioSource downSE;          //ダウン
    public AudioSource noAmmoSE;        //弾無
    public AudioSource revivalSE;       //復活
}

abstract public class InputFacade : MonoBehaviour
{
    [SerializeField]
    private int playerNum;                  //プレイヤー番号

    [SerializeField]
    protected RectTransform reticle;        //レティクル

    public RectTransform Reticle
    {
        get { return reticle; }
    }

    public int PlayerNum
    {
        get { return playerNum; }
    }

    abstract public bool whetherShot();         //射撃したかどうか
    abstract public bool whetherGuard();        //ガードしたかどうか
    abstract public bool whetherReload();       //リロードしたかどうか
    abstract public bool whetherWakeUp();       //起き上がったかどうか

    //レティクル非表示
    public void reticleVisible(bool isVisible)
    {
        reticle.gameObject.SetActive(isVisible);
    }
}
