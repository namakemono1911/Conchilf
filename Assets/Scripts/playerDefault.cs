using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (Input.GetKeyDown(player.Control.shotButton))
        {
            shootGun();
        }

        //ガード
        if (Input.GetKeyDown(player.Control.guardButton))
        {

        }

        //リロード
        if (Input.GetKeyDown(player.Control.reloadButton))
        {

        }
    }

    //射撃
    void shootGun()
    {
        //射撃処理
        player.Gun.shoot();

        //透視投影変換
        var screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, player.Control.reticle.transform.position);
        var pos = Vector3.zero;
        var canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();

        var ray = RectTransformUtility.ScreenPointToRay(Camera.main, screenPos);

        //当たり判定
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "enemy")
                Destroy(hit.transform.gameObject);
        }

        //デバッグ表示
        var line = GameObject.Find("debugLine").GetComponent<LineRenderer>();
        line.SetPosition(0, ray.origin);
        line.SetPosition(1, ray.direction * 100 + Camera.main.transform.position);
    }
}
