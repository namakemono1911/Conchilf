using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class JoyConController : MonoBehaviour {


	[SerializeField]
	private MyJoycon joycons;

	private static readonly Joycon.Button[] m_buttons =
		Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

	private Joycon.Button? m_pressedButtonL;
	private Joycon.Button? m_pressedButtonR;

	private void Start()
	{
		if (!joycons.GetStateJoyConLR())
			return;
	}

	private void Update()
	{
		m_pressedButtonL = null;
		m_pressedButtonR = null;

		foreach (var button in m_buttons)
		{
			if (joycons.GetButtonL(button))
			{
				m_pressedButtonL = button;
			}
			if (joycons.GetButtonR(button))
			{
				m_pressedButtonR = button;
			}
		}

		if (Input.GetKeyDown(KeyCode.Z))
		{
			joycons.SetRumbleL(160, 320, 0.6f, 200);
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
			joycons.SetRumbleR(160, 320, 0.6f, 200);
		}
	}

	private void OnGUI()
	{
		var style = GUI.skin.GetStyle("label");
		style.fontSize = 24;

		if (!joycons.GetStateJoyConLR())
		{
			GUILayout.Label("Joy-Con が接続されていません");
			return;
		}

		if (!joycons.GetStateJoyConL())
		{
			GUILayout.Label("Joy-Con (L) が接続されていません");
			return;
		}

		if (!joycons.GetStateJoyConR())
		{
			GUILayout.Label("Joy-Con (R) が接続されていません");
			return;
		}

		GUILayout.BeginHorizontal(GUILayout.Width(960));


		var name = "Joy-Con (L)";
		var key = "Z キー";
		Joycon.Button button;
		float[] stick;
		Vector3 gyro;
		Vector3 accel;
		Quaternion orientation;

		stick = new float[2];
		gyro = new Vector3(0, 0, 0);
		accel = new Vector3(0, 0, 0);
		orientation = new Quaternion(0, 0, 0 , 0);

		GUILayout.BeginVertical(GUILayout.Width(480));
		GUILayout.Label(name);
		GUILayout.Label(key + "：振動");
		GUILayout.Label("押されているボタン：" + m_pressedButtonL);
		GUILayout.Label(string.Format("スティック：({0}, {1})", joycons.GetStickL(stick)[0] , joycons.GetStickL(stick)[1]));
		GUILayout.Label("ジャイロ：" + joycons.GetGyroL());
		GUILayout.Label("加速度：" + joycons.GetAccelL());
		GUILayout.Label("傾き：" + joycons.GetOrientationL());
		GUILayout.Label("傾きの前方：" + joycons.GetOrientationL().eulerAngles.normalized);

		Vector3 a = joycons.GetOrientationL().eulerAngles.normalized;
		Vector3 c = new Vector3(0, -1, 0);
		Quaternion b = Quaternion.Euler(c);
		GUILayout.Label("x -1：" + calcPosReticle(new Vector3(-1, 0, 0), a));

		GUILayout.EndVertical();

		name = "Joy-Con (R)";
		key = "X キー";

		GUILayout.BeginVertical(GUILayout.Width(480));
		GUILayout.Label(name);
		GUILayout.Label(key + "：振動");
		GUILayout.Label("押されているボタン：" + m_pressedButtonR);
		GUILayout.Label(string.Format("スティック：({0}, {1})", joycons.GetStickR()[0], joycons.GetStickR()[1]));
		GUILayout.Label("ジャイロ：" + joycons.GetGyroR());
		GUILayout.Label("加速度：" + joycons.GetAccelR());
		GUILayout.Label("傾き：" + joycons.GetOrientationR());
		GUILayout.EndVertical();

		GUILayout.EndHorizontal();
	}

	// 傾きで計算
	private float calcPosReticle(Vector3 standVec, Vector3 nowVec)
	{

		// y xz平面
		float rot;
		rot = Vector3.Angle(new Vector3(standVec.x, standVec.y, standVec.z), new Vector3(nowVec.x, nowVec.y, nowVec.z));
	//	rot = rot * Mathf.PI / 180.0f;

		return rot;
	}

}
