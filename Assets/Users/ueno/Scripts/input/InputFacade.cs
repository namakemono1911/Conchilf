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

    abstract public bool whetherShot();
    abstract public bool whetherGuard();
    abstract public bool whetherReload();
}
