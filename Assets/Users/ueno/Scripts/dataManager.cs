using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dataManager : MonoBehaviour {

	public struct Data
	{
		public bool missionComp1;   // 前回成功か
		public bool missionComp2;   // 前回成功か
		public bool mission1;		// ミッション1の合否
		public bool mission2;		// ミッション2の合否
		public int highScore;		// ハイスコア
	};

	public void SaveData(Data data , int stageNum)
	{
		// キー作成
		string key = "stage" + stageNum.ToString();
		int saveData = 0001111;   // ハイスコア 000 , ミッション1 : 0(true) or 1(false) , 前回ミッション1 : 0(true) or 1(false) , ミッション2 : 0(true) or 1(false) , 前回ミッション2 : 0(true) or 1(false)
		saveData = (data.highScore * 10000) + 1111;

		if(data.mission1 || data.missionComp1)
		{
			data.missionComp1 = true;
			saveData -= 1100;
		}
		if (data.mission2 || data.missionComp2)
		{
			data.missionComp2 = true;
			saveData -= 11;
		}

		PlayerPrefs.SetInt(key, saveData);

	}

	public Data LoadData(Data data, int stageNum)
	{
		// キー作成
		string key = "stage" + stageNum.ToString();

		// データ確認
		if(PlayerPrefs.HasKey(key) != true)
		{
			// 存在しない場合
			Data hoge;
			hoge.highScore = 000;
			hoge.mission1 = false;
			hoge.mission2 = false;
			hoge.missionComp1 = false;
			hoge.missionComp2 = false;

			SaveData(hoge , stageNum);

		}

		// データ取得
		int loadDatta = PlayerPrefs.GetInt(key);
		// 各桁の情報取り出し
		int[] digit = new int[7];
		int value = loadDatta;
		for(int i = 0; i < digit.Length; ++i)
		{
			int ten = 1;

			for(int n = digit.Length - i; n > 1;--n)
			{
				ten *= 10;
			}

			digit[i] = value / ten;
			value = value % ten;

		}

		// 情報組み立て
		int higeScore = digit[0] * 100 + digit[1] * 10 + digit[2];
		bool mission1 = false;
		bool missionComp1 = false;
		bool mission2 = false;
		bool missionComp2 = false;

		if (digit[5] == 0 || digit[6] == 0)
		{
			mission2 = true;
			missionComp2 = true;
		}
		if (digit[3] == 0 || digit[4] == 0)
		{
			mission1 = true;
			missionComp1 = true;
		}

		data.highScore = higeScore;
		data.mission1 = mission1;
		data.mission2 = mission2;
		data.missionComp1 = missionComp1;
		data.missionComp2 = missionComp2;

		return data;
	}

	public void ResetData(int stageNum)
	{
		// キー作成
		string key = "stage" + stageNum.ToString();

		PlayerPrefs.DeleteKey(key);

	}

	public void ResetDataAll()
	{
		PlayerPrefs.DeleteAll();
	}
}
