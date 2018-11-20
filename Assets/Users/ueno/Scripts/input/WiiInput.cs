using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

//不本意なInputクラス
[System.Serializable]
public class WiiInput
{
    private int playerNum;
    private Wiimote wiimote;
    private bool[] isPress = new bool[11];
    private bool[] isPressBefore = new bool[11];
    private bool[] led = { false, false, false, false };

    public WiiInput(int num)
    {
        playerNum = num;
    }

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
        {
            if (!WiimoteManager.FindWiimotes())
            {
                Debug.Log("リモコンがない");
                return;
            }
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

    //加速度取得
    public Vector3 getAccelVector()
    {
        float accel_x;
        float accel_y;
        float accel_z;

        float[] accel = wiimote.Accel.GetCalibratedAccelData();
        accel_x = accel[0];
        accel_y = -accel[2];
        accel_z = -accel[1];

        return new Vector3(accel_x, accel_y, accel_z);
    }
}
