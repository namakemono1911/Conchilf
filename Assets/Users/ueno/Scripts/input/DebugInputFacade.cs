using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ボタン設定
[System.Serializable]
public class KeySetting
{
    public KeyCode shot;
    public KeyCode guard;
    public KeyCode reload;
    public KeyCode wakeUp;
}

public class DebugInputFacade : InputFacade {
    [SerializeField]
    private KeySetting setting;     //プレイヤーのコントロール

    [SerializeField]
    private float sensitivity;

    public override bool whetherGuard()
    {
        if (Input.GetKey(setting.guard))
            return true;

        return false;
    }

    public override bool whetherReload()
    {
        if (Input.GetKeyDown(setting.reload))
            return true;

        return false;
    }

    public override bool whetherShot()
    {
        if (Input.GetKeyDown(setting.shot))
            return true;

        return false;
    }

    //起き上がり判定
    public override bool whetherWakeUp()
    {
        if (Input.GetKeyDown(setting.wakeUp))
            return true;

        return false;
    }

    // Use this for initialization
    void Start ()
    {
        //マウス非表示＆固定
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (reticle == null)
            return;

        //レティクル移動
        Vector2 reticlePos = Vector2.zero;
        reticlePos.x = Input.GetAxis("Mouse X") * sensitivity;
        reticlePos.y = Input.GetAxis("Mouse Y") * sensitivity;
        reticle.anchoredPosition += reticlePos;
    }
}
