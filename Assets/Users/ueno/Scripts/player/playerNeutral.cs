using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerNeutral : playerState
{
    public playerNeutral(playerController p) : base(p) { }

    public override void hitBullet()
    {

    }

    public override void initState()
    {
        player.Control.reticleVisible(false);
        player.UI.bulletUI.gameObject.SetActive(false);
    }

    public override void updateState()
    {

    }
}
