using UnityEngine;
using System.Collections;

public class InventoryItemgrid : MonoBehaviour {


	public int id=0;
	private ObjectInfo info=null;
	public int num=0;
	private UILabel numLabel;
	// Use this for initialization
	void Start () {
		numLabel = this.GetComponentInChildren<UILabel> ();

	}
	
	public void setGridId(int id,int num=1){//根据id设置物品显示信息
		this.id = id;
		info=ObjectsInfo._instance.GetObjectInfo(id);
		InventoryItem   itemInventory = this.GetComponentInChildren<InventoryItem> ();
		itemInventory.setIcon(id,info.iconname);
		numLabel.enabled=true;


		this.num = num;//变量赋值给属性
		//显示
		numLabel.text = num.ToString ();
	}

	public void plusNum(int num=1){
		this.num += num;
		numLabel.text = this.num.ToString ();
	}
public bool MinusNum(int num=1){
		if (this.num >= num) {
			this.num-=num;
			numLabel.text = this.num.ToString ();
			if(this.num==0){
				//要清空物品的格子
				ClearItem();
				GameObject.Destroy(this.GetComponentInChildren<InventoryItem>().gameObject);
			}
			return true;
				
		}
		return false;
	}

	public void ClearItem(){
		id = 0;
		num = 0;
		info = null;
		numLabel.enabled =false;

	}
}
