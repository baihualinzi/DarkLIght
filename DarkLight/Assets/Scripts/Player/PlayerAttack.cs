using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum PlayerAttackState
{
	ControllWalk,
	Attack_normal,
	Skill_attack,
	Death
}

//攻击时候的状态
public enum PlayerAttackIndis{
	Idle,
	Moving,
	Attack
}

public class PlayerAttack : MonoBehaviour {
	public static PlayerAttack _instance;
	public PlayerAttackState playerattackxu=PlayerAttackState.Attack_normal;
	public PlayerAttackIndis attackIndisxu=PlayerAttackIndis.Idle;
	public string Attack_normalAnim;
	public string Attack_idle;
	public string Attack_now;
	public string Death_anim;

	public float attack_rate=0.5f;
	public float attack_normalTime;//动画时间
	private float attack_time=0.0f;
	private float minAttackDis=5.0f;

	//特效
	private bool isShowEffect=false;//是否已经显示过特效
	public GameObject effect;
	private Transform attacknormal_target;
	private playerMove movexu;
	private PlayerStatus psxu;

	//主角miss
	public float missrateplayer=0.25f; 
	public GameObject hudtextpre;
	private GameObject hudtextGo;
	private GameObject hudtextFollow;
	private HUDText hudtext;
	public AudioClip misssong;
	public GameObject body;
	private Color normal;
	//特效数组
	public GameObject[] effectxu;
	private Dictionary<string,GameObject> skillIeffectDict = new Dictionary<string,GameObject> ();
//是否正在锁定目标
	public bool islocking=false;
	private SkillInfo infotarget=null;
	//死亡复活
	public string Re_born;
	CharacterController controller;
	private PlayerDir dir;

	void Awake(){
		_instance = this;
		movexu = this.GetComponent<playerMove> ();
		psxu = this.GetComponent<PlayerStatus> ();
		hudtextFollow= transform.Find ("HUDTextXu").gameObject;
		//主角控制器
		controller = this.GetComponent<CharacterController> ();
		dir = this.GetComponent<PlayerDir> ();

		normal = body.renderer.material.color;
		foreach (GameObject go in effectxu) {
			skillIeffectDict.Add(go.name,go);		
		}

	}

	void Start(){
		hudtextGo = NGUITools.AddChild (HUDParent._instance.gameObject, hudtextpre) ;//预设物体
		hudtext =hudtextGo.GetComponent<HUDText> ();
		UIFollowTarget followTarget = hudtextGo.GetComponent<UIFollowTarget> ();//临时变量
		followTarget.target = hudtextFollow.transform;
		followTarget.gameCamera = Camera.main;
	}
	void Update(){
			//正在释放技能不做检测		
		if (islocking==false&&  Input.GetMouseButtonDown (0)&&playerattackxu != PlayerAttackState.Death) {
			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			bool iscollider=Physics.Raycast(ray,out hitInfo);
			if(iscollider&&hitInfo.collider.tag==Tags.enemy){
				attacknormal_target=hitInfo.collider.transform;
				//print (attacknormal_target);
				playerattackxu=PlayerAttackState.Attack_normal;
				attack_time=0; isShowEffect=false;

			}
			else{
				playerattackxu=PlayerAttackState.ControllWalk;
				attacknormal_target=null;
			}
		

		}
		if(Input.GetMouseButtonDown(0)&&islocking==true){
			OnlockTarget();
		}
		//普通攻击
		if (playerattackxu == PlayerAttackState.Attack_normal) {
						if (attacknormal_target == null) {
								playerattackxu = PlayerAttackState.ControllWalk;
						} else {
								float distance = Vector3.Distance (transform.position, attacknormal_target.position);
								if (distance <= minAttackDis) {
										//进行攻击

										transform.LookAt (attacknormal_target.position);//面向怪物
										attackIndisxu = PlayerAttackIndis.Attack;
										attack_time += Time.deltaTime;
										animation.CrossFade (Attack_now);
										if (attack_time >= attack_normalTime) {
												Attack_now = Attack_idle;//进行过一次攻击了
												if (isShowEffect == false) {//大错误导致不断刷新攻击效果
														isShowEffect = true;
														GameObject.Instantiate (effect, attacknormal_target.position, Quaternion.identity);
														attacknormal_target.GetComponent<Wolfbaby> ().Takedamage (getAttack ());
												}
										}
										if (attack_time >= (1f / attack_rate)) {//下一次攻击
												attack_time = 0;
												isShowEffect = false;
												Attack_now = Attack_normalAnim;
										}
								} else {//走向敌人
										//播放行走动画
										attackIndisxu = PlayerAttackIndis.Moving;
										movexu.simplemove (attacknormal_target.position);
								}
						}
				} 
		else if(playerattackxu == PlayerAttackState.Death){
			animation.CrossFade(Death_anim);

	}
	}
	//死后隐藏复活点显示角色
	IEnumerator  fuhuo(){
			yield return StartCoroutine(WaitForDie());
			HideCharacter();
			yield return StartCoroutine(WaitForOneSeconds());
			if (checkpoint.isActivePt) {
				controller.transform.position = checkpoint.isActivePt.transform.position;	
				controller.transform.position = new Vector3(controller.transform.position.x,controller.transform.position.y+0.1f,controller.transform.position.z);
			}
			ShowCharacter();
		 //   animation.CrossFade(Re_born);
		   

		}
	
IEnumerator WaitForDie(){
		yield return new WaitForSeconds (3.5f);
	}
	
	IEnumerator WaitForOneSeconds(){
		yield return new WaitForSeconds(1.0f);
	}
	
	void HideCharacter(){
		GameObject.Find ("Magician_Body").GetComponent<SkinnedMeshRenderer> ().enabled = false;
		GameObject.Find ("Magician_Staff").GetComponent<SkinnedMeshRenderer> ().enabled = false;
		//playerattackxu = PlayerAttackState.ControllWalk;

	}
	
	void ShowCharacter(){
		GameObject.Find ("Magician_Body").GetComponent<SkinnedMeshRenderer> ().enabled = true;
		GameObject.Find ("Magician_Staff").GetComponent<SkinnedMeshRenderer> ().enabled =true;
	//	playerController.isControllable = true;
		dir.targetposition =  checkpoint.isActivePt.transform.position;
		playerattackxu = PlayerAttackState.ControllWalk;

	}





	//人物自身的攻击
	public int  getAttack(){
		return (int)(psxu.attack + psxu.attackPlus + Equipmentxu._instance.attack);
	}
	public void TakeDamage(int attack){
		if (playerattackxu == PlayerAttackState.Death)
						return;
		float def = Equipmentxu._instance.def + psxu.def + psxu.defPlus;
		float temp = attack * ((200 - def) / 200);//防御抵消伤害
		if (temp < 1)  temp = 1;
		float value = Random.Range (0f, 1f);
		if (value < missrateplayer) {
			AudioSource.PlayClipAtPoint(misssong,transform.position);
			hudtext.Add("Miss",Color.gray,1);
				} 
		else {//打中
			hudtext.Add("-"+temp,Color.yellow,1);
			psxu.hp_remain-=temp;
			//显示红色被攻击状态
			StartCoroutine(ShowRedAttack());
			if(psxu.hp_remain<=0){
				psxu.hp_remain=0;
			 playerattackxu=PlayerAttackState.Death;
				//Destroy(this.gameObject,2);//2s之后消失
			     StartCoroutine(fuhuo());
			}
		}
		FaceTextureUI._instance.Updateshow ();

	}
	IEnumerator ShowRedAttack(){
		body.renderer.material.color = Color.red;
		yield return new WaitForSeconds(1.0f);
		body.renderer.material.color = normal;
	}
	void OnDestroy(){
		GameObject.Destroy (hudtextGo);
	}
	public void useskill(SkillInfo skillinfo){
		//两种安全校验
		if (psxu.heroType == HeroStyle.Magician) {
			if(skillinfo.applyRole==ApplyRole.Swordman){
				return ;
			}
		}
		if (psxu.heroType == HeroStyle.Swordman) {
			if(skillinfo.applyRole==ApplyRole.Magician){
				return ;
			}
		}
		switch (skillinfo.applytype) {
		case ApplyType.Passive:
			 StartCoroutine( OnPassiveUse(skillinfo));
			break;
		case ApplyType.Buff:
			StartCoroutine( OnBuffUse(skillinfo));
			break;
		case ApplyType.SingleTarget:
			OnsingleUse(skillinfo);
			break;
		case ApplyType.MultiTarget:
			OnmultiUse(skillinfo);
			break;
		}



	}
	//增益
	IEnumerator OnPassiveUse(SkillInfo info){
		playerattackxu = PlayerAttackState.Skill_attack;
		animation.CrossFade (info.aniname);
		yield return new WaitForSeconds(info.anitime);
		playerattackxu = PlayerAttackState.ControllWalk;
		int hp = 0, mp = 0;
		if (info.applyPro == ApplyProperty.HP) {
						hp = info.applyValue;
				} else if (info.applyPro == ApplyProperty.Mp) {
						mp = info.applyValue;
				}
		psxu.GetDrug (hp, mp);

		//实例化特效
		GameObject prefab = null;
		skillIeffectDict.TryGetValue (info.efx_name, out prefab);
		GameObject.Instantiate (prefab, transform .position, Quaternion.identity);
	}

	//增强
	IEnumerator  OnBuffUse(SkillInfo info){
		playerattackxu = PlayerAttackState.Skill_attack;
		animation.CrossFade (info.aniname);
		yield return new WaitForSeconds(info.anitime);
		playerattackxu = PlayerAttackState.ControllWalk;
		switch (info.applyPro) {
		case ApplyProperty.Attack:
			psxu.attack*=(info.applyValue/100f);
			break;
		case ApplyProperty.Def:
			psxu.def*=(info.applyValue/100f);
			break;
		case ApplyProperty.AttackSpeed:
			attack_rate*=(info.applyValue/100f);
			break;
		case ApplyProperty.Speed:
			movexu.speed*=(info.applyValue/100f);
			break;
		}
		yield return new WaitForSeconds(info.applyTime);
		switch (info.applyPro) {
		case ApplyProperty.Attack:
			psxu.attack/=(info.applyValue/100f);
			break;
		case ApplyProperty.Def:
			psxu.def/=(info.applyValue/100f);
			break;
		case ApplyProperty.AttackSpeed:
			attack_rate/=(info.applyValue/100f);
			break;
		case ApplyProperty.Speed:
			movexu.speed/=(info.applyValue/100f);
			break;
		}
		//实例化特效
		GameObject prefab = null;
		skillIeffectDict.TryGetValue (info.efx_name, out prefab);
		GameObject.Instantiate (prefab, transform .position, Quaternion.identity);
	}
	//准备选择单个目标
  void OnsingleUse(SkillInfo info){
		playerattackxu = PlayerAttackState.Skill_attack;
		CursorManager._instance.setLocalTarget ();
		islocking = true;
		this.infotarget = info;
	}
	//选择目标完成，开始技能释放
	void OnlockTarget(){
		islocking = false;
		switch (infotarget.applytype) {
		case ApplyType.SingleTarget:
			StartCoroutine(	OnlocksingleTarget());
			break;
		case ApplyType.MultiTarget:
			StartCoroutine(	OnlockmultiTarget());
			break;
		}

	}
	IEnumerator OnlocksingleTarget(){
		Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		bool iscollider=Physics.Raycast(ray,out hitInfo);
		if(iscollider&&hitInfo.collider.tag==Tags.enemy){
			animation.CrossFade (infotarget.aniname);
			yield return new WaitForSeconds(infotarget.anitime);
			playerattackxu = PlayerAttackState.ControllWalk;
			//实例化特效
			GameObject prefab = null;
			skillIeffectDict.TryGetValue (  infotarget.efx_name, out prefab);
			GameObject.Instantiate (prefab, hitInfo.collider.transform.position, Quaternion.identity);

			hitInfo.collider.GetComponent<Wolfbaby>().Takedamage((int) (getAttack()*(infotarget.applyValue/100f)));
		}
		else{
			playerattackxu = PlayerAttackState.Attack_normal;
		}
		CursorManager._instance.setNormal ();
}
	void 	OnmultiUse(SkillInfo info){
		playerattackxu = PlayerAttackState.Skill_attack;
		CursorManager._instance.setLocalTarget ();
		islocking = true;
		this.infotarget = info;
	}
	IEnumerator OnlockmultiTarget(){
		CursorManager._instance.setNormal ();
				//点击地面
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hitInfo;
				bool iscollider = Physics.Raycast (ray, out hitInfo);
		print ("iscollider");
		if(iscollider&&hitInfo.collider.tag==Tags.ground){
			//if (iscollider) {
						animation.CrossFade (infotarget.aniname);
						yield return new WaitForSeconds (infotarget.anitime);
						playerattackxu = PlayerAttackState.ControllWalk;
			//实例化特效
			GameObject prefab = null;
			skillIeffectDict.TryGetValue (  infotarget.efx_name, out prefab);
			//print (prefab);
		GameObject go=	GameObject.Instantiate (prefab, hitInfo.point+Vector3.up*0.5f, Quaternion.identity) as GameObject;
			go.GetComponent<Magicsphere>().attack=getAttack()*(infotarget.applyValue/100f);
				} else {
			playerattackxu = PlayerAttackState.ControllWalk;
		}
		}
}
