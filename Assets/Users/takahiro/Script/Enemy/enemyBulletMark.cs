using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletMark : MonoBehaviour {

	[SerializeField]
	private GameObject enemyBulletMarkObj;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "enemyBullet")
		{
			GameObject.Instantiate(enemyBulletMarkObj, this.transform);
		}
	}
}
