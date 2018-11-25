using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logoSceneManager : MonoBehaviour {

	[SerializeField]
	private AudioClip[] voise;

	private int index;
	// Use this for initialization
	void Start () {
		index = (Random.Range(0 , 100)) % 2;
		GetComponent<AudioSource>().PlayOneShot(voise[index]);
	}
	
	// Update is called once per frame
	void Update () {
		// 音の終わりを待つ
		if (!GetComponent<AudioSource>().isPlaying)
		{
			sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_TITLE);
		}
	}
}
