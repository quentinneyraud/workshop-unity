using UnityEngine;
using System.Collections;

public class monster : Character {

	public GameObject target;
	public float speed;
	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.LookAt (target.transform);

		Vector2 tmpVelocity = rigidBody.velocity;

		tmpVelocity.x = -20 * Time.deltaTime;

		rigidBody.velocity = tmpVelocity;
	}
}
