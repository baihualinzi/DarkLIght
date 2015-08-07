using UnityEngine;
using System.Collections;

public class shopweapon : MonoBehaviour {

	public static shopweapon _instance;
	public int[] weaponArray;
	public GameObject weaponItem;
	public UIGrid grid;

	private bool isShow=false;
	private TweenPosition tween;
	private GameObject numdialog;
	private UIInput numInput;
	private int buyId;

	void Awake(){
		_instance = this;
		tween = this.GetComponent<TweenPosition> ();
		numdialog = transform.Find ("Panel/NumDilag").gameObject;
		numInput = transform.Find ("Panel/NumDilag/NumInput").GetComponent<UIInput> ();
		numdialog.SetActive (false);

	}
	void Start(){
		InitWeapon ();//objectsinfo是在awake方法里，所以要在awake以后调用，单例模式的赋值一般在awake方法中
	}
	public void TranlateStatus(){
		if (isShow == false) {
						tween.PlayForward ();
						isShow = true;
				}
			else{
			tween.PlayReverse();
			isShow=false;

		}
	}

	public void OncloseBtn(){
		TranlateStatus ();
	}
	void InitWeapon(){
		foreach (int id in weaponArray) {
			GameObject itemgo=NGUITools.AddChild(	grid.gameObject,weaponItem);
			grid.AddChild(itemgo.transform);
			itemgo.GetComponent<WeaponItem>().setId(id);
		}
	}
	//点击ok按钮
	public void OnokBtn(){
		int count = int.Parse (numInput.value);
		if (count > 0) {
			int pricebuy=ObjectsInfo._instance.GetObjectInfo(buyId).price_buy;
			int total=pricebuy*count;
			bool sucess=Inventory._instance.GetCoin(count);
			if(sucess){
				Inventory._instance.GetId(buyId,count);

			}
		}
		//如果购买数小于等于0
		buyId = 0;
		numInput.value = "0";
		numdialog.SetActive (false);
	}
	public void OnBuybtn(int id){//将weaponitem的id传过来,就是点击某一武器项调用这个面板函数

		buyId = id;
		numdialog.SetActive (true);
		numInput.value = "0";
	

	}
	//点击面板也会隐藏ok
	public void OnClick(){//一次点击事件
		numdialog.SetActive (false);
	}

}
