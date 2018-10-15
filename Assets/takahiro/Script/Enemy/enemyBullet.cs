using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
	private int numBullet;
	private int maxBullet;

	public void setMaxBullet(int n)
	{
		maxBullet = n;
	}

	public bool isBullet()
	{
		if (numBullet <= 0)
		{
			return false;
		}

		return true;
	}

	public void shotBullet()
	{
		numBullet -= 1;
	}

	public void reloadBullet()
	{
		numBullet = maxBullet;
	}
}