using UnityEngine;
using System.Collections;

public class FaceTextureUI : MonoBehaviour {

	public static FaceTextureUI _instance;
	private UILabel name;
	private UISlider HPbar;
	private UISlider MPbar;

	private UILabel hplabel;
	private UILabel mplabel;

	private PlayerStatus psxu;
	void Awake(){
		_instance = this;
		name = transform.Find ("Name").GetComponent<UILabel> ();
		HPbar = transform.Find ("HP").GetComponent<UISlider> ();
		MPbar = transform.Find ("MP").GetComponent<UISlider> ();
		hplabel = transform.Find ("HP/Thumb/Label").GetComponent<UILabel> ();
		mplabel = transform.Find ("MP/Thumb/Label").GetComponent<UILabel> ();
	}
	void Start(){
		psxu = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerStatus> ();
	
		Updateshow ();
	}

	 public void Updateshow(){
		name.text = "Lv." + psxu.level + " " + psxu.name;
		HPbar.value = psxu.hp_remain / psxu.HP;
		MPbar.value = psxu.mp_remain / psxu.MP;
		hplabel.text = psxu.hp_remain + "/" + psxu.HP;
		mplabel.text = psxu.mp_remain + "/" + psxu.MP;
	}
}
