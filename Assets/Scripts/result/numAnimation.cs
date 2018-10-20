using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numAnimation : resultAnimation
{
    public numAnimation(resultController r) : base(r) { }

    public override void animation()
    {
        foreach (var pair in result.ResultPair)
            pair.numText.gameObject.SetActive(true);

        isAnime = false;
    }

    public override void skipAnimation()
    {
        foreach (var pair in result.ResultPair)
            pair.numText.gameObject.SetActive(true);

        isAnime = false;
    }
}
