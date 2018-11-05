using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dangerUI : MonoBehaviour
{
	[SerializeField]
	private Transform testTarnsform;			// テスト用の3d座標
	[SerializeField]
	private Image dangetUI;                     // ui
	[SerializeField]
	private int visibleTimeStart;               // 一番遠い位置での点滅間隔(フレーム)
	[SerializeField]
	private int visibleTimeEnd;                 // 一番近い位置点滅間隔(フレーム)

	private Transform tr;						// 表示位置
	private float length;						// 弾との距離(リアルタイム)
	private float lengthStand;					// 弾との距離(撃った初期位置)
	private bool visible;						// 表示、非表示
	private bool isCount;						// 表示切替か否か
	private Vector3 playerPos;					// プレイヤーの位置
	private int visibleTimeDifference;			// 点滅間隔の差分
	private int timeCount;						// タイマー
	private int visibleTime;                    // 切り替わる時間
												//	private (弾) bullet;					// 弾から位置貰う


	private void Start()
	{
		playerPos = new Vector3(0, 0, 0);
		lengthStand = 10;
		visibleTimeDifference = visibleTimeStart - visibleTimeEnd;
		isCount = false;
		visible = true;
	}

	private void Update()
	{
		if (isCount)
		{
			++timeCount;
			if (timeCount >= visibleTime)
			{
				timeCount = 0;
				isCount = false;
			}
		}
		else
		{
			isCount = true;

			Color cr = dangetUI.color;

			if(visible)
			{
				cr.a = 1.0f;
			}
			else
			{
				cr.a = 0.0f;
			}

			dangetUI.color = new Color(cr.r, cr.g, cr.b, cr.a);
			visible = !visible;

			// 今の位置
			Vector3 bulletPos = testTarnsform.position;
			//bulletPos = bullet.GetPos();    // 弾の位置取得
													// 距離の計算
			length = Vector3.Distance(bulletPos, playerPos);
			// 今の距離の初めの距離に対する割合
			float perLength = length / lengthStand;
			// 差分の大きさ
			int dif = (int)((float)visibleTimeDifference * perLength);
			// 最終的な点滅間隔
			visibleTime = visibleTimeEnd + dif;

		}
	}

	// 初期長さのセット
	public void setLength(float l)
	{
		lengthStand = l;
	}

	// 位置セット
	public void setPos(Transform t)
	{
		tr = t;
	}

	// プレイヤーの位置セット
	public void setPlayerPos(Vector3 pos)
	{
		playerPos = pos;
	}

	// 敵の弾セット
	//public void setEnemyBullet((敵の弾型) bt)
	//{
	//	bullet = bt;
	//}
}
