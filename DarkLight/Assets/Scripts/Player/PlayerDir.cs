using UnityEngine;
using System.Collections;

public class PlayerDir : MonoBehaviour {

	public GameObject effect_click;
	private bool isMoving=false;
	private playerMove playMove;
	public Vector3 targetposition=Vector3.zero;

	private PlayerAttack plyattack;


	// Use this for initialization
	void Start () {
		targetposition = transform.position;
		playMove = this.GetComponent<playerMove> ();
		plyattack = this.GetComponent<PlayerAttack> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (plyattack.islocking==false&&  Input.GetMouseButtonDown (1)&&UICamera.hoveredObject==null&&plyattack.playerattackxu != PlayerAttackState.Death){//判断鼠标有没有点击的NGUI控件，如果为空没有点击到&&UICamera.hoveredObject==null
			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			bool iscollider=Physics.Raycast(ray,out hitInfo);
			if(iscollider&&hitInfo.collider.tag==Tags.ground){
				isMoving=true;//鼠标与地面有接触
				showClickEffect(hitInfo.point);
				LookAtTarget(hitInfo.point);

			}
			     

		}

				
				

		if(Input.GetMouseButtonUp(1))
		   {
			isMoving=false;
		}
		if (isMoving == true) {
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit hitInfo;
						bool iscollider = Physics.Raycast (ray, out hitInfo);
						if (iscollider && hitInfo.collider.tag == Tags.ground) {
								LookAtTarget (hitInfo.point);
						}
				} 
		//在鼠标按下和抬起的间隔中如果主角遇到地形遮挡改变原有的方向，就会一直走，这样我们判断一下主角在这段间隔如果走的话就让它一直朝目标的点
		else if (playMove.isMoving == true) {
			LookAtTarget(targetposition);
		}

	}
	void showClickEffect(Vector3 hitpoint){
		hitpoint = new Vector3 (hitpoint.x, hitpoint.y + 0.1f, hitpoint.z);
		GameObject.Instantiate (effect_click, hitpoint, Quaternion.identity);
	}
	void LookAtTarget(Vector3 hitpoint){
		targetposition = hitpoint;
		targetposition = new Vector3 (targetposition.x, transform.position.y, targetposition.z);//与主角y轴一致
		this.transform.LookAt (targetposition);
	}

		
}
