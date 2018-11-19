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

	[System.Serializable]
	public struct CameraWait
	{
		public int cameraWait;
		public float timeLimitSecond;
	}


	[SerializeField]
	private GameObject EnemyManager;

	[SerializeField]
	private cameraMove cameraMove;
	[SerializeField]
	private EnemySceneManager enemySceneManager;
	[SerializeField]
	private float lastWaitTime;
	[SerializeField]
	private CameraCallEnemy[] cameraCallEnemy;
	[SerializeField]
	private CameraWait[] cameraWait;

	private int numScene;
	private float timerScene;   // シーンごとのタイマー
	private bool isEnemyWave;   // このシーンに敵がいるか
	private bool isSceneChange;
	private bool isSceneChangebuf;
	private bool isDebug;
	private bool isWait;
	private int waitNum;
	private bool isLast;
	public bool debugCameraStopMode = false;

	// Use this for initialization
	void Start()
	{
		isSceneChangebuf = false;
		isLast = false;
		waitNum = 0;
		isWait = false;
		isDebug = false;
		numScene = 0;

		for (int cntEnemyCall = 0; cameraCallEnemy.Length - 1 >= cntEnemyCall; ++cntEnemyCall)
		{
			if(cameraCallEnemy[cntEnemyCall].cameraCallEnemy < cameraMove.getCameraMoveNum())
			{
				++numScene;
			}
		}

		for (int cntEnemyCall = 0; cameraWait.Length - 1 >= cntEnemyCall; ++cntEnemyCall)
		{
			if (cameraWait[cntEnemyCall].cameraWait < cameraMove.getCameraMoveNum())
			{
				++waitNum;
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

		if(isSceneChange)
		{
			endTimeUpdate();

			timerScene += Time.deltaTime;
			if(timerScene >= lastWaitTime)
			{
				sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_GAME_BOSS_1);
			}
			return;
		}

		if(isWait)
		{
			timerScene += Time.deltaTime;
			if(cameraWait[waitNum].timeLimitSecond <= timerScene)
			{
				++waitNum;
				isWait = false;
				nextCamera();
			}

			return;
		}

		// タイマー処理 → 強制カメラ移動
		checkTimer();

		// カメラの終了を検知 → 敵のシーンを再生
		if (!debugCameraStopMode) {
			cameraMoveEnd();
		}

		// ウェーブ処理
		nextWave();
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

        // 敵の全消し
        EnemyManager.GetComponent<EnemyManager>().EnemyAllDelete();
	}

	// シーン変更
	private void nextScene()
	{
		++numScene;
		isEnemyWave = true;

		if(cameraMove.isMaxCamera())
		{
			isLast = true;
		}

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
			else if(cameraMove.getCameraMoveNum() == cameraWait[waitNum].cameraWait)
			{
				isWait = true;
				timerScene = 0.0f;
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
				// 次のシーンが無い時
				if(EnemyManager.GetComponent<EnemyManager>().GetNextSceneEnable(numScene) == false)
				{
					chackSceneManager();
					return;
				}
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
		timerScene = 0.0f;
		isSceneChange = true;
	}

    // ウェーブの全滅確認
	private bool nowWaveEnemyAllDead()
	{
        int cntEnemy = EnemyManager.transform.childCount;
        for (int i = 0; i < cntEnemy; ++i)
        {
            if (EnemyManager.transform.GetChild(i).GetComponent<enemy>().nowHp() > 0)
            {
                return false;
            }
        }
        return true;
	}


	// カメラが最後まで行った後の残り時間に行うもの
	private void endTimeUpdate()
	{
	}
}
