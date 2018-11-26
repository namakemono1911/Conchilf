using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshRendererHide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MeshRenderer rend = GetComponent<MeshRenderer> ();

		if (rend == null)
			Debug.Log ("MeshRendererHideにRigitbodyがついていません");
		
		rend.enabled = false;
		Debug.LogWarning ("<color = red>MeshRendererHideが使用されています、本番環境では消してください</color>");
	}
}
