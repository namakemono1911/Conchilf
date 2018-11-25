using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerNoAmmo : playerState {

    public playerNoAmmo(playerController p) : base(p) { }

    public override void initState()
    {
        //アニメーション再生
        player.Animation.reloadHint.startAnimation();
        //SE再生
        player.SE.noAmmoSE.Play();
    }

    public override void updateState()
    {
        //ガード
        if (player.Control.whetherGuard())
        {
            player.Animation.reloadHint.endAnimation();
            player.changeState(new playerGuard(player));
        }

        //リロード
        if (player.Control.whetherReload())
        {
            player.Animation.reloadHint.endAnimation();
            player.changeState(new playerReload(player));
        }
    }

    public override void hitBullet()
    {
        normalHit();
    }
}
