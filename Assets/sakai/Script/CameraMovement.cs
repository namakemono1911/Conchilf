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
        public Vector3          End_Position;       // 終了座標

        public List<Vector3>    Position_List;      // 移動先座標リスト
        public List<float>      MovementSec_List;   // 移動時間リスト
    }

    [SerializeField]
    private Option OptionInfo;              // シリアライズ系統の情報


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
