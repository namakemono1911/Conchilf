using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class playerGuardUi : MonoBehaviour {

	[SerializeField]
	private Transform guardUI;
	[SerializeField]
	private float moveTime; // 移動時間
	[SerializeField]
	private Transform movePosStart; // 移動開始位置
	[SerializeField]
	private Transform movePosEnd;	// 移動終了位置

	private bool guard;
	private bool move;
	private bool start;
	// Use this for initialization
	void Start () {
		guard = false;
		start = true;
		move = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(guard != true)
		{
			return;
		}
		else
		{
			if(move)
			{
				// 移動
				if(start)
				{
					guardUI.DOMove(movePosEnd, moveTime);
				}
				else
				{
					guardUI.DOMove(movePosStart, moveTime);
				}
			}
		}
	}

	public void guardStart()
	{
		guard = true;
		move = true;
		start = true;
	}

	public void guardEnd()
	{
		move = true;
		start = false;
	}
}
