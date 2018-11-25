using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WiimoteApi;

public class playerDefault : playerState {

    public playerDefault(playerController p) : base(p) { }
    
    public override void initState()
    {

    }

    public override void updateState()
    {
        //射撃
        if (player.Control.whetherShot())
        {
            shootGun();

            //弾切れ
            if (player.Gun.remBullet <= 0)
                player.changeState(new playerNoAmmo(player));
        }

        //ガード
        if (player.Control.whetherGuard())
            player.changeState(new playerGuard(player));

        //リロード
        if (player.Control.whetherReload())
            player.changeState(new playerReload(player));
    }

    //射撃
    void shootGun()
    {
		//SE再生
		player.SE.shotSE.Play();

        //射撃処理
        player.Gun.shoot();
        player.UI.bulletUI.addBulletLife(-1);
        Instantiate(player.UI.bulletMark, player.Control.Reticle);

        //透視投影変換
        var screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, player.Control.Reticle.transform.position);
        var pos = Vector3.zero;
        var canvasRect = player.transform.parent.GetComponent<RectTransform>();

        var ray = RectTransformUtility.ScreenPointToRay(Camera.main, screenPos);

        //当たり判定
        RaycastHit hit;
        player.result.addScore(scoreType.SHOT_NUM);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 8)))
        {
            if (hit.collider.tag == "enemy")
            {
                hit.transform.gameObject.GetComponent<enemy>().setPlayer(player);
                hit.transform.gameObject.GetComponent<enemy>().State.hitBullet(1, false);
                player.result.addScore(scoreType.HIT_NUM);
            }

            if (hit.collider.tag == "enemyCritical")
            {
                hit.transform.gameObject.GetComponent<rightGun>().Enemy.setPlayer(player);
                hit.transform.gameObject.GetComponent<rightGun>().Enemy.State.hitBullet(1, true);
                player.result.addScore(scoreType.HIT_NUM);
            }

            if (hit.collider.tag == "Boss")
            {
                
                hit.transform.gameObject.transform.root.GetComponent<boss>().setPlayer(player);
                hit.transform.gameObject.transform.root.GetComponent<boss>().BulletHit();
                player.result.addScore(scoreType.HIT_NUM);
            }
        }
	}

    //ヒット時処理
    public override void hitBullet()
    {
        normalHit();
    }
}
