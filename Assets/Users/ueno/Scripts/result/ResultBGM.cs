using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBGM : MonoBehaviour
{
    [SerializeField]
    private AudioSource b;

    public static AudioSource bgm = null;

    // Use this for initialization
    void Start ()
    {
        if (!ResultBGMManager.setResultBGM(this.gameObject))
        {
            Destroy(this.gameObject);
            return;
        }

        bgm = b;
        bgm.Play();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}
}
