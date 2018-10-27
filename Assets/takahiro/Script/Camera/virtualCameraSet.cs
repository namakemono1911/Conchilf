using System.Collections;
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

	private void Start()
	{
		nowWaypoint = 1;
		nearWaypoint = true;
		changeDistance = 1.0f;
		// objを各pathにセット
		obj = new Vector3[obj.Length];
		for (int i = 0; i < obj.Length; ++i)
		{
			Vector3 pos = cameraPsth.m_Waypoints[i].position;
			obj[i] = new Vector3(pos.x, pos.y, pos.z);
		}
	}

	private void Update()
	{
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
				}

				nearWaypoint = true;
				Debug.Log("速度変化" + nowWaypoint);
			}
		}
		else
		{
			// 一定距離離れたら
			// 今のwaypointとカメラとの距離を比較
			float distance = Vector3.Distance(Camera.main.transform.position, obj[nowWaypoint]);
			// 一定以上なら
			if (distance > changeDistance)
			{

				nearWaypoint = false;
				Debug.Log("もくひょう" + nowWaypoint);
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
		if (CameraPer.Length <= nowWaypoint - 1)
		{
			return cameraPsth.PathLength / moveCameraSecond / 60.0f;
		}
		return cameraPsth.PathLength / moveCameraSecond / 60.0f * CameraPer[nowWaypoint - 1];
	}
	public float getLookAtSpeed()
	{
		if (LookatPer.Length <= nowWaypoint - 1)
		{
			return lookAtPsth.PathLength / moveLookAtSecond / 60.0f;
		}
			
		return lookAtPsth.PathLength / moveLookAtSecond / 60.0f * LookatPer[nowWaypoint - 1];
	}

	public int getNumWaypoints()
	{
		return cameraPsth.m_Waypoints.Length - 1;
	}

}
