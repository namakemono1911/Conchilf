using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

[System.Serializable]
public class ControlSetting
{
    //デバッグキー
    public KeyCode shotButtonD;         //発射ボタン
    public KeyCode reloadButtonD;       //リロードボタン
    public KeyCode guardButtonD;        //ガードボタン
    public float mouseSensitivity;      //感度
    public string axisNameX;            //レティクルの横の動き
    public string axisNameY;            //レティクルの縦の動き

    //wiiリモコンボタン
    public WiiButtonCode shotButton;    //発射ボタン

    public RectTransform reticle;       //レティクル情報
    public RectTransform[] led = new RectTransform[2];
}

[System.Serializable]
public class GunSetting
{
    public int numBullet;           //装弾数
    public int remBullet;           //残弾数
    public float reloadTime;        //リロード時間
    public float sensitivity;       //感度

    public void shoot()
    {
        remBullet--;
    }

    public void reload()
    {
        remBullet = numBullet;
    }
}

//不本意なInputクラス
[System.Serializable]
public class WiiInput
{
    public int playerNum;
    private Wiimote wiimote;
    private bool[] isPress = new bool[11];
    private bool[] isPressBefore = new bool[11];
    private bool[] led = { false, false, false, false };

    public IRData Ir
    {
        get { return wiimote.Ir; }
    }

    public Vector2 getPointerPos()
    {
        float[] pos = wiimote.Ir.GetPointingPosition();
        return new Vector2(pos[0], pos[1]);
    }

    //初期化
    public void start()
    {
        for (int i = 0; i < 11; i++)
        {
            isPress[i] = false;
            isPressBefore[i] = false;
        }

        led[playerNum] = true;

        WiimoteManager.FindWiimotes();
        wiimote = WiimoteManager.Wiimotes[playerNum];

        wiimote.SendPlayerLED(led[0], led[1], led[2], led[3]);
        wiimote.SendDataReportMode(InputDataType.REPORT_EXT21);
    }

    //更新
    public void update()
    {
        //Wiiリモコン情報取得
        if (!WiimoteManager.HasWiimote())
            if (!WiimoteManager.FindWiimotes())
            {
                Debug.Log("リモコンがない");
                return;
            }

        int ret;
        do
        {
            ret = wiimote.ReadWiimoteData();
        } while (ret > 0);

        for (int i = 0; i < isPress.Length; i++)
        {
            isPressBefore[i] = isPress[i];
            isPress[i] = wiimote.Button.Data[i];
        }

        //ir更新
        wiimote.SetupIRCamera(IRDataType.BASIC);
    }

    //プレス取得
    public bool getPress(WiiButtonCode code)
    {
        return isPress[(int)code];
    }

    //トリガー取得
    public bool getTrigger(WiiButtonCode code)
    {
        if (isPressBefore[(int)code] == false)
            if (isPress[(int)code] == true)
                return true;

        return false;
    }

    //リリース取得
    public bool getRelease(WiiButtonCode code)
    {
        if (isPressBefore[(int)code] == true)
            if (isPress[(int)code] == false)
                return true;

        return false;
    }
}

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
    private ControlSetting control;     //プレイヤーのコントロール

    [SerializeField]
    private GunSetting gun;             //銃の設定

    [SerializeField]
    private WiiInput[] wiiInput;        //Wiiリモコンの情報

    [SerializeField]
    private PlayerUI ui;

	[SerializeField]
	private Transform[] hitPos;			//弾が飛んでく所

    private playerState state = null;   //プレイヤーのステートパターン

    public ControlSetting Control       //コントロール取得
    {
        get { return control; }
    }

    public GunSetting Gun               //銃の情報取得
    {
        get { return gun; }
    }

    public WiiInput[] Wii
    {
        get { return wiiInput; }
    }

    public PlayerUI UI
    {
        get { return ui; }
    }

	// Use this for initialization
	void Start () {
        changeState(new playerDefault(this));

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		if (ui.bulletUI != null)
			ui.bulletUI.setBulletLifeFirst(gun.numBullet);

        //Wiiリモコン初期化
        for (int i = 0; i < wiiInput.Length; i++)
            wiiInput[i].start();
    }
	
	// Update is called once per frame
	void Update () {
        //Wiiリモコン更新
        for (int i = 0; i < wiiInput.Length; i++)
            wiiInput[i].update();

        //ステート更新
        state.updateState();

		//レティクル移動
		Vector2 reticlePos = Vector2.zero;
		reticlePos.x = Input.GetAxis(control.axisNameX) * control.mouseSensitivity;
		reticlePos.y = Input.GetAxis(control.axisNameY) * control.mouseSensitivity;
		control.reticle.anchoredPosition += reticlePos;

		//float[] ir = wiiInput[(int)ControllerArm.right].Ir.GetPointingPosition();
        //var originPos = new Vector2(-Screen.width * 0.5f, -Screen.height * 0.5f);
        //control.reticle.anchoredPosition3D
        //    = new Vector2((ir[0] * Screen.width + originPos.x) * gun.sensitivity,
        //    (ir[1] * Screen.height + originPos.y) * gun.sensitivity);
    }

    public void changeState(playerState newState)
    {
        if (state != null)
            Destroy(state);

        state = newState;
        state.initState();
    }

    private void OnCollisionEnter(Collision collision)
    {
    	// デバッグ状態ではない時
		if(Debug_HP == false)
		{	
			// HPが0以下
			if(HP <= 0)
			{
				Destroy(collision.gameObject);
	
		        if (collision.gameObject.tag == "enemyBullet")
		        {
		            state.hitBullet();
		        }
		        Debug.Log("Player -> HP 0");
			} else {
				HP --;
				Debug.Log("Player -> HP " + HP);
			}
		}
    }

	public Vector3 getHitPos()
	{
		var random = Random.Range(0, hitPos.Length);
		return hitPos[random].position;
	}
}
