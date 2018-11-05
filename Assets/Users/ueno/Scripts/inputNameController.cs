﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputNameController : MonoBehaviour
{
    [SerializeField]
    private int maxNum;                 //最大文字数

    [SerializeField]
    private Text playerName;            //プレイヤー名

    [SerializeField]
    private reticleController reticle;  //レティクル

    private string text;                //入力テキスト


    // Use this for initialization
    void Start()
    {
        playerName.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (reticle.whetherShot() && playerName.text.Length <= maxNum)
        {
            if (text == "BK")
            {
                var str = playerName.text;
                playerName.text = str.Remove(str.Length - 1);
            }
            else if (text == "END")
            {
                //確認画面
            }
            else
            {
                playerName.text += text;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        text = collision.gameObject.GetComponent<Text>().text;

        if (text == "SP")
            text = " ";
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        text = "";
    }
}
