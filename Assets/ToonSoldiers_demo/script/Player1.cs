using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {
	
	//Variables públicas
	 public float velocidad = 15f;
	 public float velGiro = 12f;
	 public Transform enemigo;
	 public Transform head;
	 
	 //Variables privadas
	 private Animator anim;
	 private Rigidbody playerRigidbody;


	 private Vector3 desplazamiento;
	 
	 
	 
	// Use this for initialization
	void Start () {
		anim= GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		float hor = Input.GetAxisRaw("Horizontal");
		float ver = Input.GetAxisRaw("Vertical");
		moverPlayer(hor,ver);
		animaPlayer(hor,ver);
	}
	
	//Metodo mover el player
	void moverPlayer(float hor, float ver){
		desplazamiento.Set(hor,0f, ver);
		desplazamiento = desplazamiento.normalized * velocidad * Time.deltaTime;
		playerRigidbody.MovePosition(transform.position + desplazamiento);
		
		 if(hor != 0f || ver != 0f){
			 giroPlayer(hor,ver);
		 }
		
	}
	
	//Metodo girar el player
	void giroPlayer(float hor, float ver){
		
		Vector3 targetDirection = new Vector3 (hor,0f, ver);
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection,Vector3.up);
		Quaternion newRotation = Quaternion.Lerp(playerRigidbody.rotation,targetRotation, velGiro * Time.deltaTime);
		playerRigidbody.MoveRotation(newRotation);
		
		
	}
	
	
	void animaPlayer(float hor, float ver){
		bool correPlayer =hor != 0f || ver != 0f;
		anim.SetBool("isRunning",correPlayer);

		/*
		Vector3 direction = enemigo.position - this.transform.position;
		float angle = Vector3.Angle (direction,head.up);

		if (Vector3.Distance (enemigo.position, this.transform.position) < 20 && angle <30) {

			Quaternion targetRotation = Quaternion.LookRotation(direction);
			this.transform.rotation=  Quaternion.Slerp(this.transform.rotation,targetRotation, 0.1f);

			anim.SetBool ("isIdle", false);
			if (direction.magnitude > 5) {
				this.transform.Translate (0, 0, 0.05f);
				anim.SetBool ("isRunning", true);
				anim.SetBool ("isShooting", false);	
			} else {
				anim.SetBool ("isShooting", true);
				anim.SetBool ("isRunning", false);
			}


		} else {
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isShooting", false);
		}*/

		
	}
}