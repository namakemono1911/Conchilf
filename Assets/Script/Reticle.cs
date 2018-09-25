using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour {

	[SerializeField]
	private Transform reticle;
	[SerializeField]
	private MyJoycon joycons;
	[SerializeField]
	private Vector2 screenSize;
	[SerializeField]
	private Vector2 rimitRot;
	[SerializeField]
	private Transform canvas;
	[SerializeField]
	private float GyroSpeed;
	[SerializeField]
	private float AccelSpeed;

	private Vector2 rimitRotOrig;
	private Quaternion retPos;  // joyconの傾き基準
	// Use this for initialization
	void Start () {

		screenSize.x = Screen.width;
		screenSize.y = Screen.height;

		retPos = new Quaternion(0, 0, 0 , 0);
		retPos = joycons.GetOrientationR();

		rimitRotOrig.x = rimitRot.x * Mathf.PI / 180.0f;
		rimitRotOrig.y = rimitRot.y * Mathf.PI / 180.0f;
	}
	
	// Update is called once per frame
	void Update () {

		//if (joycons.GetButtonR(Joycon.Button.DPAD_DOWN))
		//{
		//	retPos = joycons.GetOrientationR();
		//}


		Quaternion nowRot = joycons.GetOrientationR();

		////////////////////////////////////////
		// 差分計算
		Vector2 reticlePos;
		//reticlePos = calcPosReticle(retPos.eulerAngles.normalized, nowRot.eulerAngles.normalized);
		reticlePos = calcPosReticle3();
		////////////////////////////////////////

		//Debug.Log("基準 : " + retPos.ToString("f2") + "現在" + nowRot.ToString("f2"));

		if (joycons.GetButtonR(Joycon.Button.DPAD_DOWN))
		{
			reticlePos = new Vector2(screenSize.x / 2.0f, screenSize.y / 2.0f);
		}
		reticle.position = new Vector3(reticlePos.x, reticlePos.y, 0.0f);


	}

	// 傾きで計算
	//private Vector2 calcPosReticle(Vector3 standVec, Vector3 nowVec)
	//{

	//	// y xz平面
	//	float rot;
	//	rot = Vector3.Angle(new Vector3(standVec.x, 0, standVec.z), new Vector3(nowVec.x, 0, nowVec.z));
	//	rot = rot * Mathf.PI / 180.0f;

	//	if (nowVec.x - standVec.x < 0)
	//	{
	//		rot *= -1.0f;
	//	}

	//	Debug.Log("x : " + rot.ToString("f2"));

	//	float x = (screenSize.x / 200.0f) * (rot / 0.175f * 100.0f);
	//	float y = 0;

	//	// canvas分足す
	//	x += screenSize.x / 2.0f;
	//	y += screenSize.y / 2.0f;

	//	return new Vector2(x, y);
	//}

	// ジャイロで計算

	private Vector2 calcPosReticle2()
	{
		float x = joycons.GetGyroR().z;
		float y = joycons.GetGyroR().y;

		if (x >= 0.1f || x <= -0.1f)
		{
			x *= 10.0f;
		}
		else
		{
			x = 0;
		}

		if (y >= 0.1f || y <= -0.1f)
		{
			y *= 10.0f;
		}
		else
		{
			y = 0;
		}

		// canvas分足す
		x += reticle.position.x;
		y += reticle.position.y;

		return new Vector2(x, y);
	}

	// ジャイロ + 加速度
	private Vector2 calcPosReticle3()
	{

		float x = joycons.GetGyroR().z;
		float y = joycons.GetGyroR().y;

		if (x >= 0.1f || x <= -0.1f)
		{
			x *= GyroSpeed;
		}
		else
		{
			x = 0;
		}

		if (y >= 0.1f || y <= -0.1f)
		{
			y *= GyroSpeed;
		}
		else
		{
			y = 0;
		}

		// 加速度
		float xx = joycons.GetAccelR().x;
		float yy = joycons.GetAccelR().y;

		if (xx >= 0.2f || xx <= -0.2f)
		{
			xx *= AccelSpeed;
		}
		else
		{
			xx = 0;
		}

		if (yy >= 0.2f || yy <= -0.2f)
		{
			yy *= AccelSpeed;
		}
		else
		{
			yy = 0;
		}

		// canvas分足す
		x += reticle.position.x + xx;
		y += reticle.position.y + yy;

		return new Vector2(x, y);
	}

}
