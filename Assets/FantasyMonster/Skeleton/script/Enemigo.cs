using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour {
	
	Transform player;

	NavMeshAgent nav;

	private Animator anim;

	// Use this for initialization
	void Start () {
		player= GameObject.FindGameObjectWithTag("Arturo").transform;
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		nav.SetDestination(player.position);
	}


	void OnTriggerEnter(Collider other){
		bool correEnemy = true;
		anim.SetBool ("corriendo", correEnemy);
		nav.SetDestination(player.position);
	}

	void OnTriggerExit(Collider other){
		bool correEnemy = false;
		anim.SetBool ("corriendo", correEnemy);
		nav.SetDestination(player.position);
	}
}
