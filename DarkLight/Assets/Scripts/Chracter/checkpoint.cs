using UnityEngine;
using System.Collections;

public class checkpoint : MonoBehaviour {


	public static checkpoint isActivePt;  //当前的复活点
	//public checkpoint firstPt;
	private PlayerStatus psxu;
	
	// Use this for initialization
	void Start () {
		isActivePt = this;
	psxu= GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerStatus> ();
	//	isActivePt = firstPt;
	}
	
//
		//if (isActivePt != this) {
	//		isActivePt = this;
		//}
	public void OnTriggerEnter(){
		psxu.GetDrug (psxu.HP,psxu.MP);

	}
}
