using UnityEngine;
using System.Collections;

public class ParalaxEffect : MonoBehaviour {

	public float force;

	private GameObject camera;
	private float cameraX;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		cameraX = camera.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate (){
		Vector3 tmpPosition = transform.position;

		tmpPosition.x -= (camera.transform.position.x - cameraX) * force;

		transform.position = tmpPosition;

		cameraX = camera.transform.position.x;
	}
}
