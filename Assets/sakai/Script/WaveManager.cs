///////////////////////////////////////////////
//
//  Title   : ウェーブ管理処理
//  Auther  : Shun Sakai 
//  Date    : 2018/10/20
//  Update  : 
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ウェーブマネージャ
public class WaveManager : MonoBehaviour
{
    // インスペクター上情報
    [System.Serializable]
    public struct Option
    {
        public int WaveNum;
    }

    [SerializeField]
    private EnemyManager    E_Manager;      // エネミーマネージャ

    [SerializeField]
    private Option          Option_Info;    // オプション値情報
    
    // 次回ウェーブ数
    private int NextWaveNum;

    // 初回処理
    void Awake()
    {
        if(Option_Info.WaveNum < 0)
        {
            Option_Info.WaveNum = 0;
        }

        NextWaveNum = -1;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        // 次回ウェーブ予約確認
        if(Option_Info.WaveNum != NextWaveNum)
        {
            // 次回ウェーブ情報作成(内部で前回ウェーブの情報を全削除)
            E_Manager.WaveDateMake(NextWaveNum);
            
            // 現在ウエーブ数を更新
            Option_Info.WaveNum = NextWaveNum;
        }
#if UNITY_EDITOR

        debugmessage();      

#endif


    }


    // ウェーブ数のセッタ
    public void WaveSet(int Wavenum)
    {
        // 現在ウェーブの終了処理


        // 次回ウェーブの予約
        if(Wavenum >= 0)
        {
            NextWaveNum = Wavenum;
        }


    }

    // 次回ウェーブに進行
    public void WavetoNext()
    {
        // 現在ウェーブの終了処理


        // 次回ウェーブの予約
        NextWaveNum = Option_Info.WaveNum++;
    }


    // ゲームクリアへ移行
    public void To_GameClearScene()
    {

    }

    // ゲームオーバへ移行
    public void To_GameOverScene()
    {

    }

#if UNITY_EDITOR
    // デバッグ表示
    void debugmessage(){

        // ログ表示テキスト取得
        Text debuglog = GameObject.Find("MessageWindow").GetComponent<Text>();

        debuglog.text = "現在の再生Wave  :   " + Option_Info.WaveNum;
    }


#endif


}
