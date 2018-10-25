using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{

	[SerializeField]
	private GameObject dangerUI1;
	[SerializeField]
	private GameObject dangerUI2;

	private GameObject parent;

	private float time;
	private float second;
	private bool visible;
	private int player;
	// Use this for initialization
	void Start () {
		// midolleを探す
	}
	
	// Update is called once per frame
	void Update () {

		if(visible)
		{
			time += Time.deltaTime;
			if (time >= second)
			{
				if(player != 0)
				{
					Destroy(dangerUI1);
					Destroy(dangerUI2);
				}
				Destroy(this.gameObject);
			}
		}

	}

    // バレットステータス
    public void SetBulletStatus(Vector3 TargetPos, float sec , int pl)
    {
		player = pl;
		// uiを作る
		if (player != 0)
		{
			parent = GameObject.Find("UICanvasMiddle");

			if (player == 1)
			{
				dangerUI1 = GameObject.Instantiate(dangerUI1, parent.transform);
				dangerUI1.GetComponent<dangerUI>().setEnemyBullet(this);
				float l = Vector3.Distance(this.transform.position, Camera.main.transform.position);
				dangerUI1.GetComponent<dangerUI>().setLength(l);
				dangerUI1.GetComponent<dangerUI>().setPlayerPos(Camera.main.transform.position);

			}
			else
			{
				dangerUI2 = GameObject.Instantiate(dangerUI2, parent.transform);
				dangerUI2.GetComponent<dangerUI>().setEnemyBullet(this);
				float l = Vector3.Distance(this.transform.position, Camera.main.transform.position);
				dangerUI2.GetComponent<dangerUI>().setLength(l);
				dangerUI1.GetComponent<dangerUI>().setPlayerPos(Camera.main.transform.position);
			}
		}
		visible = true;
		second = sec;

		this.transform.DOMove(TargetPos, sec);
    }

	public GameObject getDangerUI()
	{
		if(player != 0)
		{
			if(player == 1)
			{
				return dangerUI1;
			}
			else
			{
				return dangerUI2;
			}
		}

		return dangerUI1;
	}
}
