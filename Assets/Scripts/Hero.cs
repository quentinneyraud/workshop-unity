using UnityEngine;
using System.Collections;

public class Hero : Application {

	public float walk_speed;
	public float run_speed;
	public GameObject jewel_meter;

	private Animator animator;
	private Rigidbody2D rigidBody;
	private bool doubleJumpAuthorize = true;
	private int jewelCollected = 0;

	bool isWalking = false;
	public bool IsWalking {
		get { 
			return isWalking; 
		}

		set {
			if (!IsGrounded())
				return;

			isWalking = value;

			animator.SetBool ("isWalking", isWalking == true);
		}
	}

	bool isJumping = false;
	public bool IsJumping {
		get { 
			return isJumping; 
		}

		set { 
			if (isJumping == value)
				return;

			isJumping = value;

			if (isJumping) {
				IsWalking = false;
				if (IsGrounded()) {
					rigidBody.AddForce (new Vector3 (0, 200));
					doubleJumpAuthorize = true;
				} else {
					if (doubleJumpAuthorize) {
						doubleJumpAuthorize = false;
						rigidBody.AddForce (new Vector3 (0, 200));
					}
				}
			}
				
			animator.SetBool ("isJumping", isJumping == true);
		}
	}

	bool isDead = false;
	public bool IsDead {
		get { 
			return isDead; 
		}

		set { 
			if (isDead == value)
				return;

			isDead = value;

			animator.SetBool ("isDead", isDead == true);
		}
	}

	int direction = 0;
	public int Direction {
		get { 
			return direction; 
		}

		set { 
			if (direction == value)
				return;

			direction = value;

			if (direction != 0) {
				Vector3 newScale;
				newScale = transform.localScale;
				newScale.x = (direction != 1) ? -0.5f : 0.5f;

				transform.localScale = newScale;
			}
		}
	}

	float yVelocity = 0;
	public float YVelocity {
		get { 
			return yVelocity; 
		}

		set { 
			if (yVelocity == value)
				return;

			yVelocity = value;

			animator.SetFloat ("yVelocity", yVelocity);
		}
	}


	// Use this for initialization
	protected override void Start () {
		base.Start ();
		animator = GetComponent<Animator>();
		rigidBody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		IsJumping = Input.GetKey (KeyCode.UpArrow);
		IsWalking = Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow);
		Direction = DetectDirection ();
	}

	void FixedUpdate () {
		YVelocity = rigidBody.velocity.y;
		Move ();
	}

	int DetectDirection () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			return -1;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			return 1;
		} else {
			return 0;
		}
	}

	void Move (){
		Vector2 tmpVelocity = rigidBody.velocity;

		if (Direction != 0) {
			tmpVelocity.x = Direction * Time.fixedDeltaTime;
			tmpVelocity.x *= (Input.GetKey (KeyCode.LeftShift)) ? run_speed : walk_speed;
		} else {
			tmpVelocity.x = 0;
		}

		rigidBody.velocity = tmpVelocity;
	}

	void OnCollisionEnter2D(Collision2D col){
		switch (col.gameObject.tag)
		{
		case "monster":
			OnMonsterCollision (col);
			break;
		default:
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		switch (col.gameObject.tag)
		{
		case "jewel":
			OnJewelCollision (col);
			break;
		default:
			break;
		}
	}

	void OnJewelCollision(Collider2D col) {
		jewelCollected++;
		UpdateJewelIndicator ();
	}

	void OnMonsterCollision(Collision2D col) {
		if (col.contacts [0].normal.y < 0.5) {
			StartCoroutine (OnDead());
		}
	}

	void UpdateJewelIndicator () {
		string name = "jewel-indicator-grayscale-" + jewelCollected;
		jewel_meter.transform.Find(name).gameObject.SetActive(false);
		if (jewelCollected == 5) {
			StartCoroutine (base.OnGameEnd());
		}
	}

	bool IsGrounded () {
		return YVelocity > -0.2 && YVelocity < 0.2; 
	}

	IEnumerator OnDead () {

		IsDead = true;

		yield return new WaitForSeconds (2f);

		UnityEngine.Application.LoadLevel (UnityEngine.Application.loadedLevel);
	}
}