using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

//ボタン設定
[System.Serializable]
public class wiiSetting
{
    public WiiButtonCode shot;
    public WiiButtonCode reload;
}

//腕
public enum ControllerArm
{
    right = 0,
    left
}

//大本営
public class WiiInputFacade : InputFacade
{
    private WiiInput[] wiiInput = new WiiInput[2];          //Wiiリモコン

    [SerializeField]
    private wiiSetting setting;                             //Wiiリモコンの設定

    [SerializeField]
    private float sensitivity;                              //感度

    [SerializeField]
    private int shakeValue;                                 //リモコンを振る回数

    [SerializeField]
    private float shakePower;                               //振る力

    private int nowShake;                                   //振った回数

    // Use this for initialization
    void Start()
    {
        wiiInput[(int)ControllerArm.right] = new WiiInput(PlayerNum);
        wiiInput[(int)ControllerArm.left] = new WiiInput(PlayerNum + 1);
        foreach (var input in wiiInput)
            input.start();
    }

    // Update is called once per frame
    void Update()
    {
        //コントローラー情報更新
        foreach (var input in wiiInput)
            input.update();
        
        //レティクル移動
        if (reticle == null)
            return;

        float[] ir = wiiInput[(int)ControllerArm.right].Ir.GetPointingPosition();
        var originPos = new Vector2(-Screen.width * 0.5f, -Screen.height * 0.5f);
        reticle.anchoredPosition3D
            = new Vector2((ir[0] * Screen.width + originPos.x) * sensitivity,
            (ir[1] * Screen.height + originPos.y) * sensitivity);
    }

    //ガード
    public override bool whetherGuard()
    {
        float[,] ir = wiiInput[(int)ControllerArm.left].Ir.GetProbableSensorBarIR();
        var originPos = new Vector2(-Screen.width * 0.5f, -Screen.height * 0.5f);

        for (int i = 0; i < 2; i++)
        {
            Vector2 pos = new Vector2(ir[i, 0] / 1023f, ir[i, 1] / 767f);

            if (pos.x <= 0.0f || pos.y <= 0.0f)
                return false;
        }

        return true;
    }

    //射撃
    public override bool whetherShot()
    {
        if (wiiInput[(int)ControllerArm.right].getTrigger(setting.shot))
            return true;

        return false;
    }

    //リロード
    public override bool whetherReload()
    {
        if (whetherGuard())
            return true;

        if (wiiInput[(int)ControllerArm.right].getTrigger(setting.reload))
            return true;

        return false;
    }

    //起き上がったかどうか
    public override bool whetherWakeUp()
    {
        Debug.Log("pow : " + wiiInput[(int)ControllerArm.right].getAccelVector().magnitude.ToString());
        if (shakePower <= wiiInput[(int)ControllerArm.right].getAccelVector().magnitude)
            nowShake++;

        if (nowShake >= shakeValue)
        {
            nowShake = 0;
            return true;
        }

        return false;
    }
}
