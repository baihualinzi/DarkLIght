using UnityEngine;
using System.Collections;

public class ButtonContain : MonoBehaviour {

	//1.游戏数据的保存和场景之间游戏数据的传递使用playerprefs
	//2.场景的分类
	//2.1 开始场景
	//2.2 角色选择场景
	//2.3 战斗场景，村庄，野怪
	public void OnNewGame(){
		//角色选择

		PlayerPrefs.SetInt ("DataFromSave", 0);
		Application.LoadLevel (1);
	}
	//保存加载游戏
	public void OnLoadGame(){
		//加载3场景
		PlayerPrefs.SetInt ("DataFromSave", 1);
	}
}
