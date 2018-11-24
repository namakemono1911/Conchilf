using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toratoratora : MonoBehaviour {

	[SerializeField]
	private float moveSpeed;

	private Rigidbody rb;
	private int life;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce(Vector3.left * moveSpeed);
	}
}
