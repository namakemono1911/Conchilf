using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleVoice : MonoBehaviour
{
    [SerializeField]
    private AudioSource voice1;

    [SerializeField]
    private AudioSource voice2;

    private AudioSource voice;
    private bool onece = false;

	// Use this for initialization
	void Start ()
    {
        if (Random.Range(0, 100) % 2 == 0)
            voice = voice1;
        else
            voice = voice2;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!sceneManager.Instance.isFade() && !onece)
        {
            voice.Play();
            onece = true;
        }
	}
}
