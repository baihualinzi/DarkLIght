using UnityEngine;
using System.Collections;

public class skillItem : MonoBehaviour {

	public int id;
	private SkillInfo info;
	private UISprite icon_name;
	private UILabel name_label;
	private UILabel applyType_label;
	private UILabel des_label;
	private UILabel mp_label;

	private GameObject icon_mask;

	public void setId(int id ){
		InitProperty ();
		this.id = id;
	    info = SkillsInfo._instance.GetSkillInfoById(id);
	    icon_name.spriteName=info.icon_name;
		name_label.text=info.name;
		switch (info.applytype) {
		case ApplyType.Passive:
			applyType_label.text="增益";
			break;
		case ApplyType.Buff:
			applyType_label.text="增强";
			break;
		case ApplyType.SingleTarget:
			applyType_label.text="单目标";
			break;
		case ApplyType.MultiTarget:
			applyType_label.text="群体";
			break;

		}
		des_label.text = info.Des;
		mp_label.text = info.mp + "MP";
	}

 void InitProperty(){
		icon_name = transform.Find ("icon_name").GetComponent<UISprite> ();
		name_label = transform.Find ("bg_property/name_bg/name").GetComponent<UILabel> ();
		applyType_label = transform.Find ("bg_property/applyType/name").GetComponent<UILabel> ();
		des_label = transform.Find ("bg_property/des_bg/name").GetComponent<UILabel> ();
		mp_label = transform.Find ("bg_property/Mp_bg/mp").GetComponent<UILabel> ();
		icon_mask = transform.Find ("icon_mask").gameObject;
		icon_mask.SetActive (false);
	}
	public void UpdateShowItem(int level){
		if (info.level <= level) {//技能可用
			icon_mask.SetActive(false);
			icon_name.GetComponent<skillItemicon>().enabled=true;//可以拖拽
				} 
		else {
			icon_mask.SetActive(true);
			icon_name.GetComponent<skillItemicon>().enabled=false;
				
		}
	}

}
