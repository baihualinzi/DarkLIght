using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour {
	//通过单例模式可以保证系统中一个类只有一个实例而且该实例易于外界访问，从而方便对实例个数的控制并节约系统资源。
	//如果希望在系统中某个类的对象只能存在一个,这一模式的目的是使得类的一个对象成为系统中的唯一实例
	public static CursorManager _instance;//让光标管理系统成为这个游戏的唯一实例
	public Texture2D cursorAttack;
	public Texture2D cursorLockTarget;
	public Texture2D cursorNormal;
	public Texture2D cursorNPC;
	public Texture2D cursorPick;

	private Vector2 hotspot=Vector2.zero;
	//鼠标设置是硬件设置还是软件设置
	private CursorMode mode=CursorMode.Auto;

	// Use this for initialization
	void Start () {
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void setNormal(){
		Cursor.SetCursor (cursorNormal, hotspot, mode);
	}
	public void setNpcTalk(){
		Cursor.SetCursor (cursorNPC, hotspot, mode);
	}
	public void setAttack(){
		Cursor.SetCursor (cursorAttack, hotspot, mode);
	}
	public void setLocalTarget(){
		Cursor.SetCursor (cursorLockTarget, hotspot, mode);
	}
}
