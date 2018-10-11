using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerReload : playerState {

    private float startTime;

    public playerReload(playerController p) : base(p) { }

    public override void initState()
    {
        startTime = Time.time;
    }

    public override void updateState()
    {
        //リロードが終わったかどうか
        if (Time.time - startTime >= player.Gun.reloadTime)
        {
            player.UI.bulletUI.bulletReload();
            player.Gun.reload();
            player.changeState(new playerDefault(player));
        }
    }

    public override void hitBullet()
    {
        normalHit();
    }
}
