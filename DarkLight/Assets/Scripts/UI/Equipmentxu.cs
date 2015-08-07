using UnityEngine;
using System.Collections;

public class Equipmentxu : MonoBehaviour {


	public static Equipmentxu _instance;
	private TweenPosition tween;
	private bool isShow=false;
	private PlayerStatus psxu;
	public int attack = 0;
	public int def = 0;
	public int speed = 0;


	public  GameObject head;
	public GameObject armor;
	public GameObject righthand;
	public GameObject lefthand;
	public GameObject shoe;
	public GameObject acessory;
	public GameObject equipmentitem;
	// Use this for initialization
	void Awake () {
		_instance = this;
		tween = this.GetComponent<TweenPosition> ();
	
	}
	void Start(){
		psxu = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerStatus> ();
	}
	public void Translate(){
		if (isShow == false) {
						tween.PlayForward ();
						isShow = true;
				} else {
			tween.PlayReverse();
			isShow=false;
		}
	} 
	public bool Dress(int id){
		ObjectInfo info = ObjectsInfo._instance.GetObjectInfo (id);
		if (info.type != ObjectType.Equip) {
			return false;		
		}
		if (psxu.heroType == HeroStyle.Magician) {
			if(	info.aplytype==AplyType.Swordman){
				return false;
			}
		}
		if (psxu.heroType == HeroStyle.Swordman) {
			if(info.aplytype==AplyType.Magician){
				return false;
			}		
		}
		GameObject parent = null;
		switch (info.dresstype) {
		case DressType.Accessory:
			parent=acessory;
			break;
		case DressType.Armor:
			parent=armor;
			break;
		case DressType.Headgear:
			parent=head;
			break;
		case DressType.LeftHand:
			parent=lefthand;
			break;
		case DressType.RightHand:
			parent=righthand;
			break;
		case DressType.Shoe:
			parent=shoe;
			break;

		}
		Equipmenitem item = parent.GetComponentInChildren<Equipmenitem> ();
		if (item != null) {//又重复,将当前装备放到装备栏
			Inventory._instance.GetId(item.id);
			item.setId (id);
				}
		else{//没重复
			//初始化
			GameObject itemGo=NGUITools.AddChild(parent,equipmentitem);
	        itemGo.transform.localPosition=Vector3.zero;
			itemGo.GetComponent<Equipmenitem>().setInfoxu(info);
		}
		UpdatePyroperty ();
		return true;
	}
	public void Takeoff(int id,GameObject go){
		Inventory._instance.GetId(id);
		GameObject.Destroy(go);
		UpdatePyroperty ();
	}
 void UpdatePyroperty(){
		this.attack = 0;
		this.def = 0;
		this.speed = 0;
		Equipmenitem headItems = head.GetComponentInChildren<Equipmenitem> ();
		PlusProperty (headItems);
		Equipmenitem shoeItems = shoe.GetComponentInChildren<Equipmenitem> ();
		PlusProperty (shoeItems);
		Equipmenitem righthandItems =righthand.GetComponentInChildren<Equipmenitem> ();
		PlusProperty (righthandItems);
		Equipmenitem lefthandItems = lefthand.GetComponentInChildren<Equipmenitem> ();
		PlusProperty (lefthandItems);
		Equipmenitem armorItems = armor.GetComponentInChildren<Equipmenitem> ();
		PlusProperty (armorItems);
		Equipmenitem acessoryItems = acessory.GetComponentInChildren<Equipmenitem> ();
		PlusProperty (acessoryItems);

	} 
	void PlusProperty(Equipmenitem item){
		if (item != null) {
			ObjectInfo equipInfo=ObjectsInfo._instance.GetObjectInfo(item.id);
			this.attack+=equipInfo.attack;
			this.def+=equipInfo.def;
			this.speed+=equipInfo.speed;

		}

	}
}
