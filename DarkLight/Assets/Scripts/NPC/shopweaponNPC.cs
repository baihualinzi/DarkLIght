using UnityEngine;
using System.Collections;

public class shopweaponNPC : NPC {

	void OnMouseOver(){
		if (Input.GetMouseButtonDown(0)) {
			audio.Play();
			shopweapon._instance.TranlateStatus();
		}
}
}
