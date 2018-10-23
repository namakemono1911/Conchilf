using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour {

	[SerializeField]
	private virtualCameraSet[] cameraSet;
	[SerializeField]
	private int setNum;

	private float speed;
	private float now;
	private float per;
	private int oldPath;

	// Use this for initialization
	void Start () {
		speed = 0.01f * cameraSet[setNum].getSpeed();
		per = cameraSet[setNum].getSpeedChange(setNum);
		Debug.Log((float)cameraSet[setNum].getNumWaypoints());
		now = 0.0f;
		per = 1.0f;
		oldPath = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(setNum >= cameraSet.Length)
		{
			setNum = 0;
		}

		transform.position = cameraSet[setNum].getcameraPath().EvaluatePositionAtUnit(now, Cinemachine.CinemachinePathBase.PositionUnits.PathUnits);
		now += speed * per;

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

		speed = 0.01f * cameraSet[setNum].getSpeed();
		per = cameraSet[setNum].getSpeedChange(setNum);
		now = 0.0f;


	}
}
