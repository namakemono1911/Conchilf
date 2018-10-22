using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textAnimationController : MonoBehaviour
{
    public textAnimation[] animation;

    private int arrayNum = 0;

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
        foreach (var anime in animation)
            Destroy(anime.table);

        Destroy(this);
    }
}
