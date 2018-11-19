using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour {

	[SerializeField]
	private int debugCamera;
	[SerializeField]
	private bool debugMainCamera;
	[SerializeField]
	private bool debugLookAt;
	[SerializeField]
	private GameObject debugMainCameraObj;// [追加]
	[SerializeField]
	private GameObject debugLookAtObj;
	[SerializeField]
	private virtualCameraSet[] cameraSet;
	[SerializeField]
	private int setNum;

	private float speedCamera;
	private float speedLookAt;
	private float nowCamera;
	private float nowLookAt;
	private int oldPath;
	private bool isUpdate;
	// Use this for initialization
	void Start () {
		speedCamera = cameraSet[setNum].getCameraSpeed();
		speedLookAt = cameraSet[setNum].getLookAtSpeed();

		nowCamera = 0.0f;
		nowLookAt = 0.0f;
		oldPath = 0;

		cameraSet[setNum].start();
		isUpdate = true;
	}

	// Update is called once per frame
	void FixedUpdate() {

		if (isUpdate)
			return;

		if(setNum >= cameraSet.Length)
		{
			setNum = 0;
		}

		if(debugMainCamera == false && debugMainCameraObj != null)// [追加]
		{
			Destroy(debugMainCameraObj);
		}

		if(debugLookAt == false && debugLookAtObj != null)
		{
			Destroy(debugLookAtObj);
		}

		speedCamera = cameraSet[setNum].getCameraSpeed();
		speedLookAt = cameraSet[setNum].getLookAtSpeed();

		// 座標移動
		transform.position = cameraSet[setNum].getcameraPath().EvaluatePositionAtUnit(nowCamera, Cinemachine.CinemachinePathBase.PositionUnits.Distance);
		if (debugMainCameraObj != null)
		{
			debugMainCameraObj.transform.position = cameraSet[setNum].getcameraPath().EvaluatePositionAtUnit(nowCamera, Cinemachine.CinemachinePathBase.PositionUnits.Distance);// [追加]
		}
		// カメラ角度
		transform.LookAt(cameraSet[setNum].getlookAtPath().EvaluatePositionAtUnit(nowLookAt, Cinemachine.CinemachinePathBase.PositionUnits.Distance));
		if (debugLookAtObj != null)
		{
			debugLookAtObj.transform.position = cameraSet [setNum].getlookAtPath ().EvaluatePositionAtUnit (nowLookAt, Cinemachine.CinemachinePathBase.PositionUnits.Distance);
		}

		nowCamera += speedCamera;
		nowLookAt += speedLookAt;

		// 追加
		watanabeDebug();
	}

	void Update()
	{

		if (!isUpdate)
			return;

		if (setNum >= cameraSet.Length)
		{
			setNum = 0;
		}

		if (debugMainCamera == false && debugMainCameraObj != null)// [追加]
		{
			Destroy(debugMainCameraObj);
		}

		if (debugLookAt == false && debugLookAtObj != null)
		{
			Destroy(debugLookAtObj);
		}

		speedCamera = cameraSet[setNum].getCameraSpeed();
		speedLookAt = cameraSet[setNum].getLookAtSpeed();

		// 座標移動
		transform.position = cameraSet[setNum].getcameraPath().EvaluatePositionAtUnit(nowCamera, Cinemachine.CinemachinePathBase.PositionUnits.Distance);
		if (debugMainCameraObj != null)
		{
			debugMainCameraObj.transform.position = cameraSet[setNum].getcameraPath().EvaluatePositionAtUnit(nowCamera, Cinemachine.CinemachinePathBase.PositionUnits.Distance);// [追加]
		}
		// カメラ角度
		transform.LookAt(cameraSet[setNum].getlookAtPath().EvaluatePositionAtUnit(nowLookAt, Cinemachine.CinemachinePathBase.PositionUnits.Distance));
		if (debugLookAtObj != null)
		{
			debugLookAtObj.transform.position = cameraSet[setNum].getlookAtPath().EvaluatePositionAtUnit(nowLookAt, Cinemachine.CinemachinePathBase.PositionUnits.Distance);
		}

		nowCamera += speedCamera;
		nowLookAt += speedLookAt;

		// 追加
		watanabeDebug();
	}

	public void nextMove()
	{
		cameraSet[setNum].end();
		setNum += 1;

		if(setNum == 1)
		{
			isUpdate = false;
		}

		if (setNum >= cameraSet.Length)
		{
			return;
			setNum = 0;
		}

		cameraSet[setNum].start();
		speedCamera = cameraSet[setNum].getCameraSpeed();
		speedLookAt = cameraSet[setNum].getLookAtSpeed();
		nowCamera = 0.0f;
		nowLookAt = 0.0f;
	}

	public void setMove(int debug)
	{
		cameraSet[setNum].end();
		setNum = debug;

		Debug.Log(setNum);

		if (setNum >= cameraSet.Length)
		{
			setNum = 0;
		}

		cameraSet[setNum].debugMove();
		cameraSet[setNum].start();
		speedCamera = cameraSet[setNum].getCameraSpeed();
		speedLookAt = cameraSet[setNum].getLookAtSpeed();
		nowCamera = 0.0f;
		nowLookAt = 0.0f;
	}

	// 追加
	public void watanabeDebug()
	{
		// 前へ
		if (Input.GetKeyDown (KeyCode.LeftArrow))
		{
			setNum -= 1;

			if (setNum < 0)
			{
				setNum = cameraSet.Length -1;
			}

			cameraSet[setNum].debugMove();
			speedCamera = cameraSet[setNum].getCameraSpeed();
			speedLookAt = cameraSet[setNum].getLookAtSpeed();
			nowCamera = 0.0f;
			nowLookAt = 0.0f;
			Debug.Log ("NowCameraSet = [" + setNum + "]"+ cameraSet[setNum].gameObject.name);
		}
		// 次へ
		if (Input.GetKeyDown (KeyCode.RightArrow))
		{
			nextMove ();
			Debug.Log ("NowCameraSet = [" + setNum + "]"+ cameraSet[setNum].gameObject.name);
		}
		// 繰り返す
		if (Input.GetKeyDown (KeyCode.DownArrow))
		{
			cameraSet[setNum].debugMove();
			speedCamera = cameraSet[setNum].getCameraSpeed();
			speedLookAt = cameraSet[setNum].getLookAtSpeed();
			nowCamera = 0.0f;
			nowLookAt = 0.0f;
		}
		// 指定のカメラへ
		if (Input.GetKey(KeyCode.UpArrow))
		{
			setMove(debugCamera);
			return;
		}
	}

	public int getCameraMoveNum()
	{
		if (setNum >= cameraSet.Length)
		{
			setNum = 0;
		}

		return setNum;
	}

	public bool isMaxCamera()
	{
		if(setNum >= cameraSet.Length - 1)
		{
			return true;
		}

		return false;
	}

	public bool isEndMove()
	{
		return cameraSet[setNum].endMove();
	}
}
