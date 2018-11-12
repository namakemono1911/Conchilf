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
    [SerializeField]
    private WiiInput[] wiiInput;          //Wiiリモコン

    [SerializeField]
    private wiiSetting setting;         //Wiiリモコンの設定

    [SerializeField]
    private float sensitivity;
    

    // Use this for initialization
    void Start()
    {
        foreach (var input in wiiInput)
            input.start();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var input in wiiInput)
            input.update();

        //レティクル移動
        float[] ir = wiiInput[(int)ControllerArm.right].Ir.GetPointingPosition();
        var originPos = new Vector2(-Screen.width * 0.5f, -Screen.height * 0.5f);
        reticle.anchoredPosition3D
            = new Vector2((ir[0] * Screen.width + originPos.x) * sensitivity,
            (ir[1] * Screen.height + originPos.y) * sensitivity);
    }

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
        if (wiiInput[(int)ControllerArm.right].getTrigger(setting.reload))
            return true;

        return false;
    }
}
