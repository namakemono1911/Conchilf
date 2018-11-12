using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGuard : playerState {

    public playerGuard(playerController p) : base(p) { }

    public override void initState()
    {

    }

    public override void updateState()
    {
        if (player.Control.whetherGuard())
        {
            //弾がなければNoAmmoに
            if (player.Gun.remBullet > 0)
                player.changeState(new playerDefault(player));
            else
                player.changeState(new playerNoAmmo(player));
        }
        Debug.Log("state Gurd");
    }

    //ヒット時処理
    public override void hitBullet()
    {
        guardHit();
    }
}
