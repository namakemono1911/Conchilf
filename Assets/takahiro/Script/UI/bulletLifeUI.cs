using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletLifeUI : MonoBehaviour {
	[SerializeField]
	private int lifeMax;            // 最大値 0で無限
	[SerializeField]
	private int lifeFirst;          // 初期値
	[SerializeField]
	private GameObject lifeImage;       // ライフイメージ
	[SerializeField]
	private float gapPos;           // ずれ
	[SerializeField]
	private bool is1Player;         // 1pか2pか
	[SerializeField]
	private int wrapNumber;			// 表示が折り返す数

	private int lifeNow;        // 現在のライフ

	// Use this for initialization
	void Start()
	{
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
		if (isNeedReload())
		{
			return;
		}

		// 1つ目を配置
		GameObject.Instantiate(lifeImage, this.transform.position, transform.parent.transform.rotation, this.transform);

		// 2つ目以降
		GameObject childObj;
		Vector3 childPos = this.transform.position;
		for (int i = 1; i < getBulletLife(); ++i)
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

			// 折り返し判定
			if(i % wrapNumber == 0)
			{
				childPos.y -= gapPos;
				childPos.x = this.transform.position.x;
			}

			childObj = GameObject.Instantiate(lifeImage, childPos, Quaternion.identity, this.transform);
		}
	}

	// 消費or回復
	public void addBulletLife(int n)
	{
		lifeNow += n;

		if (lifeMax > 0)
		{
			if (lifeNow >= lifeMax)
			{
				lifeNow = lifeMax;
			}
		}

		if (lifeNow <= 0)
		{
			lifeNow = 0;
		}

		SetUI(lifeNow);

	}

	// 弾薬確認
	public bool isNeedReload()
	{
		if (lifeNow <= 0)
		{
			return true;
		}

		return false;
	}

	// 残弾確認
	public int getBulletLife()
	{
		return lifeNow;
	}

	// リロード
	public void bulletReload()
	{
		lifeNow = lifeMax;
		SetUI(lifeNow);
	}

	// 最大値アップ
	public void addBulletLifeMax(int n)
	{
		lifeMax += n;

		if(lifeMax <= 0)
		{
			lifeMax = 0;
		}
	}

	// 最大値セット
	public void setBulletLifeMax(int n)
	{
		lifeMax = n;

		if (lifeMax <= 0)
		{
			lifeMax = 0;
		}
	}

    // 初期弾数セット
    public void setBulletLifeFirst(int n)
    {
        lifeMax = n;
        lifeNow = n;

        if (lifeMax > 0)
        {
            if (lifeNow >= lifeMax)
            {
                lifeNow = lifeMax;
            }
        }

        if (lifeNow <= 0)
        {
            lifeNow = 0;
        }

        SetUI(lifeNow);

    }
}
