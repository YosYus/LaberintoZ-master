using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraP1 : MonoBehaviour {

		public GameObject player;
		private Vector3 distancia;



		// Use this for initialization
		void Start () {
			distancia = transform.position - player.transform.position;
			
		}

		// Update is called once per frame
		void Update () {
		    transform.position = player.transform.position + distancia;
		}

	
	}

