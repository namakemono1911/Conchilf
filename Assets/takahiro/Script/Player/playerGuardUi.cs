using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class playerGuardUi : MonoBehaviour {

	[SerializeField]
	private float moveTime; // 移動時間
	[SerializeField]
	private Transform movePosStart; // 移動開始位置
	[SerializeField]
	private Transform movePosEnd;   // 移動終了位置

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

		if (Input.GetKeyDown(KeyCode.G))
		{
			guardStart();
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			guardEnd();
		}

		if (guard != true)
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
					this.transform.DOMove(movePosEnd.position, moveTime);
				}
				else
				{
					guard = false;
					this.transform.DOMove(movePosStart.position, moveTime);
				}
			}
		}
	}

	public void guardStart()
	{
		Debug.Log("guardStart");
		guard = true;
		move = true;
		start = true;
	}

	public void guardEnd()
	{
		Debug.Log("guardEnd");
		move = true;
		start = false;
	}
}
