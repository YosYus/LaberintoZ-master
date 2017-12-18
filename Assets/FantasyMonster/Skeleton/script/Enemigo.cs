using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour {
	
	public Transform player;
	public Transform head;

	NavMeshAgent nav;

	private Animator anim;

	// Use this for initialization
	void Start () {
		
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = player.position - this.transform.position;
		float angle = Vector3.Angle (direction,head.up);
		direction.y = 0;

		if (Vector3.Distance (player.position, this.transform.position) < 20 && angle <40) {
			
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			this.transform.rotation=  Quaternion.Slerp(this.transform.rotation,targetRotation, 0.1f);
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isWalking", false);
			if (direction.magnitude > 10) {
				this.transform.Translate (0, 0, 0.05f);
				anim.SetBool ("isWalking", true);
				anim.SetBool ("isAttacking", false);	
			} else {
				anim.SetBool ("isAttacking", true);
				anim.SetBool ("isWalking", false);
			}


		} else {
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isAttacking", false);
		}
			//nav.SetDestination(player.position);
	}


	void OnTriggerEnter(Collider other){
		bool correEnemy = true;
		anim.SetBool ("isWalking", correEnemy);
		nav.SetDestination(player.position);
	}

	void OnTriggerExit(Collider other){
		bool correEnemy = false;
		anim.SetBool ("isWalking", correEnemy);
		nav.SetDestination(player.position);
	}
}
