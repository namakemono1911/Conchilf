using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wordUIManager : MonoBehaviour {

	[System.Serializable]
	public struct WORD_SPACE
	{
		public bool start;	// 開始判定
		public string word;	// 表示文字
		public AudioClip se;   // 効果音
		public Sprite sprite;    // 表示画像
	}

	[SerializeField]
	private Image image;
	[SerializeField]
	private Image wordBG;
	[SerializeField]
	private Text wordSpace;
	[SerializeField]
	private AudioSource audio;
	[SerializeField]
	private float fadeSpeedSecond;	// フェードする時間

	[SerializeField]
	private WORD_SPACE[] wordUIs;

	private int playNum;
	private bool fade;
	private bool fadeIn;
	private float fadeSpeed;
	private int playSound;
	private bool isSound;
	// Use this for initialization
	void Start () {
		playNum = 0;
		fadeSpeed = 1.0f / fadeSpeedSecond / 60.0f;
		playSound = 0;
		isSound = false;
		for (int i = 0; i < wordUIs.Length; ++i)
		{
			wordUIs[i].start = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(!fade)
		{
			if(!isSound)
			{
				// 全部0
				Color col1 = image.color;
				Color col2 = wordBG.color;
				Color col3 = wordSpace.color;

				image.color = new Color(col1.r, col1.g, col1.b, 0.0f);
				wordBG.color = new Color(col2.r, col2.g, col2.b, 0.0f);
				wordSpace.color = new Color(col3.r, col3.g, col3.b, 0.0f);

				return;
			}
			else
			{

				// 音の終わりを待つ
				if(!audio.isPlaying)
				{
					isSound = false;
					fade = true;
					fadeIn = false;
				}
			}
		}
		else
		{
			if(fadeIn)
			{
				// イン
				Color col1 = image.color;
				Color col2 = wordBG.color;
				Color col3 = wordSpace.color;

				col1.a += fadeSpeed;
				col2.a += fadeSpeed;
				col3.a += fadeSpeed;

				if (col1.a >= 1.0f)
				{
					col1.a = 1.0f;
				}
				if (col2.a >= 1.0f)
				{
					col2.a = 1.0f;
				}
				if (col3.a >= 1.0f)
				{
					col3.a = 1.0f;
				}

				image.color = new Color(col1.r, col1.g, col1.b, col1.a);
				wordBG.color = new Color(col2.r, col2.g, col2.b, col2.a);
				wordSpace.color = new Color(col3.r, col3.g, col3.b, col3.a);

				// 両方1.0f以上なら
				if(col1.a >= 1.0f && col2.a >= 1.0f && col3.a >= 1.0f)
				{
					// フェード終了 + 再生
					fade = false;
					fadeIn = false;
					playWordUI(playSound);
				}
			}
			else
			{
				// フェードアウト
				Color col1 = image.color;
				Color col2 = wordBG.color;
				Color col3 = wordSpace.color;

				col1.a -= fadeSpeed;
				col2.a -= fadeSpeed;
				col3.a -= fadeSpeed;

				if (col1.a <= 0.0f)
				{
					col1.a = 0.0f;
				}
				if (col2.a <= 0.0f)
				{
					col2.a = 0.0f;
				}
				if (col3.a <= 0.0f)
				{
					col3.a = 0.0f;
				}

				image.color = new Color(col1.r, col1.g, col1.b, col1.a);
				wordBG.color = new Color(col2.r, col2.g, col2.b, col2.a);
				wordSpace.color = new Color(col3.r, col3.g, col3.b, col3.a);

				// 両方0.0f以下なら
				if (col1.a <= 0.0f && col2.a <= 0.0f && col3.a <= 0.0f)
				{
					// フェード終了 + 再生
					fade = false;
					fadeIn = true;
				}
			}
		}
	}

	private void playWordUI(int playIndex)
	{
		audio.PlayOneShot(wordUIs[playIndex].se);
		isSound = true;
	}

	public void startPlayWordUI(int playIndex)
	{
		// スタート
		fade = true;
		fadeIn = true;
		playSound = playIndex;

		// 画像&言葉設定
		image.sprite = wordUIs[playIndex].sprite;
		wordSpace.text = wordUIs[playIndex].word;
	}

}
