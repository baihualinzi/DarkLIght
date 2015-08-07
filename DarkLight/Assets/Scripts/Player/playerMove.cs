using UnityEngine;
using System.Collections;
public enum PlayerState{
	Idle,
	Moving
}

public class playerMove : MonoBehaviour {
	public float speed=3.0f;
	public PlayerState state=PlayerState.Idle;
	public bool isMoving=false;
	private CharacterController controller;
	private PlayerDir dir;
	private PlayerAttack attackxu;

	// Use this for initialization
	void Start () {
		dir = this.GetComponent<PlayerDir> ();
		controller = this.GetComponent<CharacterController> ();
		attackxu = this.GetComponent<PlayerAttack> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (attackxu.playerattackxu == PlayerAttackState.ControllWalk) {
						float distance = Vector3.Distance (dir.targetposition, transform.position);
						if (distance > 0.3f) {
								isMoving = true;
								state = PlayerState.Moving;
								controller.SimpleMove (speed * transform.forward);
						} else {
								isMoving = false;
								state = PlayerState.Idle;
						}
				}


		}
	public void simplemove(Vector3 targetpos){
		transform.LookAt (targetpos);
		controller.SimpleMove (speed * transform.forward);
}
	}
