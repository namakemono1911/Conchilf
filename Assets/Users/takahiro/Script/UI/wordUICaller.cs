using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wordUICaller : MonoBehaviour {

	[SerializeField]
	private wordUIManager wordUIManager;
	[SerializeField]
	private int callNumber;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "player")
		{
			wordUIManager.startPlayWordUI(callNumber);
			Debug.Log("Trigger");
		}
	}
}
