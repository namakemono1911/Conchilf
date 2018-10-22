using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class textAnimation
{
    public textAnimationTable table;
    public Text[] texts;

    public void init()
    {
        table.initAnimation();
    }

    public void animation()
    {
        table.textAnimation(texts);
    }
}
