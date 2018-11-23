using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshRendererHide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MeshRenderer rend = GetComponent<MeshRenderer> ();
		rend.enabled = false;
	}
}
