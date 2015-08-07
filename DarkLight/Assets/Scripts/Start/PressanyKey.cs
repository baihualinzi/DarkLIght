using UnityEngine;
using System.Collections;

public class PressanyKey : MonoBehaviour {

	private bool isAnyKey=false;//任何键被按下
	public GameObject buttonContainer;
	// Use this for initialization
	void Start () {
	
		buttonContainer = this.transform.parent.Find ("buttonContainer").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	if (isAnyKey == false) {
		if(Input.anyKey)
			{
				showKey();
			}
		}
	}
	void showKey()
	{
		buttonContainer.SetActive (true);
		this.gameObject.SetActive (false);
		isAnyKey = true;
	}
}
