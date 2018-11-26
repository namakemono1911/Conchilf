using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerReload : playerState {

    public playerReload(playerController p) : base(p) { }

    public override void initState()
    {

    }

    public override void updateState()
    {
        player.UI.bulletUI.bulletReload();
        player.Gun.reload();
        player.changeState(new playerDefault(player));
    }

    public override void hitBullet()
    {
        normalHit();
    }
}
