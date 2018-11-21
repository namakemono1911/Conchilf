using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashing : UiAnimationInterface {
    [SerializeField]
    private Image image;

    [SerializeField]
    private float flashAlpha;

    [SerializeField]
    private float displayTime;

    [SerializeField]
    private float flashTime;

    private float startTime;

    private bool wheterFlash = false;

    private bool isDisplay = true;


    public override void endAnimation()
    {
        wheterFlash = false;
        image.color = new Color(1, 1, 1, flashAlpha);
    }

    public override void startAnimation()
    {
        wheterFlash = true;
        isDisplay = true;
        startTime = Time.time;
        image.color = new Color(1, 1, 1, 1);
    }

    private void Start()
    {
        image.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update ()
    {
        if (!wheterFlash)
            return;

        if (isDisplay)
            displayTimeCount();
        else
            flashTimeCount();
	}

    private void displayTimeCount()
    {
        if (displayTime <= Time.time - startTime)
        {
            startTime = Time.time;
            image.color = new Color(1, 1, 1, flashAlpha);
            isDisplay = false;
        }
    }

    private void flashTimeCount()
    {
        if (flashTime <= Time.time - startTime)
        {
            startTime = Time.time;
            image.color = new Color(1, 1, 1, 1);
            isDisplay = true;
        }
    }
}
