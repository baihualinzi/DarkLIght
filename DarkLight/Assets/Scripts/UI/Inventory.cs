using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Inventory : MonoBehaviour {

	public static Inventory _instance;
	public List<InventoryItemgrid> itemGrid = new List<InventoryItemgrid> ();
	public UILabel coinLabel;

	public GameObject itemxu;
	private TweenPosition tween;
	private int coinNum=1000;


	//List是一个接口，是Collection接口的一个子接口。是一个有序的集合。
	//而ArrayList是List的一个实现类，可以实现数组大小的可变，可以很方便的进行增加和删减数组内元素的操作
	// Use this for initialization

	void Awake(){
		_instance = this;
		//this.gameObject.SetActive (false);隐藏起来背包系统不能自增加
		tween = this.GetComponent<TweenPosition> ();
	}
	
	// Update is called once per frame
	void Update () {
	//测试
		if (Input.GetKeyDown (KeyCode.X)) {
			GetId(Random.Range(2001,2023));	
		}
	}
	//处理拾取ID物品的功能，并添加到物品栏中

	public void GetId(int id,int count=1){//键值
		//第一步查找在物品栏中所有物品是否存在该物品
		//如果存在让该物品num+1
		//第三步，如果不存在，查找空的方格，把这个inventoryItem放到空的方格里面
		InventoryItemgrid gridxu = null;
		foreach (InventoryItemgrid temp in itemGrid) {
			if(temp.id==id){//查找该物品
				gridxu=temp;
				break;
			}
		}
		//已经存在该物品
		if (gridxu != null) {
		gridxu.plusNum (count);
				}
		//不存在
		else {
			foreach (InventoryItemgrid temp in itemGrid) {
				if(temp.id==0)
				{
					gridxu=temp;
					break;
				}
			}
			if(gridxu!=null)
			{
			GameObject itemGo=NGUITools.AddChild(gridxu.gameObject,itemxu);

				//居中
				itemGo.transform.localPosition=Vector3.zero;
				//更新Grid
				//将item的depth设为4
				itemGo.GetComponent<UISprite>().depth=4;
				gridxu.setGridId(id,count);
			}
		}


	}
	//用物品(Drug),将物品栏拖到快捷栏
	public bool minusID(int id,int count=1){
		InventoryItemgrid gridxu = null;
		foreach (InventoryItemgrid temp in itemGrid) {
			if(temp.id==id){//查找该物品
				gridxu=temp;
				break;
			}
		}
		if (gridxu == null) {
						return false;//没得拖到快捷栏		

				} else {
			bool isSucess=gridxu.MinusNum(count);
			return isSucess;
		}
	}


private bool isShow = false;
 void show(){
		isShow = true;
		//tween.gameObject.SetActive (true);
		tween.PlayForward ();
	}
void hide(){
		isShow = false;
		tween.PlayReverse ();
	}
/*void OnPlayFinished(){//监听动画结束，提高性能,//隐藏对话框不能监听事件
		if (isShow == false) {
			this.gameObject.SetActive(false);		
		}
	}*/
	public void TranslateStatus(){
		if (isShow == false) {
						show ();		
				} else {
			hide ();		
		}
	}
	public void AddCoin(int count){
		coinNum+=count;
		coinLabel.text=coinNum.ToString();//更新显示
	}

	public bool GetCoin(int count){//从1000中取款多少钱
		if (coinNum > count) {
			coinNum-=count;
			coinLabel.text=coinNum.ToString();//更新显示
			return true;
		}
		return false;
	}

}
