using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScaling : UiAnimationInterface
{
    [SerializeField]
    private RectTransform image;        //変更適応画像

    [SerializeField]
    private float scalMagnification;    //拡大倍率

    private Vector2 originalSize;       //元のサイズ

    public override void startAnimation()
    {
        image.sizeDelta *= scalMagnification;
    }
    public override void endAnimation()
    {
        image.sizeDelta = originalSize;
    }

    // Use this for initialization
    void Start ()
    {
        originalSize = image.sizeDelta;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
