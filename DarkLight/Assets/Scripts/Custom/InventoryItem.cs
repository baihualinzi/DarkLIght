using UnityEngine;
using System.Collections;

public class InventoryItem :UIDragDropItem {


	private UISprite sprite;
	private int id;

	// Use this for initialization
//	void Start () {
	//如果继承父类start并在自己的start改变的话用base.start()然后在实现。

	void Awake(){
		sprite = this.GetComponent<UISprite> ();
	}
	void Update(){
		if (isHover == true) {
		//显示信息
			InventoryDes._instance.show(id);
			if(Input.GetMouseButtonDown(1)){
				bool sucess=Equipmentxu._instance.Dress(id);
				if(sucess){
					transform.parent.GetComponent<InventoryItemgrid>().MinusNum();
				}
			}
		}
	}
	//可拖拽功能
	//当我们拖拽物品surface放下（Release）时，此函数会被触发
	protected override void OnDragDropRelease(GameObject surface){//重写OnDragDropRelease方法
		base.OnDragDropRelease (surface);//调用父类的OnDragDropRelease(surface)方法
		if (surface != null) {
						if (surface.tag == Tags.inventory_itemgrid) {//拖拽到一个空的格子
								if (surface == this.transform.parent.gameObject) {//拖到自己原先的格子
									//	ResetPosition ();
								} else {//拖到别的空的格子1.别的格子有物品id，数量，自己的也要清空
										InventoryItemgrid oldparent = this.transform.parent.GetComponent<InventoryItemgrid> ();
										this.transform.parent = surface.transform;//坐标先转移
										ResetPosition ();
										InventoryItemgrid newparent = surface.GetComponent<InventoryItemgrid> ();
										newparent.setGridId (oldparent.id, oldparent.num);//id转移 ,num转移
										oldparent.ClearItem ();//自己清空

								}
						} else if (surface.tag == Tags.inventory_item) {//拖拽到其他有物品的格子
								//调试错误：标签错误
								InventoryItemgrid grid1 = this.transform.parent.GetComponent<InventoryItemgrid> ();
								InventoryItemgrid grid2 = surface.transform.parent.GetComponent<InventoryItemgrid> ();
								//保存原始格子的信息
								int id = grid1.id;
								int num = grid1.num;
								//改变原始格子的id，num为surface格子
								grid1.setGridId (grid2.id, grid2.num);//old->new
								grid2.setGridId (id, num);//new->old
								ResetPosition ();
						} else if (surface.tag == Tags.shortcut) {
								surface.GetComponent<Shortcutkey> ().setInventory (id);
						} 
			//else {
		//						ResetPosition ();//不为空时，拖拽到inventory面板的情况
			//			}
				}		//print (surface.tag);
	//else {
	//					ResetPosition ();
	//			}
		ResetPosition ();
	}
	void ResetPosition(){
		transform.localPosition = Vector3.zero;
	}
	//根据ID更新他的显示
	public void setId(int id){
		ObjectInfo info = ObjectsInfo._instance.GetObjectInfo (id);
		sprite.spriteName = info.iconname;
	} 
	public void setIcon(int id ,string icon_name){
		sprite.spriteName = icon_name;
		this.id = id;//外界调用这个函数将会把id传进来，然后利用这个id显示信息

	}
	private bool isHover=false;
	public void OnHoverOver(){//鼠标移到目标物体
		isHover = true;
	}//监听鼠标移到物品之上的事件
	public void OnHoverOut(){
		isHover = false;
	}

}
