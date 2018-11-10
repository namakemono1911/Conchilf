using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSceneManeger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_GAME_NORMAL);
        }

    }
}
