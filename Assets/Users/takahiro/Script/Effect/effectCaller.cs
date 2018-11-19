using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectCaller : MonoBehaviour {

	[SerializeField]
	private int effectNumber;

	[SerializeField]
	private effectManager effectManager;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "player")
		{
			effectManager.playEffect(effectNumber, this.transform);
		}
	}
}
