using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	Animator animator;
	Rigidbody2D rigidBody;
	public float WalkSpeed;


	bool isGrounded = true;
	public bool IsGrounded {
		get { 
			return isGrounded;
		}

		set { 
			if (isGrounded == value)
				return;

			animator.SetBool ("isGrounded", isGrounded == true);
		}
	}

	bool isWalking = false;
	public bool IsWalking {
		get { 
			return isWalking; 
		}

		set { 
			if (isWalking == value)
				return;
			
			isWalking = value;

			animator.SetBool ("isWalking", isWalking == true);
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

			animator.SetBool ("isCrouching", isCrouching == true);
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

			animator.SetBool ("isJumping", isJumping == true);

			if (isJumping && isGrounded) {
				rigidBody.AddForce (new Vector3 (0, 250));
			}
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



			GetComponent<SpriteRenderer> ().flipX = (direction != 0) ? direction == -1 : false;
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


		if (Input.GetKey (KeyCode.LeftArrow)) {
			Direction = -1;
			IsWalking = true;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			IsWalking = true;
			Direction = 1;
		} else {
			IsWalking = false;
			Direction = 0;
		}
	}

	void FixedUpdate () {
		Vector2 tmpVelocity = rigidBody.velocity;
		animator.SetInteger ("velocity", (int) tmpVelocity.y);
		if (tmpVelocity.y == 0) {
			IsGrounded = true;
		}

		if (Direction != 0 && !IsCrouching) {
			tmpVelocity.x = WalkSpeed * Direction * Time.fixedDeltaTime;
		}

		rigidBody.velocity = tmpVelocity;
	}

	void onCollisionEnter2D(Collision2D collision){
		IsGrounded = true;
		IsJumping = false;
	}
}
