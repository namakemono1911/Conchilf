using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virtualCameraSet : MonoBehaviour
{

	[SerializeField]
	private Cinemachine.CinemachineSmoothPath cameraPsth;
	[SerializeField]
	private Cinemachine.CinemachineSmoothPath lookAtPsth;
	[SerializeField]
	private float moveSpeed;


	public Cinemachine.CinemachineSmoothPath getcameraPath()
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
}
