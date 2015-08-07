using UnityEngine;
using System.Collections;

public class minmap : MonoBehaviour {
	private Camera minmapxu;

	void Start(){
		minmapxu = GameObject.FindGameObjectWithTag (Tags.minimap).GetComponent<Camera> ();
	}

	public void ZoomInClick(){
		if (minmapxu.orthographicSize >= 0) {

						minmapxu.orthographicSize--;
				}
	}
	public void ZoomOutClick(){

		minmapxu.orthographicSize++;
	}
}
