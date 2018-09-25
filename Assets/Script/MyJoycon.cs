using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MyJoycon : MonoBehaviour {


	// 回転する物体
	[SerializeField]
	private GameObject guardObj;
	// ガード物体
	[SerializeField]
	private JoyconGuard guardJoycon;

	private Quaternion OrientationR;
	private Quaternion OrientationL;


	private List<Joycon> m_joycons;
	private Joycon m_joyconL;
	private Joycon m_joyconR;

	// Use this for initialization
	void Start () {

		// 誤差対策
		OrientationR = new Quaternion(0, 0, 0, 1);
		OrientationL = new Quaternion(0, 0, 0, 1);



		m_joycons = JoyconManager.Instance.j;

		if (m_joycons == null || m_joycons.Count <= 0) return;

		m_joyconL = m_joycons.Find(c => c.isLeft);
		m_joyconR = m_joycons.Find(c => !c.isLeft);

	}

	// Update is called once per frame
	void Update () {

		if (m_joycons == null || m_joycons.Count <= 0) return;

		Quaternion a = GetOrientationL();
		guardObj.transform.rotation = new Quaternion(a.x,a.y,a.z,a.w);


	}

	// ボタンチェックR
	public bool GetButtonR(Joycon.Button b)
	{

		if (m_joycons == null || m_joycons.Count <= 0) return false;

		if (m_joyconR.GetButton(b))
		{
			return true;
		}

		return false;
	}

	// ボタンチェックL
	public bool GetButtonL(Joycon.Button b)
	{

		if (m_joycons == null || m_joycons.Count <= 0) return false;

		if (m_joyconL.GetButton(b))
		{
			return true;
		}

		return false;
	}

	// ジャイロR
	public Vector3 GetGyroR()
	{
		if (m_joycons == null || m_joycons.Count <= 0) return new Vector3(0, 0, 0);

		return m_joyconR.GetGyro();
	}

	// ジャイロL
	public Vector3 GetGyroL()
	{
		if (m_joycons == null || m_joycons.Count <= 0) return new Vector3(0, 0, 0);

		return m_joyconL.GetGyro();
	}

	// 傾きR
	public Quaternion GetOrientationR()
	{

		if (m_joycons == null || m_joycons.Count <= 0) return new Quaternion(0, 0, 0, 1);

		return m_joyconR.GetVector();
	}

	// 傾きL
	public Quaternion GetOrientationL()
	{

		if (m_joycons == null || m_joycons.Count <= 0) return new Quaternion(0, 0, 0, 1);

		return m_joyconL.GetVector();
	}

	// 加速度R
	public Vector3 GetAccelR()
	{
		if (m_joycons == null || m_joycons.Count <= 0) return new Vector3(0, 0, 0);

		return m_joyconR.GetAccel();
	}

	// 加速度L
	public Vector3 GetAccelL()
	{
		if (m_joycons == null || m_joycons.Count <= 0) return new Vector3(0, 0, 0);

		return m_joyconL.GetAccel();
	}

	// スティックR
	public float[] GetStickR()
	{
		if (m_joycons == null || m_joycons.Count <= 0) return new float[2];

		return m_joyconR.GetStick();
	}

	// スティックL
	public float[] GetStickL(float[] stick)
	{
		if (m_joycons == null || m_joycons.Count <= 0) return new float[2];

		return m_joyconL.GetStick();
	}

	// 振動R
	public void SetRumbleR(float low , float high , float amp , int time)
	{
		if (m_joycons == null || m_joycons.Count <= 0) return;

		m_joyconR.SetRumble(low, high, amp, time);

	}

	// 振動L
	public void SetRumbleL(float low, float high, float amp, int time)
	{
		if (m_joycons == null || m_joycons.Count <= 0) return;

		m_joyconL.SetRumble(low, high, amp, time);

	}

	// 接続確認R
	public bool GetStateJoyConR()
	{
		if (!m_joycons.Any(c => !c.isLeft)) return false;

		return true;
	}

	// 接続確認L
	public bool GetStateJoyConL()
	{
		if (!m_joycons.Any(c => c.isLeft)) return false;

		return true;
	}

	// 接続確認L:R
	public bool GetStateJoyConLR()
	{
		if (m_joycons == null || m_joycons.Count <= 0) return false;

		return true;
	}

	// ガードの確認
	public bool GetGuard()
	{
		return guardJoycon.GetGuard();
	}
}
