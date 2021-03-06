﻿///////////////////////////////////////////////
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

    // エディット関連のパラメータ
    public static class Edit
    {
        // プレハブパス
        public const string ENEMY_00 = ("Prefub/Enemy_Soldier");
    }

	
    // 敵の情報の識別数値
    public enum EnemyAnalyze
    {
        Enemy_Type,
        Enemy_Wave,
        Enemy_MoveSec,
        Enemy_Pos_x,
        Enemy_Pos_y,
        Enemy_Pos_z,
        Enemy_Move_x,
        Enemy_Move_y,
        Enemy_Move_z,
        Enemy_Scene,
        Enemy_Param_Max,
    }

}
