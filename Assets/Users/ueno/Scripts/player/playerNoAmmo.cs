using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerNoAmmo : playerState {

    public playerNoAmmo(playerController p) : base(p) { }

    public override void initState() { }

    public override void updateState()
    {
        //ガード
        if (player.Control.whetherGuard())
        {
            player.changeState(new playerGuard(player));
        }

        //リロード
        if (player.Control.whetherReload())
        {
            player.changeState(new playerReload(player));
        }
    }

    public override void hitBullet()
    {
        normalHit();
    }
}
