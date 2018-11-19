using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : UiAnimationInterface
{
    [SerializeField]
    private RectTransform effectSprite; //エフェクト
    
    [SerializeField]
    private RectTransform startPos;     //開始位置

    [SerializeField]
    private RectTransform endPos;       //終了位置

    [SerializeField]
    private float animationSpeed;       //アニメーション速度

    private bool whetherGuard = false;  //カードしてるかどうか

    public override void startAnimation()
    {
        whetherGuard = true;
    }

    public override void endAnimation()
    {
        whetherGuard = false;
    }

    public void Start()
    {
        effectSprite.position = startPos.position;
    }

    public  void Update()
    {
        if (whetherGuard)
            effectSprite.position = Vector3.Lerp(effectSprite.position, endPos.position, animationSpeed);
        else
            effectSprite.position = Vector3.Lerp(effectSprite.position, startPos.position, animationSpeed);
    }
}
