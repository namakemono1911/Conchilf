using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // バレットステータス
    public void SetBulletStatus(Vector3 TargetPos, float sec)
    {
        this.transform.DOMove(TargetPos, sec);
    }
}
