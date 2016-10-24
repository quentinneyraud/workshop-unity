using UnityEngine;
using System.Collections;

public class Paralax : MonoBehaviour {

	public GameObject camera;
	public float force;

	private float cameraX;

	// Use this for initialization
	void Start () {
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
