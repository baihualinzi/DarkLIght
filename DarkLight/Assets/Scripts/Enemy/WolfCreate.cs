using UnityEngine;
using System.Collections;

public class WolfCreate : MonoBehaviour {

	public int maxWolfnum = 6;
	public int  currentNum=0;
	public GameObject prefabwolf;

	public float timecreate=3.0f;
	private float timer = 0.0f;


	void Update(){
		if (currentNum < maxWolfnum) {
			timer+=Time.deltaTime;//时间累积
			if(timer>=timecreate){
		        Vector3 pos=transform.position;
				pos.x+=Random.Range(-5,5);
				pos.z+=Random.Range(-5,5);
				GameObject go=GameObject.Instantiate(prefabwolf,pos,Quaternion.identity) as GameObject;
				go.GetComponent<Wolfbaby>().wolfcxu=this;//

				timer=0;
				currentNum++;
			}
		}


	}
	public void minusnum(){
		this.currentNum--;
	}

}
