using UnityEngine;
using System.Collections;

public class InventoryDes : MonoBehaviour {


	public static InventoryDes _instance;
	private UILabel labelxu;
	private float timer=0.0f;
	// Use this for initialization
	void Awake () {
		_instance=this  ;
		labelxu = this.GetComponentInChildren<UILabel> ();
		this.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//每次一旦鼠标移过物品，就会调用显示函数，timer计时就会为0.1f，这时就会有显示面板，鼠标移走，就消失了
		if (this.gameObject.activeInHierarchy == true) {//在显示
			timer-=Time.deltaTime;//计时器倒计时
			if(timer<=0){
				this.gameObject.SetActive(false);
			}
		}
	}

	public void show(int id){
		this.gameObject.SetActive (true); 
		timer = 0.4f;//0.1f的话出现显示信息闪烁，时间太短
		transform.position=UICamera.currentCamera.ScreenToWorldPoint (Input.mousePosition);//世界坐标

		ObjectInfo info = ObjectsInfo._instance.GetObjectInfo (id);


		string des = "";
		switch (info.type) {
		case ObjectType.Drug:
			des=GetDrugDes(info);
			break;
		case ObjectType.Equip:
			des=GetEquipDes(info);
			break;
		}
		labelxu.text = des;

	}
	string GetDrugDes(ObjectInfo info){
		string str = "";
		str+="名称:"+info.name+"\n";
		str += "HP回复:" + info.hp + "\n";
		str += "MP回复:" + info.mp + "\n";
		str+="出售价:"+info.price_sell+"\n";
		str += "购买价:" + info.price_buy + "\n";
		return str;
	}
	string GetEquipDes(ObjectInfo info){
		string str = "";
		str+="名称:"+info.name+"\n";
		switch (info.dresstype) {
		case DressType.Armor:
			str+="穿戴类型:盔甲\n";
			break;
		case DressType.Headgear:
			str+="穿戴类型:头盔\n";
				break;
		case  DressType.Accessory:
			str+="穿戴类型:饰品\n";
			break;
		case DressType.RightHand:
			str+="穿戴类型:右手\n";
			break;
		case DressType.Shoe:
			str+="穿戴类型:鞋子\n";
			break;
		case DressType.LeftHand:
			str+="穿戴类型:左手\n";
			break;
		}
		switch (info.aplytype) {
		case AplyType.Common:
			str+="适用类型:通用\n";
			break;
		case AplyType.Magician:
			str+="适用类型:魔法师\n";
			break;
		case AplyType.Swordman:
			str+="适用类型:剑士\n";
			break;
		}
		str += "伤害值:" + info.attack + "\n";
		str+="防御值:"+info.def+"\n";
		str+="速度值:"+info.speed+"\n";
		str += "HP回复:" + info.hp + "\n";
		str += "MP回复:" + info.mp + "\n";
		str+="出售价:"+info.price_sell+"\n";
		str += "购买价:" + info.price_buy + "\n";
		return str;
	}
}
