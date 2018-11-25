using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontScene : MonoBehaviour {

	private bool scene;
	// Use this for initialization
	void Start () {
		scene = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!scene)
		{
			scene = true;
			SceneManager.LoadScene("logo");
		}
	}
}
