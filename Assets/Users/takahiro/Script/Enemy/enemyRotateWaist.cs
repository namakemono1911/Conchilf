using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRotateWaist : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 camera = Camera.main.transform.position;


		this.transform.LookAt(camera);
	}
}
