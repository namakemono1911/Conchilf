using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManagerInstancer : MonoBehaviour {

	[SerializeField]
	GameObject sceneManager;
	[SerializeField]
	private bool instance;

	private void Awake()
	{
		if(instance == true)
		{
			GameObject.Instantiate(sceneManager);
		}

		Destroy(this.gameObject);

	}
}
