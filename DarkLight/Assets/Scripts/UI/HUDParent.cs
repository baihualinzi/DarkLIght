using UnityEngine;
using System.Collections;

public class HUDParent : MonoBehaviour {

	public static HUDParent _instance;

	void Awake(){
		_instance = this;
	}


}
