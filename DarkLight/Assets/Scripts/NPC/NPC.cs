using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//不是每一帧调用
	void OnMouseEnter(){
		CursorManager._instance.setNpcTalk ();
	}
	void OnMouseExit(){
		CursorManager._instance.setNormal ();
	}
}
