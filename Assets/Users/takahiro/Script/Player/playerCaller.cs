using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCaller : MonoBehaviour {

	[SerializeField]
	private bool toNeutral;

	[SerializeField]
	private playerController[] player;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "event")
		{
			for(int i = 0; i < player.Length; ++i)
			{
				player[i].toNeutral(toNeutral);
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "event")
		{
			for (int i = 0; i < player.Length; ++i)
			{
				player[i].toNeutral(toNeutral);
			}
		}
	}
}
