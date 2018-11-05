//-----------------------------------------------------
//
//  Title   :   エネミー、銃処理
//  Auther  :   Shun Sakai
//  Date    :   2018/09/14   
//  Update  :   
//
//-----------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_gun : MonoBehaviour {

    // inspector上に表示
    [System.Serializable]
    public class OptionValue
    {
       public GameObject bullet;
    }

    [SerializeField]
    private OptionValue value;

    public void SetBullet(Vector3 TargetPos, float Sec)
    {
        // 玉のインスタンス作成
        GameObject newBullet = Instantiate(value.bullet, this.gameObject.transform.position, Quaternion.identity);

        // 玉の初期ステータス設定
        Bullet script = newBullet.GetComponent<Bullet>();
        script.SetBulletStatus(TargetPos, Sec);

    }
}
