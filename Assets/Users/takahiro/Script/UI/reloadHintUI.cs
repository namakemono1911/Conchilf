﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reloadHintUI : MonoBehaviour {

	[SerializeField]
	private Image reloadHintImage;  // 画像
	[SerializeField]
	private int visibleTime;    // 点滅間隔

	private int visibleCounter;	// タイマー
	private bool visible;       // 表示フラグ
	private Color imageColor;	// 色

	// Use this for initialization
	void Start () {
		visibleCounter = visibleTime;
		visible = false;
		imageColor = reloadHintImage.color;
	}
	
	// Update is called once per frame
	void Update () {
		if(visibleCounter >= visibleTime)
		{
			visibleCounter = 0;
			visible = !visible;

			if(visible)
			{
				imageColor.a = 1.0f;
			}
			else
			{
				imageColor.a = 0.0f;
			}

			reloadHintImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, imageColor.a);
		}
		else
		{
			++visibleCounter;
		}



	}
}
