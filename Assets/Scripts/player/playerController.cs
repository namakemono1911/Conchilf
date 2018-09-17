using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlSetting
{
    public KeyCode shotButton;         //発射ボタン
    public KeyCode reloadButton;       //リロードボタン
    public KeyCode guardButton;        //ガードボタン
    public float mouseSensitivity;     //感度
    public string axisNameX;           //レティクルの横の動き
    public string axisNameY;           //レティクルの縦の動き
    public GameObject reticle;         //レティクル情報
}

[System.Serializable]
public class GunSetting
{
    public int numBullet;           //装弾数
    public int remBullet;           //残弾数
    public float reloadTime;        //リロード時間

    public void shoot()
    {
        remBullet--;
    }

    public void reload()
    {
        remBullet = numBullet;
    }
}

public class playerController : MonoBehaviour {

    [SerializeField]
    private ControlSetting control;     //プレイヤーのコントロール

    [SerializeField]
    private GunSetting gun;             //銃の設定

    private playerState state;          //プレイヤーのステートパターン

    public ControlSetting Control       //コントロール取得
    {
        get { return control; }
    }

    public GunSetting Gun               //銃の情報取得
    {
        get { return gun; }
    }


	// Use this for initialization
	void Start () {
        changeState(new playerDefault(this));

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        //レティクル移動
        Vector3 reticlePos = Vector3.zero;
        reticlePos.x = Input.GetAxis(control.axisNameX) * control.mouseSensitivity;
        reticlePos.y = Input.GetAxis(control.axisNameY) * control.mouseSensitivity;
        control.reticle.transform.position += reticlePos;

        state.updateState();
	}

    public void changeState(playerState newState)
    {
        state = newState;
        state.initState();
    }
}
