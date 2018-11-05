using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class playerState : MonoBehaviour {

    protected playerController player;

    //コンストラクタ
    public playerState(playerController p) { player = p; }

    //初期化
    abstract public void initState();

    //更新
    abstract public void updateState();

    //ヒット処理
    abstract public void hitBullet();

    //通常ヒット
    protected void normalHit()
    {
        player.changeState(new playerDown(player));
    }

    //防御時ヒット
    protected void guardHit()
    {
        //ガードエフェクトとか
    }
}
