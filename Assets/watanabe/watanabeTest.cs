using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watanabeTest : MonoBehaviour {

	Rigidbody rb;
	float move = 1.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 now = this.transform.position;

		if (Input.GetKey ("w")) { now += new Vector3( 0.0f, 0.0f, move ); }
		if (Input.GetKey ("s")) { now += new Vector3( 0.0f, 0.0f, -move ); }
		if (Input.GetKey ("a")) { now += new Vector3( -move, 0.0f, 0.0f ); }
		if (Input.GetKey ("d")) { now += new Vector3( move, 0.0f, 0.0f ); }

		if (Input.GetKey ("space")) { now += new Vector3( 0.0f, move, 0.0f ); }
		if (Input.GetKey ("return")) { now += new Vector3( 0.0f, -move, 0.0f ); }

		if (Input.GetKey ("left")) { rb.transform.Rotate (new Vector3 (0, -10, 0)); }
		if (Input.GetKey ("right")) { rb.transform.Rotate (new Vector3 (0, 10, 0)); }

		this.transform.position = now;
	}
}
