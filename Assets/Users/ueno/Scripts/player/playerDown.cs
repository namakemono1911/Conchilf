using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDown : playerState {

    public playerDown(playerController p) : base(p) { }

    private bool isTop = false;

    public override void initState()
    {
		//SE再生
		player.SE.downSE.Play();

        //スコア加算
        player.score.Score.addCount(ScoreCount.DOWN_CNT);
    }

    public override void updateState()
    {
        if (player.Control.whetherWakeUp())
        {
            if (player.Gun.remBullet >= 0)
                player.changeState(new playerDefault(player));
            else
                player.changeState(new playerNoAmmo(player));
        }
    }

    public override void hitBullet()
    {

    }
}
