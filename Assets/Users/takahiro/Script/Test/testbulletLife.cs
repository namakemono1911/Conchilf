using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbulletLife : MonoBehaviour
{

	[SerializeField]
	private bulletLifeUI plife;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			plife.addBulletLife(1);
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			plife.addBulletLife(-1);
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			plife.bulletReload();
		}
	}
}