using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour {

	public enum SCENE
	{
		SCENE_TITLE = 0,
		SCENE_GAME,
		SCENE_MAX
	}

	private Dictionary<int, string> sceneNames;

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		// シーンと列挙型の関連付け
		sceneNames = new Dictionary<int, string> {
			{(int)SCENE.SCENE_TITLE , "title" },
			{(int)SCENE.SCENE_GAME , "spy" },
		};
	}

	public void SceneChange(SCENE scene)
	{
		SceneManager.LoadScene(sceneNames[(int)scene]);
	}



}
