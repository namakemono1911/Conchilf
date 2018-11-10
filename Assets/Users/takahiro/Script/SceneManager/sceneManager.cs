using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneManager : singletonMonobihavior<sceneManager> {

	public enum SCENE
	{
		SCENE_TITLE = 0,
		SCENE_GAME_NORMAL_1,
		SCENE_GAME_BOSS_1,
        SCENE_GAME_NORMAL_2,
        SCENE_GAME_BOSS_2,
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
	private bool fadeIn;
    private float fadeSpeed;
    private SCENE nowScene;
    private SCENE beforScene;
    private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		// シーンと列挙型の関連付け
		sceneNamesIdx = new Dictionary<int, string> {
			{(int)SCENE.SCENE_TITLE , sceneNames[0] },
			{(int)SCENE.SCENE_GAME_NORMAL_1 , sceneNames[1] },
			//{(int)SCENE.SCENE_GAME_BOSS_1 , sceneNames[2] },
			//{(int)SCENE.SCENE_RESULT , sceneNames[3] },
			//{(int)SCENE.SCENE_RANKING , sceneNames[4] },
		};

        beforScene = SCENE.SCENE_TITLE;
        nowScene = SCENE.SCENE_TITLE;

    }

    private void Start()
    {
        fadeSpeed = 1.0f / fadeSecond;
    }

    private void Update()
	{
        Debug.Log(fade);

        // fade
        if (fade != true)
        {
            return;
        }

        Color color = fadeImage.color;

        if (fadeIn)
        {
            color.a -= fadeSpeed;

            if (color.a <= 0.0f)
            {
                color.a = 0.0f;
                fade = false;
                fadeIn = false;
            }

        }
        else
        {
            color.a += fadeSpeed;

            if (color.a >= 1.0f)
            {
                color.a = 1.0f;
                fadeIn = true;
                SceneManager.LoadScene(sceneNames[(int)nowScene]);
            }
        }

        fadeImage.color = new Color(color.r, color.g, color.b, color.a);

    }

	public void SceneChange(SCENE scene)
	{
        beforScene = nowScene;
        nowScene = scene;
        fade = true;
        fadeIn = false;
	}
}
