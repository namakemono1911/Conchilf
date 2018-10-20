using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class resultAnimation
{
    public bool isAnime = true;
    protected resultController result;

    public resultAnimation(resultController r) { result = r; }

    abstract public void animation();
    public bool isAnimation() { return isAnime; }
    abstract public void skipAnimation();
}
