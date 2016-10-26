using UnityEngine;
using System.Collections;

public class Hero : Character {

	Animator animator;
	Rigidbody2D rigidBody;
	public float WalkSpeed;
	private bool doubleJumpAuthorize = true;

	bool isWalking = false;
	public bool IsWalking {
		get { 
			return isWalking; 
		}

		set {
			if (isWalking == value)
				return;
			
			isWalking = value;

			if (!IsJumping) {
				animator.SetBool ("isWalking", isWalking == true);
			}
		}
	}

	bool isCrouching = false;
	public bool IsCrouching {
		get { 
			return isCrouching; 
		}

		set { 
			if (isCrouching == value)
				return;

			isCrouching = value;

			if (!IsJumping) {
				animator.SetBool ("isCrouching", isCrouching == true);
			}
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

			if (isJumping && !IsCrouching) {
				if (YVelocity > -0.5 && YVelocity < 0.5) {
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
	void Start () {
		animator = GetComponent<Animator>();
		rigidBody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		IsJumping = Input.GetKey (KeyCode.UpArrow);
		IsCrouching = Input.GetKey (KeyCode.DownArrow);
		IsWalking = Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow);
		Direction = detectDirection ();
	}

	int detectDirection () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			return -1;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			return 1;
		} else {
			return 0;
		}
	}
 
	void FixedUpdate () {
		YVelocity = rigidBody.velocity.y;
		move ();
	}

	void move (){
		Vector2 tmpVelocity = rigidBody.velocity;

		if (Direction != 0 && !IsCrouching) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				tmpVelocity.x = WalkSpeed * Direction * Time.fixedDeltaTime * 2;
			} else {
				tmpVelocity.x = WalkSpeed * Direction * Time.fixedDeltaTime;
			}
		} else {
			tmpVelocity.x = 0;
		}

		rigidBody.velocity = tmpVelocity;
	}

	void OnCollisionEnter2D(Collision2D col){
		switch (col.gameObject.tag)
		{
		case "terrain":
			OnTerrainCollision (col);
			break;
		case "jewel":
			OnJewelCollision (col);
			break;
		case "monster":
			OnMonsterCollision (col);
			break;
		default:
			break;
		}
	}

	void OnTerrainCollision(Collision2D col) {
		Vector2 normal = col.contacts [0].normal;

		if (normal.x > -0.7 && normal.x < 0.7) {
			return;
		}

		Vector2 newVelocity = rigidBody.velocity;
		newVelocity.y = -5f;
		newVelocity.x = (normal.x > 0.7) ? -3f : 3f;
		rigidBody.velocity = newVelocity;
	}

	void OnJewelCollision(Collision2D col) {
		GameObject jewel = col.gameObject;

		Destroy (jewel);
	}

	void OnMonsterCollision(Collision2D col) {

	}

	/*void OnCollisionEnter2D (Collision2D col)
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
	}*/
}
