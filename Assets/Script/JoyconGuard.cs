using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconGuard : MonoBehaviour {

	[SerializeField]
	private GameObject guardObj;
	[SerializeField]
	private MyJoycon joycons;

	private bool guard;

	// Use this for initialization
	void Start () {
		guard = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == guardObj)
		{
			guard = true;
			Debug.Log("guard start");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == guardObj)
		{
			guard = false;
			Debug.Log("guard end");
		}
	}

	// ガード判定
	public bool GetGuard()
	{
		return guard;
	}
}
