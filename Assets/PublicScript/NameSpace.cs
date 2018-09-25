///////////////////////////////////////////////
//
//  Title   : ネームスペース
//  Auther  : Shun Sakai 
//  Date    : 2018/09/20
//  Update  : 
//  Memo    : ユーザー定義用のネームスペース
//
///////////////////////////////////////////////
using System.Collections;
using UnityEngine;

// ネームスペース
namespace Common
{
    // システム関連のパラメータ
    public static class OptionDefine
    {
        public const int FRAME_RATE  = (60);     // フレームレート希望値
        public const int WINDOW_H    = (1080);   // ウィンドウサイズ(高さ)
        public const int WINDOW_V    = (1920);   // ウィンドウサイズ(幅)
    }

    // 初期化用
    public static class Initialize
    {
        public const int    INIT_INT    = (0);      // int型初期化値
        public const float  INIT_FLOAT  = (0.0f);   // float型初期化値
    }

    // シーン名
    public static class SceneName
    {
        public const string MANAGER_SCENE = ("Manager");        // マネージャーシーン

    }


    // ゲームシーン関連のパラメータ
    public static class GameStatus
    {        
        public const int    GAMESCENE_INDEX     = (0);          // ゲームシーン番号
        public const int    TOUCHCOUNT_FALSE    = (0);          // タッチカウントの無効値

        public const float  STAGE_INTERVAL       = (1.5f);      // ステージ間インターバル  
    }
}
