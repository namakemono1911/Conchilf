///////////////////////////////////////////////
//
//  Title   : ボスシーンドラマパート処理
//  Auther  : Shun Sakai 
//  Date    : 2018/11/19
//  Update  : 
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneDramaPart : MonoBehaviour {

    // ドラマパートの構造体
    [System.Serializable]
    public struct DramaInfo
    {
        public float StartTime;
        public float EndTime;

        public AudioSource PlayVoice;
    }

    // メンバ
    private List<DramaInfo> DramaList;          // ドラマ情報リスト
    private bool            DramaPlay_Enable;   // 再生フラグ
    private float           TimeElspeed;        // タイマー
    private float           StartTime;          // スタートタイム
    private int             DramaIndex;         // ドラマ情報インデックス

    // 初期化処理
    private void Awake()
    {
        // リスト作成
        DramaList = new List<DramaInfo>();
        DramaPlay_Enable = false;
        TimeElspeed = 0.0f;
        DramaIndex = 0;
        StartTime = 0.0f;
    }


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// 更新処理
	void Update ()
    {
        // ドラマ再生処理
	    if(DramaPlay_Enable)
        {
            // ドラマ再生
            PlayDrama();

        }

	}

    // ドラマ再生処理
    private void PlayDrama()
    {
        // データ確認
        if(DramaList == null)
        {
            // 初期化
            TimeElspeed = 0.0f;
            DramaIndex = 0;
            DramaPlay_Enable = false;
            return;
        }



        // タイマー処理
        TimeElspeed = Time.time - StartTime;

        // ドラマリストチェック
        if(DramaList[DramaIndex].StartTime < TimeElspeed && DramaList[DramaIndex].EndTime > TimeElspeed)
        {
            // ボイス再生
            DramaList[DramaIndex].PlayVoice.Play();
            // インデックスカウントアッウ
            DramaIndex++;
        }

        // インデックスチェック
        if(DramaIndex >= DramaList.Count)
        {
            // ドラマパート終了
            TimeElspeed = 0.0f;
            DramaIndex = 0;
            DramaPlay_Enable = false;

        }
    }
    
    // ドラマ情報セット
    public void SetDramaDate_And_Play( List<DramaInfo> buf)
    {
        // フラグチェック
        if (DramaPlay_Enable == false)
        {
            // リスト初期化
            DramaList.Clear();

            if (buf == null) { return; }

            // ドラマ情報格納
            DramaList = buf;

            // スタートタイム取得
            StartTime = Time.time;

            // 初期化
            TimeElspeed = 0.0f;
            DramaIndex = 0;
            DramaPlay_Enable = true;
        }
    }

    // ドラマ再生フラグゲッタ
    public bool Get_DramaPlayEnable()
    {
        return DramaPlay_Enable;
    }

}
