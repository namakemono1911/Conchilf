//-----------------------------------------------------
//
//  Title   :   エネミー、銃処理
//  Auther  :   Shun Sakai
//  Date    :   2018/09/14   
//  Update  :   
//
//-----------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_gun : MonoBehaviour {

    // inspectar上に表示
    [SerializeField] private AudioSource    shotSE;     //　銃の発砲音
    [SerializeField] private Light          shotLight;  //　銃の光
    [SerializeField] private GameObject     shotEffect; //　弾が当たった時のパーティクル


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public AudioSource GetShotSE()
    {
        return shotSE;
    }

    public Light GetShotLight()
    {
        return shotLight;
    }

    public GameObject GetShotEffect()
    {
        return shotEffect;
    }

}
