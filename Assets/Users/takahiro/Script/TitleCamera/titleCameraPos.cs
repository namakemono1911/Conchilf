using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleCameraPos : MonoBehaviour {

	[SerializeField]
	private Transform startTransform;
	[SerializeField]
	private Transform endTransform;

	public Transform getStartTransform()
	{
		return startTransform;
	}
	public Transform getEndTransform()
	{
		return endTransform;
	}
}
