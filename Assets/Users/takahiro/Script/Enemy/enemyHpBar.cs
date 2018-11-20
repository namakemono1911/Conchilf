using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHpBar : MonoBehaviour
{

	[SerializeField]
	private Transform textureScale;    // 初めスケール

	private int enemyHpStart;   // 初めhp
	private int enemyHpNow;     // 現hp
	private float enemyHpRaito; // 今のhpと初めのhpの比率
	private enemy myEnemy;
	private bool endInfoSet;
	private bool isActive;
	// Use this for initialization
	void Start()
	{
		endInfoSet = false;

		/*現hpの取得*/
		myEnemy = this.transform.parent.parent.parent.parent.GetComponent<enemy>();
		enemyHpStart = myEnemy.enemyTypeInfo.hp;
		if (enemyHpStart >= 1)
		{
			endInfoSet = true;
			enemyHpNow = enemyHpStart;
			Debug.Log("hp" + enemyHpNow);
		}
	}
	// Update is called once per frame
	void Update()
	{
		if(!isActive)
		{
			return;
		}

		// hp読み込みが終わってなかったら
		if (endInfoSet == false)
		{
			myEnemy = this.transform.parent.parent.parent.GetComponent<enemy>();
			enemyHpStart = myEnemy.enemyTypeInfo.hp;
			if (enemyHpStart >= 1)
			{
				endInfoSet = true;
				enemyHpNow = enemyHpStart;
				Debug.Log("hp" + enemyHpNow);
			}
		}
		else
		{
			enemyHpNow = myEnemy.nowHp();

			// 割合計算
			enemyHpRaito = enemyHpNow / (float)enemyHpStart;
			if(enemyHpRaito <= 0.0f)
			{
				enemyHpRaito = 0.0f;
			}
			Vector3 enemyHpScale = textureScale.localScale;
			enemyHpScale.x = enemyHpRaito;
			textureScale.localScale = new Vector3(enemyHpScale.x, enemyHpScale.y, enemyHpScale.z);
		}
	}

	private void OnEnable()
	{
		isActive = true;
		endInfoSet = false;

		/*現hpの取得*/
		myEnemy = this.transform.parent.parent.parent.parent.GetComponent<enemy>();
		enemyHpStart = myEnemy.enemyTypeInfo.hp;
		if (enemyHpStart >= 1)
		{
			endInfoSet = true;
			enemyHpNow = enemyHpStart;
			Debug.Log("hp" + enemyHpNow);
		}
		else
		{
			endInfoSet = false;
		}
	}
}