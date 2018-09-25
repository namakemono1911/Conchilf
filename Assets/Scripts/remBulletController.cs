using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class remBulletController : MonoBehaviour {

    [SerializeField]
    playerController player;

    private Text targetText;

	// Use this for initialization
	void Start () {
        targetText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        targetText.text = player.Gun.remBullet.ToString();
	}
}
