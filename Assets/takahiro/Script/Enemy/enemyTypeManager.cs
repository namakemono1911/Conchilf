using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTypeManager : MonoBehaviour {


	[System.Serializable]
	public struct EnemyTypeInfo
	{
		public int score;					// 倒したときのスコア
		public float criticalMagnification; // クリティカルのスコア倍率
		public int shotInterval;            // 射撃感覚
		public int bulletNum;               // 弾数
		public int hitProbability;			// プレイヤーに当たる確率
		public int reloadFrame;             // リロードの速さ
		public int hp;						// hp
	}

	public enum ENEMY_TYPE
	{
		TYPE_EASY = 0,
		TYPE_NORMAL,
		TYPE_HARD,
		TYPE_MAX
	}

	// タイプ
	[SerializeField]
	private EnemyTypeInfo[] enemyTypes;

	public EnemyTypeInfo getEnemyTypeInfo(ENEMY_TYPE type)
	{
		if(type >= ENEMY_TYPE.TYPE_MAX || (int)type <= -1)
		{
			Debug.Log("引数がおかしい");
		}

		return enemyTypes[(int)type];
	}
}
