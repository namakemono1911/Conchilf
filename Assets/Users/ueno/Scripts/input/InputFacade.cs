using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

[System.Serializable]
public class InputSound
{
    public AudioSource shotSE;
    public AudioSource reloadSE;
    public AudioSource guardSE;
}

abstract public class InputFacade : MonoBehaviour
{
    [SerializeField]
    private int playerNum;                  //プレイヤー番号

    [SerializeField]
    protected RectTransform reticle;        //レティクル

    [SerializeField]
    protected InputSound sound;

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
}
