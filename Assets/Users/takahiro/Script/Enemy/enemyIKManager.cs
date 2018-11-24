using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyIKManager : MonoBehaviour {

	[SerializeField]
	private Transform waistTrasform;    // 腰のtransform

	private Animator myAnimator;
	private Vector3 targetPos;

	// Use this for initialization
	void Start () {
		// animatorの取得
		myAnimator = GetComponent<Animator>();

		targetPos = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		targetPos = Camera.main.transform.position;
	}


	private void LateUpdate()
	{
		Transform camera = Camera.main.transform;
		Vector3 cameraPos = camera.position;
		cameraPos = new Vector3(camera.position.x, this.transform.position.y, camera.position.z);
		this.transform.LookAt(cameraPos);
	}


	private void OnAnimatorIK(int layerIndex)
	{
		myAnimator.SetLookAtWeight(1.0f, 0.3f, 1.0f, 0.0f, 0f);
		myAnimator.SetLookAtPosition(targetPos);
	}

}
