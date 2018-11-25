using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour {

	[System.Serializable]
	private enum YukaMaki
	{
		Forward,
		Back,
		Right,
		Left,
		Up,
		Down,
	}

	[SerializeField]
	private YukaMaki move;

	[SerializeField]
	private float moveSpeed;

	private Rigidbody rb;
	private int life;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		if (rb == null)
			Debug.Log ("toratoratoraにRigitbodyがついていません");

		switch (move)
		{
			case YukaMaki.Forward:
			{
				rb.AddForce(Vector3.forward * moveSpeed);
				break;
			}
			case YukaMaki.Back:
			{
				rb.AddForce(Vector3.back * moveSpeed);
				break;
			}
			case YukaMaki.Left:
			{
				rb.AddForce(Vector3.left * moveSpeed);
				break;
			}
			case YukaMaki.Right:
			{
				rb.AddForce(Vector3.right * moveSpeed);
				break;
			}
			case YukaMaki.Up:
			{
				rb.AddForce(Vector3.up * moveSpeed);
				break;
			}
			case YukaMaki.Down:
			{
				rb.AddForce(Vector3.down * moveSpeed);
				break;
			}
		}
	}
}
