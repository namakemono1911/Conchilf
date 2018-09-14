using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlSetting
{
    public KeyCode shotButton;         //発射ボタン
    public KeyCode reloadButton;       //リロードボタン
    public float mouseSensitivity;     //感度
    public string axisNameX;           //レティクルの横の動き
    public string axisNameY;           //レティクルの縦の動き
    public RectTransform reticle;      //レティクル情報
}

public class playerController : MonoBehaviour {

    [SerializeField]
    private ControlSetting control;     //プレイヤーのコントロール

    private playerState state;          //プレイヤーのステートパターン

    public ControlSetting Control       //コントロール取得
    {
        get { return control; }
    }


	// Use this for initialization
	void Start () {
        changeState(new playerDefault(this));

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        state.updateState();
	}

    public void changeState(playerState newState)
    {
        state = newState;
        state.initState();
    }
}
