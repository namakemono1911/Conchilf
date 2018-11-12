using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class titleManager : MonoBehaviour
{
    [SerializeField]
    private Button mode1;

    [SerializeField]
    private Button mode2;

    [SerializeField]
    private InputFacade reticle;

    private Button selectButton = null;

    // Update is called once per frame
    void Update()
    {
        if (reticle.whetherShot() && selectButton != null)
            ExecuteEvents.Execute(selectButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }

    public void select1()
    {
        //遷移処理
        sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_GAME_NORMAL_1);
    }

    public void select2()
    {
        //遷移処理
        sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_GAME_NORMAL_2);
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
