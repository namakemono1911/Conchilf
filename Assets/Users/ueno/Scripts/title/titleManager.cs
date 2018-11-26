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

    [System.Serializable]
    class TitleSE
    {
        public AudioSource play1;
        public AudioSource play2;
    }

    [SerializeField]
    private TitleSE se;

    private Button selectButton = null;     //選択されているボタン

	private bool once = false;              //一度だけ通る

	private void Start()
	{
		//スコア初期化
		for (int j = 0; j < 2; j++)
		{
			for (int i = 0; i < (int)scoreType.TYPE_MAX; i++)
			{
				PlayerPrefs.SetInt(((scoreType)i).ToString() + j.ToString(), 0);
			}
		}
	}

	// Update is called once per frame
	void Update()
    {
		if (sceneManager.Instance.isFade() || once)
			return;

        if (reticle.whetherShot() && selectButton != null)
            ExecuteEvents.Execute(selectButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }

    public void select1()
    {
        //SE再生
        se.play1.Play();
		once = true;

        //遷移処理
        sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_GAME_NORMAL_1);
    }

    public void select2()
    {
        //SE再生
        se.play2.Play();
		once = true;

		//遷移処理
		sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_GAME_NORMAL_2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "button")
        {
            selectButton = collision.gameObject.GetComponent<Button>();
            collision.gameObject.GetComponent<ChangeScaling>().startAnimation();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "button")
        {
            collision.gameObject.GetComponent<ChangeScaling>().endAnimation();
            selectButton = null;
        }
    }
}
