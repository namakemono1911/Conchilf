///////////////////////////////////////////////
//
//  Title   : 子要素のコライダー当たり判定
//  Auther  : Shun Sakai 
//  Date    : 2018/11/19
//  Update  : 
//  Memo    : 
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildOnTrigger : MonoBehaviour {

    // 親要素
    boss colliderTriggerParent;

    // 初回処理
    void Start()
    {
        GameObject Parent = gameObject.transform.parent.parent.gameObject;
        colliderTriggerParent = Parent.GetComponent<boss>();
    }

    void OnCollisionEnter(Collision collision)
    {
        colliderTriggerParent.OnCollisionBoss(collision);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
