using UnityEngine;
using System.Collections;
//显示顺序先按照panel显示再按照depth
public class WeaponItem : MonoBehaviour {
	private int id;
	private ObjectInfo info;

	private UISprite iconxu;
	private UILabel name_label;
	private UILabel effect_label;
	private UILabel pricesell_label;

	void Awake(){
		iconxu = transform.Find ("icon").GetComponent<UISprite> ();
		name_label=transform.Find("nameDrug").GetComponent<UILabel>();
		effect_label=transform.Find("numEffect").GetComponent<UILabel>();
		pricesell_label=transform.Find("numPrice").GetComponent<UILabel>();
	}



	public void setId(int id){//根据id设置不同的显示
		this.id = id;
		info = ObjectsInfo._instance.GetObjectInfo (id);
		iconxu.spriteName = info.iconname;
		name_label.text = info.name;
		if (info.attack > 0) {
						effect_label.text = "+伤害" + info.attack;
				} else if (info.def > 0) {
						effect_label.text = "+防御" + info.def;
						
				} else if (info.speed>0) {
			effect_label.text = "+速度" + info.speed;
		}

		pricesell_label.text = info.price_sell.ToString ();

	}

	public void OnBuybtn(){
		shopweapon._instance.OnBuybtn (id);
	}
}
