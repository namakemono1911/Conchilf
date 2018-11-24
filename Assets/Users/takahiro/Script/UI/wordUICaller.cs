using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wordUICaller : MonoBehaviour {

	[SerializeField]
	private wordUIManager wordUIManager;

	[SerializeField]
	private int callNumber;

	[SerializeField]
	private bool Hakujin;

	[SerializeField][MultilineAttribute(2)]
	private string textData;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "event")
		{
			Debug.Log("call -> word");
			wordUIManager.startPlayWordUI(callNumber,Hakujin,textData);
		}
	}
}
