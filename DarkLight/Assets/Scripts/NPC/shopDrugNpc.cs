using UnityEngine;
using System.Collections;

public class shopDrugNpc : NPC {


	void OnMouseOver(){
		if (Input.GetMouseButtonDown(0)) {
			audio.Play();
			ShopDrug._instance.Translante();
		}
}
}
