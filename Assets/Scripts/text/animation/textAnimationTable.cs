using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class textAnimationTable : MonoBehaviour
{
    protected bool isActive = true;

    public bool IsActiveAnimation
    {
        get { return isActive; }
    }

    abstract public void initAnimation();
    abstract public void textAnimation(Text[] text);
}
