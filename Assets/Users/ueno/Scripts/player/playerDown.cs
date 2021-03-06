﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDown : playerState {

    public playerDown(playerController p) : base(p) { }

    private bool isTop = false;

    public override void initState()
    {
		//SE再生
		player.SE.downSE.Play();

		//ヒントUI再生
		player.Animation.downHint.startAnimation();

		//スコア加算
		player.result.addScore(scoreType.DOWN_NUM);
    }

    public override void updateState()
    {
        if (player.Control.whetherWakeUp())
        {
            //SE再生
            player.SE.revivalSE.Play();

			//UI非表示
			player.Animation.downHint.endAnimation();

			if (player.Gun.remBullet >= 0)
				player.changeState(new playerReload(player));
        }
    }

    public override void hitBullet()
    {

    }
}
