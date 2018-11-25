using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBGM : MonoBehaviour
{
    [SerializeField]
    private AudioSource b;

    public static AudioSource bgm;

    // Use this for initialization
    void Start ()
    {
        bgm = b;
        ResultBGMManager.setResultBGM(this.gameObject);
        bgm.Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
