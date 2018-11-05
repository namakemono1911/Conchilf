using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShotDanger : MonoBehaviour
{

	[SerializeField]
	private Transform textureScale;
	[SerializeField]
	private float flashTime;    // 点滅間隔

	private bool isFlash;
	private bool isStart;
	private float time;

	// Use this for initialization
	void Start()
	{
		time = 0.0f;
		isStart = false;
		isFlash = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (isStart != true)
		{
			time = 0.0f;
			textureScale.localScale = new Vector3(0, 0, 0);
		}
		else
		{
			Debug.Log("aa");
			time += Time.deltaTime;
			Debug.Log(time);

			if (isFlash)
			{
				textureScale.localScale = new Vector3(1, 1, 1);
			}
			else
			{
				textureScale.localScale = new Vector3(0, 0, 0);
			}

			if (time >= flashTime)
			{
				isFlash = !isFlash;
				time = 0.0f;
			}
		}
	}

	public void flashStart()
	{
		isStart = true;
		isFlash = true;
		time = 0.0f;
	}

	public void flashEnd()
	{
		isStart = false;
		isFlash = true;
		time = 0.0f;
	}
}