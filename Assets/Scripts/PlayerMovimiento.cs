using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimiento : MonoBehaviour {


	private CharacterController controlador;
	public float velocidadgiro = 2;
	public float velocidad = 2;

	void Start(){

		controlador = GetComponent<CharacterController>();

	}
	void Update () {
		

		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");
		moverse (hor, ver);
	}

	void moverse(float hor, float ver){

		float giro = hor * velocidadgiro;
		transform.Rotate (new Vector3 (0, giro, 0));
		//Vector3 haciaDelante = transform.TransformDirection (Vector3.forward * ver * velocidad);
		Vector3 haciaDelante = Vector3.forward;
		controlador.SimpleMove (haciaDelante);
	}
}

