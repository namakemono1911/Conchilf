using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titlePlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletMark;

    [SerializeField]
    private InputFacade input;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (input.whetherShot())
        {
            var rot = transform.rotation;
            rot.z += Random.value;
            GameObject.Instantiate(bulletMark, transform.position, rot, transform);
        }
	}
}
