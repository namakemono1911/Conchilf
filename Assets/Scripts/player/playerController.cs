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

    public RectTransform reticle;         //レティクル情報
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

//不本意なInputクラス
[System.Serializable]
public class WiiInput
{
    public int playerNum;
    private Wiimote wiimote;
    private bool[] isPress = new bool[11];
    private bool[] isPressBefore = new bool[11];

    public IRData Ir
    {
        get { return wiimote.Ir; }
    }

    //初期化
    public void start()
    {
        for (int i = 0; i < 11; i++)
        {
            isPress[i] = false;
            isPressBefore[i] = false;
        }

        WiimoteManager.FindWiimotes();
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

        wiimote = WiimoteManager.Wiimotes[playerNum];
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

public class playerController : MonoBehaviour {

    [SerializeField]
    private ControlSetting control;     //プレイヤーのコントロール

    [SerializeField]
    private GunSetting gun;             //銃の設定

    [SerializeField]
    private WiiInput wiiInput;          //Wiiリモコンの情報

    private playerState state;          //プレイヤーのステートパターン

    public ControlSetting Control       //コントロール取得
    {
        get { return control; }
    }

    public GunSetting Gun               //銃の情報取得
    {
        get { return gun; }
    }

    public WiiInput Wii
    {
        get { return wiiInput; }
    }


	// Use this for initialization
	void Start () {
        changeState(new playerDefault(this));

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        //Wiiリモコン初期化
        wiiInput.start();
    }
	
	// Update is called once per frame
	void Update () {
        //Wiiリモコン更新
        wiiInput.update();

        //ステート更新
        state.updateState();

        //レティクル移動
        //Vector2 reticlePos = Vector2.zero;
        //reticlePos.x = Input.GetAxis(control.axisNameX) * control.mouseSensitivity;
        //reticlePos.y = Input.GetAxis(control.axisNameY) * control.mouseSensitivity;
        //control.reticle.anchoredPosition += reticlePos;

        float[] ir = wiiInput.Ir.GetPointingPosition();
        var originPos = new Vector2(-Screen.width * 0.5f, -Screen.height * 0.5f);
        control.reticle.anchoredPosition3D = new Vector2((ir[0] * Screen.width + originPos.x) * 2, (ir[1] * Screen.height + originPos.y) * 2);
    }

    public void changeState(playerState newState)
    {
        state = newState;
        state.initState();
    }
}
