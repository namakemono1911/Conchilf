using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInput : MonoBehaviour {

	public GameObject policeCar = null;
	public GameObject Explotion = null;
	public GameObject Black = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("1"))
		{
			Time.timeScale = 0.3f;
			Explotion.SetActive (true);
		}
		if (Input.GetKeyDown ("2"))
		{
			Time.timeScale = 1.0f;
			Black.SetActive (true);
		}
		if (Input.GetKey ("3"))
		{
			if (Input.GetKeyDown (KeyCode.Space))
			{
				Black.SetActive (false);
				policeCar.SetActive (false);
			}
		}
	}
}
