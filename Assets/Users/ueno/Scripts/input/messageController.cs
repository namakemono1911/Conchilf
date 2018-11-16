using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class messageController : MonoBehaviour {
    [SerializeField]
    private Button yes;

    [SerializeField]
    private Button no;

    [SerializeField]
    private InputFacade reticle;

    [SerializeField]
    private inputNameController inputName;

    [SerializeField]
    private GameObject messageBox;

    private Button selectButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (reticle.whetherShot())
            ExecuteEvents.Execute(selectButton.gameObject, new PointerEventData( EventSystem.current), ExecuteEvents.pointerClickHandler);
    }

    public void selectYes()
    {
        Debug.Log("YES");
    }

    public void selectNo()
    {
        inputName.enabled = true;
        messageBox.SetActive(false);
        enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "button")
            selectButton = collision.gameObject.GetComponent<Button>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "button")
            selectButton = null;
    }
}
