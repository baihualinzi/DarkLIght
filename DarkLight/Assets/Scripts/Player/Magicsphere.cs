using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Magicsphere : MonoBehaviour {

	public float attack=0;
	private List<Wolfbaby> wolfattacked=new List<Wolfbaby>();//已经受过伤害得狼群

	public void OnTriggerEnter(Collider col){
		if (col.tag == Tags.enemy) {
			Wolfbaby baby=col.GetComponent<Wolfbaby>();
			int index=wolfattacked.IndexOf(baby);
			if(index==-1){
				baby.Takedamage((int)attack);
				wolfattacked.Add(baby);
			}
		}
	}


}
