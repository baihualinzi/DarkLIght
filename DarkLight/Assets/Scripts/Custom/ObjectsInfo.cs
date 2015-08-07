using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectsInfo : MonoBehaviour {
	//Awake用于在游戏开始之前初始化变量或游戏状态。在脚本整个生命周期内它仅被调用一次.Awake在所有对象被初始化之后调用
	//Awake在MonoBehavior创建后就立刻调用，Start将在MonoBehavior创建后在该帧Update之前，
	//在该Monobehavior.enabled == true的情况下执行。
	// Use this for initialization
	//物品信息管理系统，单例模式
	public static ObjectsInfo _instance;
	public TextAsset ObjectListInfo;
	private Dictionary<int,ObjectInfo> objectInfoDict=new Dictionary<int ,ObjectInfo>();


	//外面调用这个字典的方法
	public ObjectInfo GetObjectInfo(int id){
		ObjectInfo info = null;
		objectInfoDict.TryGetValue (id,out info);
		return info;
	}

	void Awake()
	{
		_instance = this;
		ReadInfo ();
	//	print(objectInfoDict.Keys.Count);
	}
	
	void ReadInfo(){
		string text = ObjectListInfo.text;
		string[] strArray = text.Split ('\n');
		foreach (string str in strArray) {
			string[] proArray=str.Split(',');

			ObjectInfo info=new ObjectInfo();//物品信息类存储，将其存储到字典中,根据键值调用，键就是id
			int id=int.Parse(proArray[0]);
			string name=proArray[1];
			string icon_name=proArray[2];
			string str_type=proArray[3];
			ObjectType type=ObjectType.Drug;
			switch(str_type){
			case "Drug":
				type=ObjectType.Drug;
				break;
			case "Mat":
				type=ObjectType.Mat;
				break;
			case "Equip":
				type=ObjectType.Equip;
				break;
			}
			info.id=id;info.name=name;info.iconname=icon_name;
			info.type=type;
			if(type==ObjectType.Drug){
				int hp=int.Parse(proArray[4]);
				int mp=int.Parse(proArray[5]);
				int price_sell=int.Parse(proArray[6]);
				int price_buy=int.Parse(proArray[7]);
				info.hp=hp;
				info.mp=mp;
				info.price_sell=price_sell;
				info.price_buy=price_buy;
			}else if(type==ObjectType.Equip){
				info.attack=int.Parse(proArray[4]);
				info.def=int.Parse(proArray[5]);
				info.speed=int.Parse(proArray[6]);
				info.price_buy=int.Parse(proArray[10]);
				info.price_sell=int.Parse(proArray[9]);
				string  strdresstype=proArray[7];
				switch(strdresstype){
				case  "Armor":
					info.dresstype=DressType.Armor;
					break;
				case "Headgear":
					info.dresstype=DressType.Headgear;
					break;
				case "Accessory":
					info.dresstype=DressType.Accessory;
					break;
				case "RightHand":
					info.dresstype=DressType.RightHand;
					break;
				case "LeftHand":
					info.dresstype=DressType.LeftHand;
					break;
				case "Shoe":
					info.dresstype=DressType.Shoe;
					break;

				}
				string strAplytype=proArray[8];
				switch(strAplytype){
				case "Swordman":
					info.aplytype=AplyType.Swordman;
					break;
				case"Magician":
					info.aplytype=AplyType.Magician;
					break;
				case "Common":
					info.aplytype=AplyType.Common;
					break;
				}


			}

			objectInfoDict.Add(id,info);//根据ID查找
		}
	}


}
public enum ObjectType{
	Drug,
	Mat,
	Equip
}
public enum DressType{
	Armor,
	Headgear,
	Accessory,
	RightHand,
	LeftHand,
	Shoe
}
public enum AplyType{
	Swordman,
	Magician,
	Common
}
public class ObjectInfo{
	public int id;
	public string name;
	public string iconname;
	public ObjectType type;
	public int hp;
	public int mp;
	public int price_sell;
	public int price_buy;

	public int attack;
	public int def;
	public int speed;
	public DressType dresstype;
	public AplyType aplytype;

}
