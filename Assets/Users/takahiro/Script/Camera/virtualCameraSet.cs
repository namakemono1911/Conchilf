﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virtualCameraSet : MonoBehaviour
{

	[SerializeField]
	private Cinemachine.CinemachinePath cameraPsth;
	[SerializeField]
	private Cinemachine.CinemachinePath lookAtPsth;
	[SerializeField]
	private float moveCameraSecond;
	[SerializeField]
	private float moveLookAtSecond;

	[SerializeField]
	private float[] CameraPer;
	[SerializeField]
	private float[] LookatPer;

	private int nowWaypoint;
	private bool nearWaypoint;
	private float changeDistance;
	private Vector3[] obj;

	private int nowWaypointLook;
	private bool nearWaypointLook;
	private float changeDistanceLook;
	private Vector3[] objLook;
	private Vector3 lookPos;
	private float nowLookAt;
	private float nowCamera;
	private bool isStart;
	private bool isEnd;

	private void Start()
	{
		isEnd = false;
		nowLookAt = 0.0f;
		nowCamera = 0.0f;

		nowWaypoint = 1;
		nearWaypoint = true;

		nowWaypointLook = 1;
		nearWaypointLook = true;

		changeDistance = 1.0f;
		changeDistanceLook = 8.0f;
		// objを各pathにセット
		obj = new Vector3[cameraPsth.m_Waypoints.Length];
		for (int i = 0; i < obj.Length; ++i)
		{
			Vector3 pos = cameraPsth.m_Waypoints[i].position + cameraPsth.gameObject.transform.position;
			obj[i] = new Vector3(pos.x, pos.y, pos.z);
		}

		objLook = new Vector3[lookAtPsth.m_Waypoints.Length];
		for (int i = 0; i < objLook.Length; ++i)
		{
			Vector3 pos = lookAtPsth.m_Waypoints[i].position + lookAtPsth.gameObject.transform.position;
			objLook[i] = new Vector3(pos.x, pos.y, pos.z);
		}
	}

	private void FixedUpdate()
	{

		if(!isStart)
		{
			return;
		}

		// 近くにwaypointが無い
		if (nearWaypoint == false)
		{
			// 今のwaypointとカメラとの距離を比較
			float distance = Vector3.Distance(Camera.main.transform.position, obj[nowWaypoint]);

			// 一定以下なら
			if (distance < changeDistance)
			{
				// 次のwaypointに変更
				++nowWaypoint;
				if (nowWaypoint >= obj.Length)
				{
					nowWaypoint = obj.Length - 1;
					isEnd = true;
				}

				nearWaypoint = true;
			}
		}
		else
		{
			// 一定距離離れたら
			// 今のwaypointとカメラとの距離を比較
			float distance = Vector3.Distance(Camera.main.transform.position, obj[nowWaypoint - 1]);
			// 一定以上なら
			if (distance > changeDistance)
			{
				nearWaypoint = false;
			}
		}

		// 注視点の更新
		nowLookAt += getLookAtSpeed();
		lookPos = getcameraPath().EvaluatePositionAtUnit(nowLookAt, Cinemachine.CinemachinePathBase.PositionUnits.Distance);


		// 近くにwaypointが無い
		if (nearWaypointLook == false)
		{
			// 今のwaypointとカメラとの距離を比較
			float distance = Vector3.Distance(lookPos, objLook[nowWaypointLook]);
			// 一定以下なら
			if (distance < changeDistanceLook)
			{
				// 次のwaypointに変更
				++nowWaypointLook;
				if (nowWaypointLook >= objLook.Length)
				{
					nowWaypointLook = objLook.Length - 1;

				}

				nearWaypointLook = true;
			}
		}
		else
		{
			// 一定距離離れたら
			// 今のwaypointとカメラとの距離を比較
			float distance = Vector3.Distance(lookPos, objLook[nowWaypointLook - 1]);
			// 一定以上なら
			if (distance > changeDistanceLook)
			{
				nearWaypointLook = false;
			}
		}
	}

	public Cinemachine.CinemachinePath getcameraPath()
	{
		return cameraPsth;
	}

	public Cinemachine.CinemachinePath getlookAtPath()
	{
		return lookAtPsth;
	}

	public float getCameraSpeed()
	{
		// nowWaypointが0の場合のバグを予防する
		if (nowWaypoint <= 0)
		{
			return 0.0f;
		}

		if (CameraPer.Length <= nowWaypoint - 1)
		{
			return cameraPsth.PathLength / moveCameraSecond / 60.0f;
		}
		return cameraPsth.PathLength / moveCameraSecond / 60.0f * CameraPer[nowWaypoint - 1];
	}
	public float getLookAtSpeed()
	{
		// nowWaypointが0の場合のバグを予防する
		if (nowWaypoint <= 0)
		{
			return 0.0f;
		}

		if (LookatPer.Length <= nowWaypointLook - 1)
		{
			return lookAtPsth.PathLength / moveLookAtSecond / 60.0f;
		}

		return lookAtPsth.PathLength / moveLookAtSecond / 60.0f * LookatPer[nowWaypointLook - 1];
	}

	public int getNumWaypoints()
	{
		return cameraPsth.m_Waypoints.Length - 1;
	}

	public void debugMove()
	{
		nowLookAt = 0.0f;

		nowWaypoint = 1;
		nearWaypoint = true;

		nowWaypointLook = 1;
		nearWaypointLook = true;

		changeDistance = 1.0f;
		changeDistanceLook = 8.0f;

		// objを各pathにセット
		obj = new Vector3[cameraPsth.m_Waypoints.Length];
		for (int i = 0; i < obj.Length; ++i)
		{
			Vector3 pos = cameraPsth.m_Waypoints[i].position + cameraPsth.gameObject.transform.position;
			obj[i] = new Vector3(pos.x, pos.y, pos.z);
		}

		objLook = new Vector3[lookAtPsth.m_Waypoints.Length];
		for (int i = 0; i < objLook.Length; ++i)
		{
			Vector3 pos = lookAtPsth.m_Waypoints[i].position + lookAtPsth.gameObject.transform.position;
			objLook[i] = new Vector3(pos.x, pos.y, pos.z);
		}
	}

	public void start()
	{
		isStart = true;
	}

	public void end()
	{
		isStart = false;
	}

	public bool endMove()
	{
		if(isStart)
		{
			return isEnd;
		}

		return false;
	}
}
