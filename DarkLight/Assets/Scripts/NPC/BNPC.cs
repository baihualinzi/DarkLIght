using UnityEngine;
using System.Collections;

public class BNPC : NPC {
	public static BNPC _instance;
	public TweenPosition questTween;
	public UILabel DesLabel;
	public GameObject AcceptBtn;
	public GameObject OkBtn;
	public GameObject CancelBtn;

	public bool isInTask=false;//是否在任务中
	public int killCount=0;//任务进度已经杀死几只狼
	private PlayerStatus status;


	void Awake(){
		_instance = this;
	}
	void start(){

		status = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStatus> ();
	}
	// Use this for initialization
	void OnMouseOver(){
		if (Input.GetMouseButtonDown(0)) {
			audio.Play();
			if(isInTask==true){
				showTaskProgress();
			}
			else{
				showTaskDes();
			}
			showquest();		
		}
	}
	
	void showquest(){
		questTween.gameObject.SetActive (true);
		questTween.PlayForward ();//向前播放动画

	}
	public void OnCloseButton(){
		hidequest ();

	}
	void hidequest(){
		questTween.PlayReverse ();//向后播放动画
	}
public void OnKillwolf(){
		if (isInTask) {
			killCount++;
		}
	}

	void showTaskProgress(){
		DesLabel.text = "任务:\n你已经杀死了"+killCount+"/10只狼\n\n奖励:\n1000金币";
		AcceptBtn.SetActive (false);
		OkBtn.SetActive (true);
		CancelBtn.SetActive (false);
	}
	void showTaskDes(){
		DesLabel.text = "任务:\n杀死了10只狼\n\n奖励:\n1000金币";
		AcceptBtn.SetActive (true);
		CancelBtn.SetActive (true);
		OkBtn.SetActive (false);
	}
	public void OnClickAccept(){
		showTaskProgress ();
		isInTask = true;//表示在任务中
	}
	public void OnClickCancel(){
		hidequest ();
	}
	//抽象类是接口和普通类的过渡类型，处于半实现状态
	public void OnOk(){
		if (killCount >= 10) {
						//完成任务
		//	status.GetCoin(1000);
			Inventory._instance.AddCoin(1000);
			killCount=0;
			showTaskDes();
				} else {
			hidequest();
		}
	}
}
