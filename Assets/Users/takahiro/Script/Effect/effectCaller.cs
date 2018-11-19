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
		Debug.Log("hit" + collision.transform.tag);
		if(collision.transform.tag == "player")
		{
			Debug.Log("call");
			effectManager.playEffect(effectNumber);
		}
	}
}
