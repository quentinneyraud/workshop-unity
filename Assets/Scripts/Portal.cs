﻿using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "player") {
			GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");

			camera.GetComponent<CameraController> ().ToggleDrugEffect ();
		}
	}
}