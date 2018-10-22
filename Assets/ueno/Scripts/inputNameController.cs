using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputNameController : MonoBehaviour {
    [SerializeField]
    private string offset;          //文字オフセット

    [SerializeField]
    private int charNum;            //名前の文字数

    [SerializeField]
    private Text playerName;        //プレイヤー名

    [SerializeField]
    private Transform inputTable;  //入力文字テーブル

	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
