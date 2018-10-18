using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class resultPair
{
    public string resultName;
    public Text numText;
    public Text scoreText;
}

public class resultController : MonoBehaviour
{
    [SerializeField]
    private resultPair[] pair;

    private Dictionary<string, Text> dict;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
