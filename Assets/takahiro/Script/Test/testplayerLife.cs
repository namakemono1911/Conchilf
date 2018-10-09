using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testplayerLife : MonoBehaviour {

	[SerializeField]
	private playerLifeUI plife;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			plife.addPlayerLife(1);
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			plife.addPlayerLife(-1);
		}
	}
}
