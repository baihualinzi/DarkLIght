using UnityEngine;
using System.Collections;

public class ShopDrug : MonoBehaviour {
	public static ShopDrug _instance;
	 private TweenPosition tween;
	private bool isShow=false;
	private int buy_id=0;
	private GameObject NumDialog;
	private UIInput inputxu;
	// Use this for initialization
	void Awake() {
		_instance = this;
		tween = this.GetComponent<TweenPosition> ();
		NumDialog = this.transform.Find ("NumDilag").gameObject;

		inputxu=transform.Find("NumDilag/NumInput").GetComponent<UIInput>();
		NumDialog.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	 public void Translante(){
		if (isShow == false) {
						tween.PlayForward ();
						isShow = true;
				} else {
			tween.PlayReverse();
			isShow=false;
		}
	}
	public void OncheckBack(){
		Translante ();
	}

	//步骤如下:
	//1.点击对应药品的buy按钮2.显示输入框输入数量得到物品的信息，显示物品的购买价计算总价
	//3.物品栏中金钱减少(取钱）,金钱显示余额，物品显示数量增加

	public void Onbuy1001(){
		Buy (1001);
	}
	public void Onbuy1002(){
		Buy (1002);
	}
	public void Onbuy1003(){
		Buy (1003);
	}
	public void OnbuyOK(){
		int count = int.Parse (inputxu.value);
		ObjectInfo info = ObjectsInfo._instance.GetObjectInfo (buy_id);
		int price = info.price_buy;
		int price_total = price * count;
		bool success=Inventory._instance.GetCoin (price_total);
		if (success) {
			if(count >0){
				Inventory._instance.GetId(buy_id,count);
			}		
		}
		NumDialog.SetActive (false);
	}
	void Buy(int id){
		//显示购买输入框
		ShowDialog ();
		buy_id = id;

	}
	void ShowDialog(){
		NumDialog.SetActive (true);
		inputxu.value="0";
	}

}