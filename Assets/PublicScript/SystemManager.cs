///////////////////////////////////////////////
//
//  Title   : システムマネージャ
//  Auther  : Shun Sakai 
//  Date    : 2018/09/20
//  Update  : リファクタリング
//  Memo    : システム全般の管理、Awake前に呼び出し
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

[System.Serializable]
class option
{
    public bool DebugLogEnable;     // デバッグログの表示フラグ
    public int TargetFramerate;    // 目標とする秒間フレームレート数
}


// システムマネージャー
public class SystemManager : SingletonMonoBehaviour<SystemManager>
{
    // メンバ
    private int     FrameCount;         // フレームカウンタ
    private float   PrevTime;           // 前回処理時間

    [SerializeField]
    private option  OptionValue;        // オプション数値
  

    // 起動時の処理(すべての処理より先)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    void First_Init()
    {
        // デバッグログを無効に
        Debug.unityLogger.logEnabled = OptionValue.DebugLogEnable;

        // 目標フレームレートを、FRAME_RATE内の数値に設定
        Application.targetFrameRate = OptionValue.TargetFramerate;
    }

    // 初回処理
    void Start ()
    {
        // フレームカウンタの初期化
        FrameCount  = Initialize.INIT_INT;
        PrevTime    = Initialize.INIT_FLOAT;
	}
	
    // 更新処理
	void Update ()
    {
        // フレームレート観測
        ++FrameCount;
        float time = Time.realtimeSinceStartup - PrevTime;

        if (time >= 0.5f)
        {
            Debug.Log("<color=red>■フレームレート  :   </color>" + FrameCount / time);

            // カウンタリセット
            FrameCount = Initialize.INIT_INT;
            PrevTime    = Time.realtimeSinceStartup;
        }
	}
}
