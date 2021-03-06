﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneManager : singletonMonobihavior<sceneManager> {

	public enum SCENE
	{
		SCENE_DONT = 0,
		SCENE_LOGO,
		SCENE_TITLE,
		SCENE_GAME_NORMAL_1,
		SCENE_GAME_BOSS_1,
        SCENE_GAME_NORMAL_2,
        SCENE_GAME_BOSS_2,
        SCENE_RESULT_1,
        SCENE_RESULT_2,
        SCENE_INPUT_NAME,
		SCENE_RANKING,
		SCENE_THANKYOU,
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
			{(int)SCENE.SCENE_DONT , sceneNames[0] },
			{(int)SCENE.SCENE_LOGO , sceneNames[1] },
			{(int)SCENE.SCENE_TITLE , sceneNames[2] },
			{(int)SCENE.SCENE_GAME_NORMAL_1 , sceneNames[3] },
            {(int)SCENE.SCENE_GAME_BOSS_1 , sceneNames[4] },
			{(int)SCENE.SCENE_GAME_NORMAL_2 , sceneNames[5] },
			{(int)SCENE.SCENE_GAME_BOSS_2 , sceneNames[6] },
			{(int)SCENE.SCENE_RESULT_1 , sceneNames[7] },
            {(int)SCENE.SCENE_RESULT_2 , sceneNames[8] },
            {(int)SCENE.SCENE_INPUT_NAME , sceneNames[9] },
            {(int)SCENE.SCENE_RANKING , sceneNames[10] },
            {(int)SCENE.SCENE_THANKYOU , sceneNames[11] },
        };

        beforScene = SCENE.SCENE_TITLE;
        nowScene = SCENE.SCENE_TITLE;

    }

    private void Start()
    {
        fadeSpeed = 1.0f / (fadeSecond * 60.0f);
    }

    private void Update()
	{
        //Debug.Log(fade);

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

	public bool isFade()
	{
		return fade;
	}

    public bool isFadeIn()
    {
        return fadeIn;
    }
}
