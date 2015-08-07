using UnityEngine;
using System.Collections;

public enum Wolfstate{
	Idle,
	Walk,
	Attack,
	Death
}



public class Wolfbaby : MonoBehaviour {

	public  Wolfstate wolfstatexu=Wolfstate.Idle;

	public string wolf_death;
	public string wolf_idle;
	public string wolf_walk;
	public string wolf_now;
	public float speed=1;
	public   float time=1;

	public float miss_rate=0.2f;
	public int hp=100;
	public AudioClip misssong;
	private GameObject HUDFollow;
	private GameObject HUDGo;
	public GameObject HUDtextprefab;
	private HUDText hudtext;//MISS字符冒泡的显示
	private UIFollowTarget followTarget;

	private CharacterController cc;
	private  float timer=0;//计时
	private Color normal;
	public  GameObject baby;
	private bool isAttack=false;

	//攻击
	public string wolf_normalAttack;
	public float wolfnormal_time;//播放动画时间
	public string wolf_crazyAttack;
	public float wolfcrazy_time;
	//疯狂攻击的触发概率
	public float wolfcrazy_rate;

	//当前攻击状态
	public string wolf_nowAttack;
	public int attack_rate=1;
	private float attack_time=0.0f;//攻击计时
	public int wolf_damage;
//攻击目标
	public Transform targetPlayer;
	public float  mindistance=2.0f;
	public float  maxdistance=5.0f;

	public  WolfCreate wolfcxu;
	private PlayerStatus psxu;
	//经验值
	public int expwolf=20;
	void Awake(){
		wolf_now = wolf_idle;
//		baby = transform.Find ("Wolf_Baby").gameObject;
		cc = this.GetComponent<CharacterController> ();
		normal = baby.renderer.material.color;
		HUDFollow = transform.Find ("HUDTextXu").gameObject;//小狼的上面物体
	}

	void Start(){
	//	HUDGo = GameObject.Instantiate (HUDtextprefab, Vector3.zero, Quaternion.identity) as GameObject;
		HUDGo = NGUITools.AddChild (HUDParent._instance.gameObject, HUDtextprefab) ;//预设物体
		hudtext = HUDGo.GetComponent<HUDText> ();
		followTarget = HUDGo.GetComponent<UIFollowTarget> ();
		followTarget.target = HUDFollow.transform;
	    followTarget.gameCamera = Camera.main;
       // followTarget.uiCamera = UICamera.current.GetComponent<Camera> ();
		psxu = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerStatus> ();
	}

	// Update is called once per frame
	void Update () {
		if (wolfstatexu == Wolfstate.Death) {
						animation.CrossFade (wolf_death);

				} else if (wolfstatexu == Wolfstate.Attack) {
				//攻击状态
			autoAttack();
				}
		else {//巡逻
			animation .CrossFade(wolf_now);
			if(wolf_now==wolf_walk){
				cc.SimpleMove(transform.forward*speed);
			}


			timer+=Time.deltaTime;
			if(timer>=time){//切换状态
				timer=0;
				RandomState();//切换随机状态
			}
		}
	}
	void RandomState(){
		int value=Random.Range(0,2);
	//	print(value);
		if(value==0)
		{
			wolf_now=wolf_idle;

		}
		else{
			if(wolf_now!=wolf_walk){
				transform.Rotate(transform.up*Random.Range(0,360));
			}

			wolf_now=wolf_walk;
		}
	}
	void autoAttack(){
		PlayerAttackState playerstate = targetPlayer.GetComponent<PlayerAttack> ().playerattackxu;
		if (playerstate == PlayerAttackState.Death) {
			targetPlayer=null;
			
			wolfstatexu=Wolfstate.Idle;
			return;
				}
		if (targetPlayer != null) {
			float distancexu=Vector3.Distance(targetPlayer.position,transform.position);
			if(distancexu<=mindistance){
				//自动攻击
				attack_time+=Time.deltaTime;//完成最后一帧所需时间
				animation.CrossFade(wolf_nowAttack);
				if(wolf_nowAttack==wolf_normalAttack){
					if(attack_time>wolfnormal_time){
						//产生伤害
						targetPlayer.GetComponent<PlayerAttack>().TakeDamage(wolf_damage);
						wolf_nowAttack=wolf_idle;
					}

				}else if(wolf_nowAttack==wolf_crazyAttack){
					if(attack_time>wolfcrazy_time){
						//产生伤害
						targetPlayer.GetComponent<PlayerAttack>().TakeDamage(wolf_damage);
						wolf_nowAttack=wolf_idle;
					}
				}
				if(attack_time>1/attack_rate){
					//再次攻击
					RandomAttack();
					attack_time=0;
				}

			}
			else if(distancexu>maxdistance){
				targetPlayer=null;

				wolfstatexu=Wolfstate.Idle;
			}
			else{
				//朝向目标
				transform.LookAt(targetPlayer.position);
				cc.SimpleMove(transform.forward*speed);
				animation .CrossFade(wolf_walk);
			//	wolfstatexu=Wolfstate.Walk;
			}
				} 
		else {
			wolfstatexu=Wolfstate.Idle;
		}
	}
	void RandomAttack(){
		float value = Random.Range (0f, 1f);
		if (value < wolfcrazy_rate) {
			wolf_nowAttack=wolf_crazyAttack;
				} else {
			wolf_nowAttack=wolf_normalAttack;
		}
	}


	public void Takedamage(int damage){
		if (wolfstatexu == Wolfstate.Death) return;

		//攻击主角目标
		targetPlayer = GameObject.FindGameObjectWithTag (Tags.player).transform;
		wolfstatexu = Wolfstate.Attack;
		float value = Random.Range (0.0f, 1.0f);
		if (value < miss_rate) {//miss
			AudioSource.PlayClipAtPoint(misssong,transform.position);
			hudtext.Add("Miss",Color.blue,1);
				}
		else {
			hudtext.Add("-"+damage,Color.yellow,1);
			this.hp-=damage;
//显示红色被攻击状态
			StartCoroutine(ShowRedAttack());
			if(hp<=0){
				wolfstatexu=Wolfstate.Death;
				Destroy(this.gameObject,2);//2s之后消失
			}
		}
	}
	IEnumerator ShowRedAttack(){
		baby.renderer.material.color = Color.red;
		yield return new WaitForSeconds(1.0f);
		baby.renderer.material.color = normal;
	}
	//当MonoBehaviour将被销毁时，这个函数被调用
		void OnDestroy(){
		wolfcxu.minusnum ();
		psxu.GetExp (expwolf);
		BNPC._instance.OnKillwolf ();
		GameObject.Destroy (HUDGo);
		}
	void OnMouseEnter(){

		if (PlayerAttack._instance.islocking == false) {
						CursorManager._instance.setAttack ();
				}
	}
	void OnMouseExit(){
		if (PlayerAttack._instance.islocking == false) {
						CursorManager._instance.setNormal ();
				}
	}
}
