using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : UiAnimationInterface
{
	[SerializeField]
	private GameObject image;

	public override void endAnimation()
	{
		image.SetActive(false);
	}

	public override void startAnimation()
	{
		image.SetActive(true);
	}

	// Use this for initialization
	void Start ()
	{
		image.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
