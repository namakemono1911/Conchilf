using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitAnimation : resultAnimation
{
    private float startTime;
    private float wait;

    public waitAnimation(resultController r ,float waitTime) : base(r)
    {
        startTime = Time.time;
        wait = waitTime;
    }

    public override void animation()
    {
        if (Time.time - startTime >= wait)
            isAnime = false;
    }

    public override void skipAnimation()
    {
        isAnime = false;
    }
}
