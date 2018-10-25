using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyBulletMarkUI : MonoBehaviour {

	[SerializeField]
	private Image enemyBulletMarkImage;	// 弾痕イメージ
	[SerializeField]
	private Transform[] effectPos;  // 弾痕エフェクトの位置
	[SerializeField]
	private int fadeTime;           // 弾痕が消える時間

	private int timeCounter;        // フェードカウンター
	private int effectPosNum;       // 弾痕位置のインデックス
	private Transform effectTransform; // 最終的なエフェクトのTransform
	private float fadeSpeed;        // 毎フレーム透明になる数
	private Color fadeColor;		// イメージの色

	private void Start()
	{
		effectPosNum = 0;
		timeCounter = 0;
		fadeSpeed = 1.0f / (float)fadeTime;
		fadeColor = enemyBulletMarkImage.color;
		SetBulletMark(0);
	}

	private void Update()
	{
		// 弾痕のフェード処理
		if (timeCounter >= fadeTime)
		{
			Destroy(this.gameObject);
		}

		fadeColor.a -= fadeSpeed;
		enemyBulletMarkImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeColor.a);
		++timeCounter;
	}

	// あらかじめ座標を指定している場合
	public void SetBulletMark(int posIdx)
	{
		effectPosNum = posIdx;
		effectTransform = effectPos[effectPosNum];
	}

	// 動的に場所を指定する場合
	public void SetBulletMarkTransform(Transform tr)
	{
		effectTransform = tr;
	}
}
