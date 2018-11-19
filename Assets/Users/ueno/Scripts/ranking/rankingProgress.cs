using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rankingProgress : MonoBehaviour {
    [SerializeField]
    private InputFacade input;

    [SerializeField]
    private float changeTime;

    private float startTime;

	// Use this for initialization
	void Start ()
    {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (changeTime >= Time.time - startTime || input.whetherShot())
            sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_TITLE);
	}
}
