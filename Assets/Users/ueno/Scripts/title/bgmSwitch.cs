using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmSwitch : MonoBehaviour {

    [SerializeField]
    private AudioSource Intro = null;
    [SerializeField]
    private AudioSource Loop = null;

    private bool LoopStart;

    // Use this for initialization
    void Start () {
        Intro.Play();
        Loop.Pause();

        LoopStart = false;
        Debug.Log("BGM Start");
    }
	
	// Update is called once per frame
	void Update () {
        if ((!Intro.isPlaying) && (!LoopStart))
        {
            Loop.Play();
            LoopStart = true;
            Debug.Log("BGM Switch");
        }
    }
}
