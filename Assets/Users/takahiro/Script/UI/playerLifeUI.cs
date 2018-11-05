using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerLifeUI : MonoBehaviour {

	[SerializeField]
	private int lifeMax;			// 最大値 0で無限
	[SerializeField]
	private int lifeFirst;			// 初期値
	[SerializeField]
	private GameObject lifeImage;		// ライフイメージ
	[SerializeField]
	private float gapPos;			// ずれ
	[SerializeField]
	private bool is1Player;			// 1pか2pか
	[SerializeField]
	private float gapSize;			// 1つめと2つめ以降のサイズ比率

	private int lifeNow;        // 現在のライフ
	private Vector2 lifeSizeDelta;	// 2ライフ目以降のサイズ

	// Use this for initialization
	void Start () {
		lifeNow = lifeFirst;
		lifeSizeDelta = lifeImage.GetComponent<RectTransform>().sizeDelta;
		lifeSizeDelta *= gapSize;
		SetUI(lifeNow);
	}

	private void SetUI(int life)
	{
		// 全除去
		if (this.transform.childCount > 0)
		{
			int child = this.transform.childCount;
			for (int i = 0; i < child; ++i)
			{
				Destroy(this.transform.GetChild(i).gameObject);
			}
		}

		// ライフ確認
		if (isDeth())
		{
			return;
		}

		// 1つ目を配置
		GameObject.Instantiate(lifeImage, this.transform.position, Quaternion.identity, this.transform);

		// 2つ目以降
		GameObject childObj;
		Vector3 childPos = this.transform.position;
		for (int i = 1; i < getPlayerLife(); ++i)
		{
			if (is1Player)
			{
				childPos.x -= gapPos;
			}
			else
			{
				childPos.x += gapPos;
			}
			childPos.z -= 0.1f;

			childObj = GameObject.Instantiate(lifeImage, childPos, Quaternion.identity, this.transform);
			childObj.GetComponent<RectTransform>().sizeDelta = new Vector2(lifeSizeDelta.x, lifeSizeDelta.y);
		}
	}

	// ダメージor回復
	public void addPlayerLife(int n)
	{
		lifeNow += n;

		if(lifeMax > 0)
		{
			if(lifeNow >= lifeMax)
			{
				lifeNow = lifeMax;
			}
		}

		if(lifeNow <= 0)
		{
			lifeNow = 0;
		}

		SetUI(lifeNow);

	}

	// 死亡確認
	public bool isDeth()
	{
		if(lifeNow <= 0)
		{
			return true;
		}

		return false;
	}

	// 残基確認
	public int getPlayerLife()
	{
		return lifeNow;
	}
}
