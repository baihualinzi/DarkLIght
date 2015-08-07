using UnityEngine;
using System.Collections;

public class ExpBar : MonoBehaviour {

	public static ExpBar _instance;
	private UISlider Expslider;


	void Awake(){
		_instance = this;
		Expslider = this.GetComponent<UISlider> ();
	}
	public void Setvalue(float value){
		Expslider.value = value;
	}
}
