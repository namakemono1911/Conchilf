using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRotateWaist : MonoBehaviour {

	private void LateUpdate()
	{
		Transform camera = Camera.main.transform;
		Vector3 cameraPos = camera.position;
		cameraPos = new Vector3(camera.position.x , this.transform.position.y , camera.position.z);
		this.transform.LookAt(cameraPos);
	}
}
