using UnityEngine;
using System.Collections;

public class PlayerwalkAnim : MonoBehaviour {

	private playerMove move;
	private PlayerAttack attackxu;
	// Use this for initialization
	void Start () {
		move = this.GetComponent<playerMove> ();
		attackxu = this.GetComponent<PlayerAttack> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {//动画慢于状态设置

		if (attackxu.playerattackxu == PlayerAttackState.ControllWalk) {
						if (move.state == PlayerState.Moving) {
								PlayerAnim ("Run");

						}
						if (move.state == PlayerState.Idle) {
								PlayerAnim ("Idle");	
						}

		
				} else if (attackxu.playerattackxu == PlayerAttackState.Attack_normal) {
			if(attackxu.attackIndisxu ==PlayerAttackIndis.Moving){
				PlayerAnim ("Run");
			}
		}
	
	}
	void PlayerAnim(string animName){
		animation.CrossFade (animName);
	}
}
