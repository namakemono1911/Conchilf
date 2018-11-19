using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
	private float time;
	private float timeRimit;

	// Use this for initialization
	void Start () {
		time = 0;
	}
	
	// Update is called once per frame
	private void FixedUpdate()
	{
		time += Time.deltaTime;

		if(time > timeRimit)
		{
			Destroy(this.gameObject);
		}
	}

	// バレットステータス
	public void SetBulletStatus(Vector3 TargetPos, float sec)
    {
		timeRimit = sec;

		this.transform.DOMove(TargetPos, sec);
    }
}
