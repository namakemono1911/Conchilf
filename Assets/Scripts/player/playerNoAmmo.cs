using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerNoAmmo : playerState {

    public playerNoAmmo(playerController p) : base(p) { }

    public override void initState() { }

    public override void updateState()
    {
        //ガード
        if (Input.GetKeyDown(player.Control.guardButton))
        {
            player.changeState(new playerGuard(player));
        }

        //リロード
        if (Input.GetKeyDown(player.Control.reloadButton))
        {
            player.changeState(new playerReload(player));
        }
    }
}
