using UnityEngine;
using System.Collections;

public class FunctionBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnclickStatus(){
		Statusxu._instance.TranslateStatus ();
	}
	public void OnclickBag(){
		Inventory._instance.TranslateStatus ();
	}
	public void OnclickEquip(){
		Equipmentxu._instance.Translate ();
	}
	public void OnclickSkill(){
		Skillxu._instance.TranslateStatus ();
	}
	public void OnclickSetting(){

	}
}
