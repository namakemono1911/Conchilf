using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thankyouManager : MonoBehaviour {

	[SerializeField]
	private AudioClip[] voise;

	private AudioSource audio;
	private int index;
	// Use this for initialization
	void Start () {
		index = 0;
		audio = GetComponent<AudioSource>();
		audio.PlayOneShot(voise[(Random.Range(0, 100)) % 2]);
	}
	
	// Update is called once per frame
	void Update () {
		if(index == 0)
		{
			if(!audio.isPlaying)
			{
				index = 1;
				audio.PlayOneShot(voise[2]);
			}
		}
		else
		{
			if (!audio.isPlaying)
			{
				sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_LOGO);
			}
		}
	}
}
