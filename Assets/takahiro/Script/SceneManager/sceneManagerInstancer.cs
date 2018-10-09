using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManagerInstancer : MonoBehaviour {

	[SerializeField]
	GameObject sceneManager;

	private void Awake()
	{
		GameObject gm;
		gm = GameObject.Find("sceneManager");

		if(gm == null)
		{
			GameObject.Instantiate(sceneManager);
		}

		Destroy(this.gameObject);

	}
}
