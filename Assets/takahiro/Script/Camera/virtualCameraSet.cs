using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virtualCameraSet : MonoBehaviour
{

	[SerializeField]
	private Cinemachine.CinemachinePath cameraPsth;
	[SerializeField]
	private Cinemachine.CinemachineSmoothPath lookAtPsth;
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float[] per;
	[SerializeField]
	private Transform[] obj;

	private void Start()
	{
		// objを各pathにセット
		for(int i = 0; i < obj.Length; ++i)
		{
			Vector3 pos = cameraPsth.m_Waypoints[i].position;
			obj[i].position = new Vector3(pos.x, pos.y, pos.z);
		}
	}

	public Cinemachine.CinemachinePath getcameraPath()
	{
		return cameraPsth;
	}

	public Cinemachine.CinemachineSmoothPath getlookAtPath()
	{
		return lookAtPsth;
	}
	public float getSpeed()
	{
		return moveSpeed;
	}

	public int getNumWaypoints()
	{
		return cameraPsth.m_Waypoints.Length - 1;
	}

	public float getSpeedChange(int wayPoint)
	{
		return per[wayPoint];
	}
}
