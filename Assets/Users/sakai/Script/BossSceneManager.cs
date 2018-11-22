///////////////////////////////////////////////
//
//  Title   : ボスシーンマネージャ
//  Auther  : Shun Sakai 
//  Date    : 2018/11/19
//  Update  : ボスシーン管理用
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ボスシーンマネージャ
public class BossSceneManager : MonoBehaviour {

    // シリアライズ
    [System.Serializable]
    class Option
    {
        public List< GameObject> EnemyList; 
    }

    // シリアリズメンバ
    [SerializeField]
    private Option OptionInfo;  // オプション情報

    // 通常メンバ


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // ボス全滅確認
        if (EnemyAllDeth())
        {
            GotoNextScene();
        }
        
	}



    // ボスが全滅したか確認
    private bool EnemyAllDeth()
    {
        for(int i = 0; i <  OptionInfo.EnemyList.Count; i++)
        {
            if( OptionInfo.EnemyList[i] != null)
            {
                return false;
            }
            
        }
        return true;
    }
    
    // シーン遷移
    private void GotoNextScene()
    {
        // シーンマネージャ
        sceneManager.Instance.SceneChange(sceneManager.SCENE.SCENE_RESULT);
    }

}
