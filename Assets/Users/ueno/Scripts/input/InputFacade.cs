using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

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

    abstract public bool whetherShot();
    abstract public bool whetherGuard();
    abstract public bool whetherReload();
}
