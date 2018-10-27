using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour {

	[SerializeField]
	private virtualCameraSet[] cameraSet;
	[SerializeField]
	private int setNum;

	private float speedCamera;
	private float speedLookAt;
	private float nowCamera;
	private float nowLookAt;
	private int oldPath;

	// Use this for initialization
	void Start () {
		speedCamera = cameraSet[setNum].getCameraSpeed();
		speedLookAt = cameraSet[setNum].getLookAtSpeed();

		nowCamera = 0.0f;
		nowLookAt = 0.0f;
		oldPath = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(setNum >= cameraSet.Length)
		{
			setNum = 0;
		}


		speedCamera = cameraSet[setNum].getCameraSpeed();
		speedLookAt = cameraSet[setNum].getLookAtSpeed();

		// 座標移動
		transform.position = cameraSet[setNum].getcameraPath().EvaluatePositionAtUnit(nowCamera, Cinemachine.CinemachinePathBase.PositionUnits.Distance);
		// カメラ角度
		transform.LookAt(cameraSet[setNum].getlookAtPath().EvaluatePositionAtUnit(nowLookAt, Cinemachine.CinemachinePathBase.PositionUnits.Distance));
		//Debug.Log (nowLookAt);
		nowCamera += speedCamera;
		nowLookAt += speedLookAt;



		if (Input.GetKeyDown(KeyCode.Space))
			nextMove();
	}

	public void nextMove()
	{
		setNum += 1;

		if (setNum >= cameraSet.Length)
		{
			setNum = 0;
		}

		speedCamera = cameraSet[setNum].getCameraSpeed();
		speedLookAt = cameraSet[setNum].getLookAtSpeed();
		nowCamera = 0.0f;
		nowLookAt = 0.0f;

	}
}
