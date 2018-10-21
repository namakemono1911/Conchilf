using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class uiAnimation
{
    public enum AnimationType
    {
        waitAnime = 0,
        numAnime,
        scoreAnime,
    };

    public AnimationType type;
}
