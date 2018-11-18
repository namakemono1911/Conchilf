///////////////////////////////////////////////
//
//  Title   : デバッグモード(ツカサ用)
//  Auther  : Shun Sakai 
//  Date    : 2018/11/15
//  Update  : 
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDebug : MonoBehaviour {

    // シリアライズ
    [System.Serializable]
    class Option
    {
        public CsvManager           Csv_Manager;        // CSVマネージャ
        public EnemySceneManager    EnemyScene_Manager; // エネミーシーンマネージャ
        public int                  PlaySceneIndex;     // 指定再生したいシーン番号
    }

    [SerializeField]
    private Option SerializeMember; // インスペクタ上のメンバ

    private int SceneNum;
    private int WaveNum;

	public bool clearEnemy = false;

	public GameObject policeCar = null;
	public GameObject Explotion = null;
	public GameObject Black = null;

    // Use this for initialization
    void Start () {

        // 初期化
        SceneNum    = 0;
        WaveNum     = 0;

		// CSV用のエネミー全削除
		if (clearEnemy) 
		{
			SerializeMember.Csv_Manager.Alldelete();
			Debug.Log("Csv用の配置データを全削除しました");
		}
	

	}
	
	// Update is called once per frame
	void Update () {

        // デバッグ
        if (Input.GetKey(KeyCode.C))
        {
            if (Input.GetKeyDown("1"))
            {
                // CSVアウトプット
                SerializeMember.Csv_Manager.CsvOutput();
                Debug.Log("Csvをセーブしました");

            }
            else if (Input.GetKeyDown("2"))
            {
                // CSVロード
                SerializeMember.Csv_Manager.CsvLoad();
                Debug.Log("Csvをロードしました");

            }
            else if (Input.GetKeyDown("3"))
            {
                // CSV用のエネミー全削除
                SerializeMember.Csv_Manager.Alldelete();
                Debug.Log("Csv用の配置データを全削除しました");
            }
            else if (Input.GetKeyDown("4"))
            {
                // シーン再生の初期化
                SerializeMember.EnemyScene_Manager.StartEnemyScene();
                SceneNum    = 0;
                WaveNum     = 0;
                Debug.Log("シーン再生をスタートしました -> " + SceneNum);
            }
            else if (Input.GetKeyDown("5"))
            {
                // ウェーブを進ませる
                SerializeMember.EnemyScene_Manager.EnemyWaveNext();
                WaveNum ++;
                Debug.Log("ウェーブを進めました -> " + WaveNum);
            }
            else if (Input.GetKeyDown("6"))
            {
                // シーンを進ませる
                SerializeMember.EnemyScene_Manager.EnemySceneNext();
                SceneNum++;
                WaveNum = 0;
                Debug.Log("シーンを進めました -> " + SceneNum);
            }
            else if (Input.GetKeyDown("7"))
            {
                // 指定のシーンを再生させる
                SceneNum = SerializeMember.PlaySceneIndex;
                SerializeMember.EnemyScene_Manager.EnemySceneNextToIndex(SceneNum);
                Debug.Log("指定したシーンの再生を始めました -> " + SceneNum);
            }
            else if (Input.GetKeyDown("8"))
            {
                // エネミーの全削除
                SerializeMember.EnemyScene_Manager.EnemyAllDelete();
                Debug.Log("シーン内のエネミーを全削除しました -> ");
            }
        }

		// イベントトリガー
		EventTrigger ();
    }

	void EventTrigger()
	{
		if (Input.GetKey (KeyCode.E))
		{
			if (Input.GetKeyDown ("1"))
			{
				Time.timeScale = 0.1f;
				Explotion.SetActive (false);
				Explotion.SetActive (true);
				Debug.Log("Event : 爆発");
			}
			if (Input.GetKeyDown ("2"))
			{
				Time.timeScale = 1.0f;
				Black.SetActive (false);
				Black.SetActive (true);
				Debug.Log("Event : フェードアウト");
			}
			if (Input.GetKey ("3"))
			{
				if (Input.GetKeyDown (KeyCode.Space))
				{
					Black.SetActive (false);
					policeCar.SetActive (false);
					Debug.Log("Event : フェードイン");
				}
			}
		}
	}
}