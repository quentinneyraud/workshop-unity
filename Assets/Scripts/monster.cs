using UnityEngine;
using System.Collections;

public class monster : Character {

	public float speed;
	private Rigidbody2D rigidBody;
	private Animator animator;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		Vector2 tmpVelocity = rigidBody.velocity;
		tmpVelocity.x = -20 * Time.deltaTime;
		rigidBody.velocity = tmpVelocity;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "player") {

			Vector2 normal = col.contacts [0].normal;

			if (normal.y < -0.5) {
				rigidBody.AddForce (new Vector3 (0, 200));
				animator.SetBool ("isDead", true);
				GetComponent<Collider2D> ().enabled = false;
				iTween.FadeTo (this.gameObject, 0f, 1f);
			}
		}
	}
}