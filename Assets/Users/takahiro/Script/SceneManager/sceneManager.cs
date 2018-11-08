using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneManager : MonoBehaviour {

	public enum SCENE
	{
		SCENE_TITLE = 0,
		SCENE_GAME_NORMAL,
		SCENE_GAME_BOSS,
		SCENE_RESULT,
		SCENE_RANKING,
		SCENE_MAX
	}

	[SerializeField]
	private float fadeSecond;
	[SerializeField]
	private Image fadeImage;

	[SerializeField]
	private string[] sceneNames;

	private Dictionary<int, string> sceneNamesIdx;
	private bool fade;
	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		// シーンと列挙型の関連付け
		sceneNamesIdx = new Dictionary<int, string> {
			{(int)SCENE.SCENE_TITLE , sceneNames[0] },
			{(int)SCENE.SCENE_GAME_NORMAL , sceneNames[1] },
			{(int)SCENE.SCENE_GAME_BOSS , sceneNames[2] },
			{(int)SCENE.SCENE_RESULT , sceneNames[3] },
			{(int)SCENE.SCENE_RANKING , sceneNames[4] },
		};
	}

	private void Update()
	{
		// fade
	}

	public void SceneChange(SCENE scene)
	{
		SceneManager.LoadScene(sceneNames[(int)scene]);
	}
}
