using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	private float speed=10.0f;
	private float endZ=-20.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z < endZ) {
			transform.Translate(Vector3.forward*speed*Time.deltaTime);	
		}
		
	}
}
