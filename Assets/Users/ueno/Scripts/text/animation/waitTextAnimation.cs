using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class waitTextAnimation : textAnimationTable
{
    [SerializeField]
    public float waitTime;

    private float startTime;

    public override void initAnimation()
    {
        startTime = Time.time;
        isActive = true;
    }

    public override void textAnimation(Text[] text)
    {
        if (Time.time - startTime >= waitTime)
            isActive = false;
    }
}
