using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

//銃の設定
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

//アニメーション
[System.Serializable]
public class PlayerAnimation
{
    public UiAnimationInterface guard;      //ガードエフェクト
    public UiAnimationInterface reloadHint; //リロードヒント
	public UiAnimationInterface downHint;   //ダウンヒント
}

//UI
[System.Serializable]
public class PlayerUI
{
    public bulletLifeUI bulletUI = null;      //弾のUI
    public GameObject bulletMark = null;      //弾のエフェクト
}

public class playerController : MonoBehaviour {

	[SerializeField]
	public int HP = 3;					//HP
	
	[SerializeField]
	public bool Debug_HP = false;		//HPデバッグモード

    [SerializeField]
    private InputFacade control;        //プレイヤーのコントロール

    [SerializeField]
    private GunSetting gun;             //銃の設定

    [SerializeField]
    private PlayerUI ui;

	[SerializeField]
	private Transform[] hitPos;			//弾が飛んでく所

    [SerializeField]
    private PlayerAnimation animation;  //プレイヤーのエフェクト集

	[SerializeField]
	private PlayerSE se;				//SE

    private playerState state = null;   //プレイヤーのステートパターン

    public playerScore result;          //リザルト

    public InputFacade Control          //コントロール取得
    {
        get { return control; }
    }

    public GunSetting Gun               //銃の情報取得
    {
        get { return gun; }
    }

    public PlayerUI UI
    {
        get { return ui; }
    }

    public PlayerAnimation Animation
    {
        get { return animation; }
    }

	public PlayerSE SE
	{
		get { return se; }
	}

	// Use this for initialization
	void Start () {
        //リザルト初期化
        if (control.PlayerNum == 0)
            result = new playerScore(1);
        else
            result = new playerScore(2);

		changeState(new playerNeutral(this));
		loadScore();

		if (ui.bulletUI != null)
			ui.bulletUI.setBulletLifeFirst(gun.numBullet);
    }
	
	// Update is called once per frame
	void Update () {
        //ステート更新
        state.updateState();

        //デバッグ用セーブ機能
        if (Input.GetKey(KeyCode.S))
            result.save();
    }

    public void changeState(playerState newState)
    {
        Debug.Log("newState : " + newState);
        if (state != null)
            Destroy(state);

        state = newState;
        state.initState();
    }

    private void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.tag == "enemyBullet")
			state.hitBullet();
    }

	public Vector3 getHitPos()
	{
		var random = Random.Range(0, hitPos.Length);
		return hitPos[random].position;
	}

    public void saveScore()
    {
        result.save();
    }

    public void loadScore()
    {
        result.load();
    }

    public void toNeutral(bool isNeutral)
    {
        if (isNeutral)
            changeState(new playerNeutral(this));
        else
        {
            control.reticleVisible(true);
            ui.bulletUI.gameObject.SetActive(true);
            changeState(new playerDefault(this));
        }
    }
}
