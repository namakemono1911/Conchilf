using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wordUITester : MonoBehaviour {

	[SerializeField]
	private wordUIManager wordUIManager;
	[SerializeField]
	private int debugIdx;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
		{
			wordUIManager.startPlayWordUI(debugIdx);
		}
	}
}
