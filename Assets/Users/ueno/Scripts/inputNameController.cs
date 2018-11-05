using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputNameController : MonoBehaviour {
    [SerializeField]
    private int charNum;            //名前の文字数

    [SerializeField]
    private Text playerName;        //プレイヤー名

    [SerializeField]
    private Transform inputTable;  //入力文字テーブル

    private string text;            //入力テキスト

	// Use this for initialization
	void Start ()
    {
        playerName.text = "";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            text = collision.gameObject.GetComponent<Text>().text;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        text = "";
    }
}
