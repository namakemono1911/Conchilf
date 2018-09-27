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
        float[,] ir = player.Wii[(int)ControllerArm.left].Ir.GetProbableSensorBarIR();
        bool isVisible = true;
        var originPos = new Vector2(-Screen.width * 0.5f, -Screen.height * 0.5f);

        for (int i = 0; i < 2; i++)
        {
            Vector2 pos = new Vector2(ir[i, 0] / 1023f, ir[i, 1] / 767f);
            player.Control.led[i].anchoredPosition = new Vector2((pos.x * Screen.width + originPos.x),
            (pos.y * Screen.height + originPos.y));

            if (pos.x <= 0.0f || pos.y <= 0.0f)
            {
                isVisible = false;
                break;
            }
        }

        if (Input.GetKeyUp(player.Control.guardButtonD) || !isVisible)
        {
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
