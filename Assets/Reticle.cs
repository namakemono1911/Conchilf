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
		reticlePos = calcPosReticle2();
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
	//	// それぞれのベクトルの角度を計算
	//	Vector2 vec1 = new Vector2(standVec.x, standVec.z);
	//	Vector2 vec2 = new Vector2(nowVec.x, nowVec.z);

	//	float dot = Vector2.Dot(vec1, vec2);
	//	float vecLen1 = vec1.magnitude;
	//	float vecLen2 = vec2.magnitude;

	//	float rot = Mathf.Acos(dot / (vecLen1 * vecLen2));

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

		x *= 10.0f;
		y *= 10.0f;
		// canvas分足す
		x += reticle.position.x;
		y += reticle.position.y;

		return new Vector2(x, y);
	}
}
