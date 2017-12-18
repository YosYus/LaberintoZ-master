using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeAI : MonoBehaviour {

	private NavMeshAgent nav;
	public Transform objective;
	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		nav.SetDestination (objective.position);
	}
}
