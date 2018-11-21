using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TitleSE
{
	public AudioSource shotSE;
}

public class titlePlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletMark;

    [SerializeField]
    private InputFacade input;

	[SerializeField]
	private TitleSE se;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (input.whetherShot())
        {
			//SE再生
			se.shotSE.Play();

            var rot = transform.rotation;
            rot.z += Random.value;
            GameObject.Instantiate(bulletMark, transform.position, rot, transform);
        }
	}
}
