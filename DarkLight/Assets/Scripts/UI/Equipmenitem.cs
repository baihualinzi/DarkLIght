using UnityEngine;
using System.Collections;

public class Equipmenitem : MonoBehaviour {

	private UISprite sprite;
	private bool isHover=false;
	public int id;
	// Use this for initialization
	void Awake () {

		sprite = this.GetComponent<UISprite> ();
	}
	
	// Update is called once per frame
	void Update(){
		if (isHover==true) {
			if(Input.GetMouseButtonDown(1)){
				Equipmentxu._instance.Takeoff(id,this.gameObject);


			}	
		}

	}
	public void setId(int id){
		this.id = id;
		ObjectInfo info = ObjectsInfo._instance.GetObjectInfo (id);
		setInfoxu (info);
	}
	public void setInfoxu(ObjectInfo info){
		this.id = info.id;

		sprite.spriteName = info.iconname;
	}
	public void OnHover(bool isover){
		isHover = isover;

	}
}
