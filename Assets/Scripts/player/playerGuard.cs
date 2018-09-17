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
        if (Input.GetKeyUp(player.Control.guardButton))
        {
            if (player.Gun.remBullet > 0)
                player.changeState(new playerDefault(player));
            else
                player.changeState(new playerNoAmmo(player));
        }
    }
}
