using UnityEngine;
using System.Collections;

public class GameLoad : MonoBehaviour {
	public GameObject swordPre;
	public GameObject magicanPre;

	void Awake(){
	
	
		int selectindex = PlayerPrefs.GetInt ("SelectCharacterIndex");
		string name = PlayerPrefs.GetString ("name");
		GameObject go = null;
		if (selectindex == 0) {
						go = GameObject.Instantiate (swordPre) as GameObject;
				} 
		else if (selectindex == 1) {
						go = GameObject.Instantiate (magicanPre) as GameObject;
				}
		go.GetComponent<PlayerStatus> ().name = name;
	}

}
