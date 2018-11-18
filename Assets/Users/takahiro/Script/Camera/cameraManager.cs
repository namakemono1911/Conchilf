using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour {

	[System.Serializable]
	public struct CameraCallEnemy
	{
		public int cameraCallEnemy;
		public float timeLimitSecond;
	}

	[SerializeField]
	private cameraMove cameraMove;
	[SerializeField]
	private EnemySceneManager enemySceneManager;
	[SerializeField]
	private int debugScene;
	[SerializeField]
	private CameraCallEnemy[] cameraCallEnemy;

	private int numCamera;
	private int numScene;
	private float timerScene;   // シーンごとのタイマー
	private bool isEnemyWave;	// このシーンに敵がいるか

	// Use this for initialization
	void Start()
	{
		numCamera = 0;
		numScene = 0;
		timerScene = 0;
		isEnemyWave = false;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		// タイマー処理 → 強制カメラ移動
		checkTimer();

		// カメラの終了を検知 → 敵のシーンを再生
		cameraMoveEnd();

		// ウェーブ処理
		nextWave();
	}

	// カメラ変更
	private void nextCamera()
	{
		timerScene = 0.0f;
		isEnemyWave = false;
		++numCamera;

		// カメラの移動
		cameraMove.nextMove();
	}

	// シーン変更
	private void nextScene()
	{
		++numScene;
		isEnemyWave = true;
		if (enemySceneManager.EnemySceneNext() == false)
		{
			// 全滅したらシーン遷移 ← ここまだ条件かわる可能性あり
			//sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_GAME_BOSS);
		}
	}

	// カメラの終了を検知
	private void cameraMoveEnd()
	{
		if (cameraMove.isEndMove() && isEnemyWave == false)
		{
			if(cameraCallEnemy.Length > numScene && cameraCallEnemy[numScene].cameraCallEnemy == numCamera)
			{
				// 敵再生
				nextScene();
			}
			else
			{
				// 敵がいないシーンの場合
				nextCamera();
			}
		}
	}

	// ウェーブの終了を検知
	private void nextWave()
	{
		// 今のウェーブの敵がいない事を検知
		if(enemySceneManager.EnemyAllDead() && isEnemyWave == true)
		{
			// 全滅
			// 次のウェーブを呼ぶ
			if(enemySceneManager.EnemyWaveNext() == false)
			{
				// 次のウェーブが無い時
				// カメラを動かす
				nextCamera();
			}
		}
	}

	// タイマー処理
	private void checkTimer()
	{
		if (isEnemyWave)
		{
			timerScene += Time.deltaTime;

			if (cameraCallEnemy.Length > numScene && timerScene >= cameraCallEnemy[numScene].timeLimitSecond)
			{
				// 強制移動
				nextCamera();
			}
		}
		else
		{
			timerScene = 0.0f;
		}
	}
}
