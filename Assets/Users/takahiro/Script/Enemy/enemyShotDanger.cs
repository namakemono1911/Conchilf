using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShotDanger : MonoBehaviour
{

	[SerializeField]
	private Transform textureScale;
	[SerializeField]
	private Transform texturebg;

	private float flashTime;    // 点滅間隔

	private bool isStart;
	private float time;

	// Use this for initialization
	void Start()
	{
		time = 0.0f;
	}

	// Update is called once per frame
	void Update()
	{
		if (isStart != true)
		{
			time = 0.0f;
			textureScale.localScale = new Vector3(0, 0, 0);
			texturebg.localScale = new Vector3(0, 0, 0);
		}
		else
		{
			texturebg.localScale = new Vector3(0.025f, 0.025f, 0.025f);

			time += Time.deltaTime;

			float a = time / flashTime;

			textureScale.localScale = new Vector3(a, a, a);

			if (time >= flashTime)
			{
				isStart = false;
			}
		}
	}

	public void flashStart()
	{
		isStart = true;
		time = 0.0f;
	}

	public void flashEnd()
	{
		isStart = false;
		time = 0.0f;
	}

	public  void setTime(float fTime)
	{
		flashTime = fTime;
	}
}