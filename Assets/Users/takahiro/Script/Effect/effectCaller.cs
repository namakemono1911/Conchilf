using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectCaller : MonoBehaviour {

	[SerializeField]
	private effectManager effectManager;

	[SerializeField]
	private int effectNumber;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "event")
		{
			effectManager.playEffect(effectNumber);
		}
	}
}
