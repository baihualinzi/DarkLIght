using UnityEngine;
using System.Collections;


public enum shortcuttype{
	Skill,
	Drug,
	None
}


public class Shortcutkey : MonoBehaviour {


	public KeyCode keycode;
	private UISprite iconsprite;
	private SkillInfo skillinfoicon;
	private int id;
	private PlayerStatus psxu;
	private PlayerAttack paxu;
	private shortcuttype typexu=shortcuttype.None;

	private ObjectInfo objectinfoxu;
	void Awake(){
		iconsprite = transform.Find ("icon").GetComponent<UISprite> ();
		iconsprite.gameObject.SetActive (false);

	}
	void Start(){
		psxu = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerStatus> ();
		paxu=GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerAttack> ();
	}
	void Update(){
		//处理按键按下药品和技能的反应
		if (Input.GetKeyDown (keycode)) {
			if(typexu==shortcuttype.Drug){
				OnDrugUse();
			}
			else if(typexu==shortcuttype.Skill){
				//释放技能
				//1.得到该技能的Mp
				bool success=psxu.TakeMp(skillinfoicon.mp);
				if(success==false){
					return;
				}
				else{
					//2.获得MP释放技能
					paxu.useskill(skillinfoicon);
				}
			}
		}
	}
	//更新显示技能
	public void setskill(int id){
		this.id = id;
		this.skillinfoicon = SkillsInfo._instance.GetSkillInfoById (id);//取出对应id的信息
		//将此id显示出来
		iconsprite.gameObject.SetActive (true);
		iconsprite.spriteName = skillinfoicon.icon_name;
		typexu = shortcuttype.Skill;


	}
	public void setInventory(int id){
				this.id = id;
				objectinfoxu = ObjectsInfo._instance.GetObjectInfo (id);
				if (objectinfoxu.type == ObjectType.Drug) {
						iconsprite.gameObject.SetActive (true);
						iconsprite.spriteName = objectinfoxu.iconname;
						typexu = shortcuttype.Drug;//快捷方式类型
				}
		}
	//快捷栏是用药品
public void OnDrugUse(){
	bool sucess=	Inventory._instance.minusID (id,1);
		if (sucess) {
						psxu.GetDrug (objectinfoxu.hp, objectinfoxu.mp);		
				}
		//如果不能用
		else {
			typexu=shortcuttype.None;
			iconsprite.gameObject.SetActive(false);
			id=0;
			skillinfoicon=null;
			objectinfoxu=null;

		}
	}
}
