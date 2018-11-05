//-----------------------------------------------------
//
//  Title   :   カメラ移動処理
//  Auther  :   Shun Sakai
//  Date    :   2018/10/11   
//  Update  :   
//  Memo    :   
//
//-----------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    // シリアライズ
    [System.Serializable]
    class Option
    {
        public int Value;                           // リストの要素数

        public Vector3          First_Position;     // 初期座標
        public Quaternion       First_Rotation;     // 初期角度

        public Vector3          End_Position;       // 終了座標

        public List<Vector3>    Position_List;      // 移動先座標リスト
        public List<float>      MovementSec_List;   // 移動時間リスト
    }

    [SerializeField]
    private Option  OptionInfo;                     // シリアライズ系統の情報

    private bool    Move_Enable;                    // 移動フラグ
    private int     WaveIndex;                      // 現在ウェーブ数
    private Vector3 TargetPosition;                 // ターゲット座標
    private float   MovementSec;                    // 移動時間

    // 初回処理
    private void Awake()
    {
        // 初期座標
        this.gameObject.transform.position = OptionInfo.First_Position;
        // 初期角度
        this.gameObject.transform.rotation = OptionInfo.First_Rotation;

    }


    // 初期化処理
    void Start () {
		
	}
	
	// 更新処理
	void Update ()
    {
        var pos = this.gameObject.transform.position;


        // 移動処理
        if (Move_Enable == true)
        {
            // 


            // 移動先へ移動 (座標と時間)



            // 移動先到達
            if(pos == TargetPosition)
            {
                Move_Enable = false;
            }

        }






	}

    // 移動処理
    private void Set_CameraMove(int Wave)
    {
        // 移動開始
        if(Move_Enable == false)
        {
            Move_Enable = true;
            WaveIndex   = Wave;

            // 移動先座標を取得
            TargetPosition = OptionInfo.Position_List[WaveIndex];
            // 移動間時間を取得
            MovementSec = OptionInfo.MovementSec_List[WaveIndex];
        }
    }

}
