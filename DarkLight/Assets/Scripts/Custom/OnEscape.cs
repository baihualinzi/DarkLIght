using UnityEngine;
using System.Collections;

public class OnEscape : MonoBehaviour {
	private bool buttonClicked =false;
	void OnGUI() { 
		if (Input.GetKeyDown (KeyCode.Escape)) {
			buttonClicked = true; 
		}
		if (buttonClicked == true) {
			
			if (GUI.Button( new Rect (530,150, 100, 50), "ESCAPE")){
				Application.Quit ();
				//Debug.Log("Clicked the button with an image");
			}
		}
}
}