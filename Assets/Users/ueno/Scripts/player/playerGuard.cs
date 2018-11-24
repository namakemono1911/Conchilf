using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGuard : playerState {

    public playerGuard(playerController p) : base(p) { }

    public override void initState()
    {
        Debug.Log("playerState : Guard");
        
		//ガードアニメーション開始
        player.Animation.guard.startAnimation();

		//SE再生
		player.SE.guardSE.Play();

        player.UI.bulletUI.bulletReload();
        player.Gun.reload();
        //player.changeState(new playerDefault(player));
    }

    public override void updateState()
    {
        if (!player.Control.whetherGuard())
        {
            //弾がなければNoAmmoに
            if (player.Gun.remBullet > 0)
                player.changeState(new playerDefault(player));
            else
                player.changeState(new playerNoAmmo(player));

            //ガードアニメーション終了
            player.Animation.guard.endAnimation();
        }
        Debug.Log("state Gurd");
    }

    //ヒット時処理
    public override void hitBullet()
    {
		//SE再生
		player.SE.guardHitSE.Play();

        guardHit();
    }
}
