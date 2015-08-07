using UnityEngine;
using System.Collections;

public class Statusxu : MonoBehaviour {
	//单例模式
	public static Statusxu _instance;
	public UILabel attackLabel;
	public UILabel defLabel;
	public UILabel speedLabel;
	public UILabel pointRemain;
	public UILabel summary;
	public GameObject attackPlusbtn;
	public GameObject defPlusbtn;
	public GameObject speedPlusbtn;


	private TweenPosition tween;
	private bool isShow=false;
	private PlayerStatus psxu;
	// Use this for initialization
	void Awake() {
		_instance = this;
		tween = this.GetComponent<TweenPosition> ();

	}
	void Start(){
		psxu = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerStatus> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void UpdateShow(){//根据ps的值更新显示
		attackLabel.text = psxu.attack + "+" + psxu.attackPlus;
		defLabel.text = psxu.def + "+" + psxu.defPlus;
		speedLabel.text = psxu.speed + "+" + psxu.speedPlus;
		pointRemain.text = psxu.pointRemain.ToString ();
		summary.text = "伤害:" + (psxu.attack + psxu.attackPlus) + " " + "防御:" + (psxu.def + psxu.defPlus) + " " + "速度:" +
						(psxu.speed + psxu.speedPlus);
		if (psxu.pointRemain > 0) {
						attackPlusbtn.SetActive (true);
						defPlusbtn.SetActive (true);
						speedPlusbtn.SetActive (true);
				} else {
			attackPlusbtn.SetActive (false);
			defPlusbtn.SetActive (false);
			speedPlusbtn.SetActive (false);
		}

	}
	public void TranslateStatus(){
		if (isShow == false) {
			tween.PlayForward();
			UpdateShow();
			isShow=true;		
		} else {
			tween.PlayReverse();
			isShow=false;
		}
	}
	public void OnattackPlusbtn(){
		bool sucess = psxu.GetPoint ();
		if (sucess) {
			psxu.attackPlus++;
			UpdateShow();
		}
	}
	public void OndefPlusbtn(){
		bool sucess = psxu.GetPoint ();
		if (sucess) {
			psxu.defPlus++;
			UpdateShow();
		}
	}
	public void OnspeedPlusbtn(){
		bool sucess = psxu.GetPoint ();
		if (sucess) {
			psxu.speedPlus++;
			UpdateShow();
		}
	}
}
