using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyIKManager : MonoBehaviour {

	[SerializeField]
	private Transform neckTrasform;    // 首のtransform

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

	private void OnAnimatorIK(int layerIndex)
	{
		myAnimator.SetLookAtWeight(1.0f, 0.0f, 1.0f, 0.0f, 0f);
		myAnimator.SetLookAtPosition(targetPos);
	}

}
