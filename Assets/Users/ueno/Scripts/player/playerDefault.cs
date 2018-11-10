﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WiimoteApi;

public class playerDefault : playerState {

    private Canvas canvas;

    public playerDefault(playerController p) : base(p) { }

	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
	}

    public override void initState()
    {

    }

    public override void updateState()
    {
        //射撃
        if (Input.GetKeyDown(player.Control.shotButtonD) || player.Wii[(int)ControllerArm.right].getTrigger(player.Control.shotButton))
        {
            shootGun();

            //弾切れ
            if (player.Gun.remBullet <= 0)
                player.changeState(new playerNoAmmo(player));
        }

        //ガード
        //float[,] ir = player.Wii[(int)ControllerArm.left].Ir.GetProbableSensorBarIR();
        //bool isVisible = true;
        //var originPos = new Vector2(-Screen.width * 0.5f, -Screen.height * 0.5f);

        //for (int i = 0; i < 2; i++)
        //{
        //    Vector2 pos = new Vector2(ir[i, 0] / 1023f, ir[i, 1] / 767f);
        //    player.Control.led[i].anchoredPosition = new Vector2((pos.x * Screen.width + originPos.x),
        //    (pos.y * Screen.height + originPos.y));

        //    if (pos.x <= 0.0f || pos.y <= 0.0f)
        //    {
        //        isVisible = false;
        //        break;
        //    }
        //}

        if (Input.GetKeyDown(player.Control.guardButtonD))
        {
            player.changeState(new playerGuard(player));
        }

        //リロード
        if (Input.GetKeyDown(player.Control.reloadButtonD))
        {
            player.changeState(new playerReload(player));
        }

        Debug.Log("state Default");
    }

    //射撃
    void shootGun()
    {
        //射撃処理
        player.Gun.shoot();
        player.UI.bulletUI.addBulletLife(-1);
        Instantiate(player.UI.bulletMark, player.Control.reticle);

        //透視投影変換
        var screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, player.Control.reticle.transform.position);
        var pos = Vector3.zero;
        var canvasRect = player.transform.parent.GetComponent<RectTransform>();

        var ray = RectTransformUtility.ScreenPointToRay(Camera.main, screenPos);

        //当たり判定
        //var hitObjects = Physics.RaycastAll(ray, 100.0f, LayerMask.NameToLayer("Player"));
        //RaycastHit fuck = hitObjects[0];
        //var distance = 1000.0f;
        //foreach (var hit in hitObjects)
        //{
        //    if (hit.distance < distance)
        //    {
        //        distance = hit.distance;
        //        fuck = hit;
        //    }
        //}
        //if (fuck.collider.tag == "enemy")
        //    fuck.transform.gameObject.GetComponent<enemy>().State.hitBullet(1, false);

        //if (fuck.collider.tag == "enemyCritical")
        //    fuck.transform.gameObject.GetComponent<enemy>().State.hitBullet(1, true);

        RaycastHit hit;
        bool isHit = false;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 8)))
        {
            if (hit.collider.tag == "enemy")
            {
                hit.transform.gameObject.GetComponent<enemy>().setPlayer(player);
                hit.transform.gameObject.GetComponent<enemy>().State.hitBullet(1, false);
                isHit = true;
            }

            if (hit.collider.tag == "enemyCritical")
            {
                hit.transform.gameObject.GetComponent<enemy>().setPlayer(player);
                hit.transform.gameObject.GetComponent<rightGun>().Enemy.State.hitBullet(1, true);
                isHit = true;
            }

            if (hit.collider.tag == "Boss")
            {
                hit.transform.gameObject.GetComponent<enemy>().setPlayer(player);
                hit.transform.gameObject.GetComponent<boss>().BulletHit();
                isHit = true;
            }
        }
        player.result.shot(isHit);

        //デバッグ表示
  //      var line = GameObject.Find("debugLine").GetComponent<LineRenderer>();
		//line.SetPosition(0, ray.origin);
		//line.SetPosition(1, hit.point);
	}

    //ヒット時処理
    public override void hitBullet()
    {
        normalHit();
    }
}
