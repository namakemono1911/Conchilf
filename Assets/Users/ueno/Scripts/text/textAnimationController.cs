using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textAnimationController : MonoBehaviour
{
    public textAnimation[] animation;

    private int arrayNum = 0;

    private bool isAnimation = true;

    public bool IsAnimation
    {
        get { return isAnimation; }
    }

	// Use this for initialization
	void Start ()
    {
        animation[0].init();
	}
	
	// Update is called once per frame
	void Update ()
    {
        animation[arrayNum].animation();

        if (!animation[arrayNum].table.IsActiveAnimation)
        {
            if (arrayNum < animation.Length - 1)
                animation[++arrayNum].init();
            else
                uninit();
        }
	}

    private void uninit()
    {
        isAnimation = false;

        foreach (var anime in animation)
            Destroy(anime.table);
    }
}
