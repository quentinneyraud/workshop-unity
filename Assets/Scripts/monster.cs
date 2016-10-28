using UnityEngine;
using System.Collections;

public class Monster : Application {

	public float distance;
	public float speed;

	private Rigidbody2D rigidBody;
	private Animator animator;
	private float direction = 1;
	private float xMax;
	private float xMin;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		rigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();

		xMax = transform.position.x + distance;
		xMin = transform.position.x - distance;
	}

	// Update is called once per frame
	void Update () {
		Vector2 tmpVelocity = rigidBody.velocity;
		tmpVelocity.x = direction * speed * Time.deltaTime;
		rigidBody.velocity = tmpVelocity;

		if ((transform.position.x > xMax && direction == 1) || (transform.position.x < xMin && direction == -1)) {
			Flip ();
		}
	}

	void Flip () {
		Vector3 scale = transform.localScale;
		scale.x = - scale.x;
		transform.localScale = scale;

		direction = - direction;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "player") {

			Vector2 normal = col.contacts [0].normal;

			if (normal.y < -0.5) {
				StartCoroutine (Kill ());
			}
		}
	}

	IEnumerator Kill() {
		rigidBody.AddForce (new Vector3 (0, 200));
		animator.SetBool ("isDead", true);
		GetComponent<Collider2D> ().enabled = false;
		iTween.FadeTo (this.gameObject, 0f, 1f);

		yield return new WaitForSeconds (1);
		Destroy (this.gameObject);
	}
}