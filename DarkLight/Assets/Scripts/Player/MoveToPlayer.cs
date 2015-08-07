using UnityEngine;
using System.Collections;

public class MoveToPlayer : MonoBehaviour {

	private Transform player;
	private Vector3 offsetPlayer;
	private bool isRotating=false;
	public float distance=0;
	public float ScrollSpeed=6.0f;
	public float RotateSpeed=2.0f;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		transform.LookAt (player);
		offsetPlayer = transform.position - player.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = offsetPlayer + player.position;
	
		//处理事业的旋转
		RotateView ();
		//s鼠标拉近拉远效果,中建
		ScrollView ();


	
	}
	void ScrollView(){
		//滑轮向前正值，向后滑动负值，拉远视野
		distance = offsetPlayer.magnitude;
		distance -= Input.GetAxis ("Mouse ScrollWheel")*ScrollSpeed;
		distance=Mathf.Clamp (distance, 3, 10);
		offsetPlayer = offsetPlayer.normalized * distance;
	}
	void RotateView (){
		if (Input.GetMouseButtonDown (0)&&UICamera.hoveredObject==null) {
						isRotating = true;
				}
		if(Input.GetMouseButtonUp(0)){
			isRotating=false;
		}
		if(isRotating==true){

			Vector3 originalpos=transform.position;
		    Quaternion	originalrot=transform.rotation;
			transform.RotateAround(player.position,player.up,RotateSpeed*Input.GetAxis("Mouse X"));
			transform.RotateAround(player.position,transform.right,-RotateSpeed*Input.GetAxis("Mouse Y"));
			float x=transform.eulerAngles.x;
			if(x<10 || x>80)//超出范围，旋转无效
			{
				transform.position=originalpos;
				transform.rotation=originalrot;
			}
		}
		offsetPlayer = transform.position - player.position;
	}
}
