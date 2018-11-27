using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimultaneousPlayback : UiAnimationInterface
{
    [SerializeField]
    private UiAnimationInterface[] animations;

    public override void endAnimation()
    {
        foreach (var anime in animations)
            anime.endAnimation();
    }

    public override void startAnimation()
    {
        foreach (var anime in animations)
            anime.startAnimation();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
