using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class activeTextAnimation : textAnimationTable
{
    [SerializeField]
    bool setActive;

    public override void initAnimation()
    {
        isActive = true;
    }

    public override void textAnimation(Text[] text)
    {
        foreach (var t in text)
        {
            t.gameObject.SetActive(setActive);
        }
        isActive = false;
    }
}
