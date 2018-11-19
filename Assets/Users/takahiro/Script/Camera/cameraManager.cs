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
	private GameObject EnemyManager;

	[SerializeField]
	private cameraMove cameraMove;
	[SerializeField]
	private EnemySceneManager enemySceneManager;
	[SerializeField]
	private int debugScene;
	[SerializeField]
	private CameraCallEnemy[] cameraCallEnemy;

	private int numScene;
	private float timerScene;   // シーンごとのタイマー
	private bool isEnemyWave;   // このシーンに敵がいるか
	private bool isSceneChange;
	private bool isDebug;
	// Use this for initialization
	void Start()
	{

		isDebug = false;
		numScene = 0;

		for (int cntEnemyCall = 0; cameraCallEnemy.Length - 1 >= cntEnemyCall; ++cntEnemyCall)
		{
			if(cameraCallEnemy[cntEnemyCall].cameraCallEnemy <= cameraMove.getCameraMoveNum())
			{
				++numScene;
			}
		}
		isSceneChange = false;
		timerScene = 0;
		isEnemyWave = false;

		if(cameraMove.getCameraMoveNum() != 0)
		{
			isDebug = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		// タイマー処理 → 強制カメラ移動
		checkTimer();

		// カメラの終了を検知 → 敵のシーンを再生
		cameraMoveEnd();

		// ウェーブ処理
		nextWave();

		// このゲームシーンの終了を検知
		chackSceneManager();
	}

	// カメラ変更
	private void nextCamera()
	{
		timerScene = 0.0f;
		isEnemyWave = false;

		if(cameraMove.isMaxCamera())
		{
			return;
		}

		// カメラの移動
		cameraMove.nextMove();
	}

	// シーン変更
	private void nextScene()
	{
		++numScene;
		isEnemyWave = true;

		if(isDebug == true)
		{
			enemySceneManager.EnemySceneNextToIndex(numScene - 1);
			return;
		}

		if(numScene == 1)
		{
			// 初めの敵
			enemySceneManager.StartEnemyScene();
			return;
		}

		if (enemySceneManager.EnemySceneNext() == false)
		{
			int ii = 00;
			// 全滅したらシーン遷移 ← ここまだ条件かわる可能性あり
			//sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_GAME_BOSS_1);
		}
	}

	// カメラの終了を検知
	private void cameraMoveEnd()
	{
		if (cameraMove.isEndMove() && isEnemyWave == false)
		{
			if(cameraCallEnemy.Length > numScene && cameraCallEnemy[numScene].cameraCallEnemy == cameraMove.getCameraMoveNum())
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
		if(nowWaveEnemyAllDead() == true && isEnemyWave == true)
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

	// このゲームシーンの終了を検知
	private void chackSceneManager()
	{
		// 最後のカメラが停止したら
		if(cameraMove.isMaxCamera() && cameraMove.isEndMove() && isSceneChange == false)
		{
			isSceneChange = true;
			sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_GAME_BOSS_1);
		}
	}

	private bool nowWaveEnemyAllDead()
	{
		int enemyNum = EnemyManager.transform.childCount;
		for(int cntenemy = 0; cntenemy <= enemyNum - 1; ++cntenemy)
		{
			if(EnemyManager.transform.GetChild(cntenemy).gameObject.activeSelf == true)
			{
				return false;
			}
		}
		return true;
	}
}
