using UnityEngine;
using System.Collections;

public class Skillxu : MonoBehaviour {

	public static Skillxu _instance;
	private bool isShow=false;
	private TweenPosition tween;
	private PlayerStatus ps;

	public int[] magicianskill;
	public int[] swordskill;
	public UIGrid grid;
	public GameObject skillItemPre;
	// Use this for initialization
	//基本的初始化在Awake中执行
	//需要依赖其他属性来完成初始化的在start中执行
	void Awake () {
		_instance = this;
		tween = this.GetComponent<TweenPosition> ();

	}

	void Start(){
		ps = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerStatus> ();
		int[] idList = null;
		switch (ps.heroType) {
		case HeroStyle.Magician:
			idList=magicianskill;
			break;
		case HeroStyle.Swordman:
			idList=swordskill;
			break;

		}
		foreach (int id in idList) {
		GameObject itemgo=	NGUITools.AddChild(grid.gameObject,skillItemPre);	
			grid.AddChild(itemgo.transform);//itemgo交给grid管理
			itemgo.GetComponent<skillItem>().setId(id);
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
	public void TranslateStatus(){
		if (isShow == false) {
						tween.PlayForward ();
						isShow = true;
			UpdateShow();
				} 
		else {
			tween.PlayReverse();
			isShow=false;
		}
	}
	void UpdateShow(){
		skillItem[] items = this.GetComponentsInChildren<skillItem> ();
		foreach (skillItem item in items) {
			item.UpdateShowItem(ps.level);
		}
	}
}
