using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour {

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	public void SceneChange(string scene)
	{
		SceneManager.LoadScene(scene);
	}

}
