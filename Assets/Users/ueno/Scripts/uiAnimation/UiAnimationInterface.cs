using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UiAnimationInterface : MonoBehaviour
{
    protected bool isAnimation = true;       //アニメーションしてるかどうか

    public bool IsAnimation
    {
        get { return isAnimation; }
    }

    public abstract void startAnimation();
    public abstract void endAnimation();
}
