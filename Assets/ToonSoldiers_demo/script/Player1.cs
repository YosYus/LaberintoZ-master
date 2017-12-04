using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {
	
	//Variables públicas
	 public float velocidad = 7f;
	 public float velGiro = 12f;
	 
	 
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
		Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation,targetRotation, velGiro * Time.deltaTime);
		GetComponent<Rigidbody> ().MoveRotation(newRotation);
		
		
	}
	
	
	void animaPlayer(float hor, float ver){
		bool correPlayer =hor != 0f || ver != 0f;
		anim.SetBool("corriendo",correPlayer);
		
	}
}