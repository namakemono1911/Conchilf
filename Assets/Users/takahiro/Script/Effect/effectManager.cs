using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectManager : MonoBehaviour {

	[SerializeField]
	private GameObject[] effects;

	public void playEffect(int idx , Transform transform)
	{
		GameObject.Instantiate(effects[idx], transform);
	}
}
