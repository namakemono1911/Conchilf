using System.Collections;
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
    private InputFacade reticle;        //レティクル

    [SerializeField]
    private GameObject messageBox;      //メッセージボックス

    [SerializeField]
    private messageController messageController;

    private string text;                //入力テキスト


    // Use this for initialization
    void Start()
    {
        playerName.text = "";
        messageController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (reticle.whetherShot())
        {
            if (text == "BK")
            {
                var str = playerName.text;
                playerName.text = str.Remove(str.Length - 1);
            }
            else if (text == "END")
            {
                //確認画面
                messageBox.SetActive(true);
                messageController.enabled = true;
                enabled = false;
            }
            else if (playerName.text.Length < maxNum)
            {
                playerName.text += text;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "inputTable")
        {
            text = collision.gameObject.GetComponent<Text>().text;

            if (text == "SP")
                text = " ";
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "inputTable")
        {
            text = "";
        }
    }
}
