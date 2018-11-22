using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBulletMarkUI : MonoBehaviour {

	[SerializeField]
	private Image playerBulletMarkImage; // 弾痕イメージ

	[SerializeField]
	private int fadeTime;           // 弾痕が消える時間
	[SerializeField]
	private float scale;            // 拡縮誤差誤差

	private Transform[] effectPos;  // 弾痕エフェクトの位置
	private int timeCounter;        // フェードカウンター
	private int effectPosNum;       // 弾痕位置のインデックス
	private Transform effectTransform; // 最終的なエフェクトのTransform
	private float fadeSpeed;        // 毎フレーム透明になる数
	private Color fadeColor;        // イメージの色

	private void Start()
	{
		timeCounter = 0;
		fadeSpeed = 1.0f / (float)fadeTime;
		fadeColor = playerBulletMarkImage.color;

		// 回転
		int lotate = Random.Range(0, 360);
		this.transform.Rotate(new Vector3(0, 0, (float)lotate));

		// 拡縮
		float locallocate = Random.Range(-scale, scale);
		Vector3 thisScale = this.transform.localScale;
		this.transform.localScale = new Vector3(thisScale.x + locallocate, thisScale.y + locallocate, thisScale.z + locallocate);
	}

	private void Update()
	{
		// 弾痕のフェード処理
		if (timeCounter >= fadeTime)
		{
			Destroy(this.gameObject);
		}

		fadeColor.a -= fadeSpeed;
		playerBulletMarkImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeColor.a);
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
