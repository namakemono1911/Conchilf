using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logoSceneManager : MonoBehaviour {

	[SerializeField]
	private AudioClip[] voise;

	private int index;
	// Use this for initialization
	void Start () {
		index = -1;
	}
	
	// Update is called once per frame
	void Update () {
		// fadeの終わりを組む
		if(sceneManager.Instance.isFade())
		{
			return;
		}

		if(!sceneManager.Instance.isFade() && index == -1)
		{
			index = (Random.Range(0, 100)) % 2;
			GetComponent<AudioSource>().PlayOneShot(voise[index]);
			return;
		}

		// 音の終わりを待つ
		if (!GetComponent<AudioSource>().isPlaying)
		{
			sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_TITLE);
		}
	}
}
