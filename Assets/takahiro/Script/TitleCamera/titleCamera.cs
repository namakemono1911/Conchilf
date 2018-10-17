using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class titleCamera : MonoBehaviour {

	[SerializeField]
	private float moveTime;
	[SerializeField]
	private titleCameraPos[] cameraPos;

	private int cameraIdx;
	private bool bMove;
	private float countTime;

	// Use this for initialization
	void Start () {
		Transform cameraTransform = cameraPos[0].getStartTransform();
		this.transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, cameraTransform.position.z);
		this.transform.rotation = new Quaternion(cameraTransform.rotation.x , cameraTransform.rotation.y , cameraTransform.rotation.z , cameraTransform.rotation.w);

		bMove = true;
		cameraIdx = 0;

		this.transform.DOMove(cameraPos[0].getEndTransform().position, moveTime);
		countTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(bMove)
		{
			// 座標間の移動
			countTime += Time.deltaTime;

			if(countTime >= moveTime)
			{
				bMove = false;
			}
		}
		else
		{
			countTime = 0;
			// 次の座標へ移動
			++cameraIdx;

			if(cameraIdx >= cameraPos.Length)
			{
				cameraIdx = 0;
			}

			Transform cameraTransform = cameraPos[cameraIdx].getStartTransform();
			this.transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, cameraTransform.position.z);
			this.transform.rotation = new Quaternion(cameraTransform.rotation.x, cameraTransform.rotation.y, cameraTransform.rotation.z, cameraTransform.rotation.w);

			bMove = true;

			this.transform.DOMove(cameraPos[cameraIdx].getEndTransform().position, moveTime);
		}
	}
}
