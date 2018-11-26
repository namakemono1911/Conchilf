///////////////////////////////////////////////
//
//  Title   : ライフバー処理
//  Auther  : Shun Sakai 
//  Date    : 2018/10/25
//  Update  : 2018/11/26
//  Memo    : 効果音処理を追加
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ライフバー処理
public class Lifebar : MonoBehaviour
{
    [SerializeField]
    private Color[] BarColor;

    private List<Image> MemoryList;

    private int MemoryCount;
    private int BarCount;

    // 初回処理
    void Start ()
    {
        // 初期化
        MemoryCount = 10;
        BarCount    = 4;

        MemoryList = new List<Image>();

        // メモリ取得
        for (int i = 0; i <  this.transform.childCount; i++)
        {
            // 全イメージ取得
            MemoryList.Add( this.transform.GetChild(i).GetComponent<Image>());

            MemoryList[i].color = BarColor[BarCount];
        }
        
        BarCount = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // メモリを減らす
    public void MemoryBreak()
    {
        MemoryCount--;

        // 現在のバーが最後のバーだったら
        if (BarCount == -1)
        {
            // メモリの破壊
            MemoryList[MemoryCount].gameObject.SetActive(false);
        }
        else
        {
            // メモリの色変え
            MemoryList[MemoryCount].color = BarColor[BarCount];
        }
        // メモリが減り切ったか
        if(MemoryCount <= 0)
        {
            BarCount--;
            MemoryCount = 10;
        }
    }
}
