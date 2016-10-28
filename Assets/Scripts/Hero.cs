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
	private AudioSource walkSound;
	//private AudioSource jumpSound;

	bool isWalking = false;
	public bool IsWalking {
		get { 
			return isWalking; 
		}

		set {
			if (!IsGrounded())
				return;

			isWalking = value;

			if (isWalking) {
				walkSound.loop = true;
				walkSound.Play ();
			} else {
				walkSound.Stop ();
			}

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

	bool isHurt = false;
	public bool IsHurt {
		get { 
			return isHurt; 
		}

		set { 
			if (isHurt == value)
				return;

			isHurt = value;

			if (isHurt) {
				StartCoroutine (removeIsHurt());
			}

			animator.SetBool ("isHurt", isHurt == true);
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
		base.Start ();
		animator = GetComponent<Animator>();
		rigidBody = GetComponent<Rigidbody2D>();
		walkSound = GetComponents<AudioSource> ()[0];
		//jumpSound = GetComponents<AudioSource> ()[1];
	}

	// Update is called once per frame
	void Update () {
		IsJumping = Input.GetKey (KeyCode.UpArrow);
		IsCrouching = Input.GetKey (KeyCode.DownArrow);
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

		if (Direction != 0 && !IsCrouching) {
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
		case "terrain":
			OnTerrainCollision (col);
			break;
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

	void OnTerrainCollision(Collision2D col) {
		/*Vector2 normal = col.contacts [0].normal;

		if (normal.x > -0.7 && normal.x < 0.7) {
			return;
		}

		Vector2 newVelocity = rigidBody.velocity;
		newVelocity.y = -5f;
		newVelocity.x = (normal.x > 0.7) ? -3f : 3f;
		rigidBody.velocity = newVelocity;*/
	}

	void OnJewelCollision(Collider2D col) {
		jewelCollected++;
		UpdateJewelIndicator ();
	}

	void OnMonsterCollision(Collision2D col) {
		if (col.gameObject.tag == "monster") {

			Vector2 normal = col.contacts [0].normal;

			if (normal.y > -0.5) {
				rigidBody.AddForce (new Vector3(-1000, 250, 0));
				IsHurt = true;
				StartCoroutine (FlashOnHurt());
			}
		}
	}

	void UpdateJewelIndicator () {
		string name = "jewel-indicator-grayscale-" + jewelCollected;
		jewel_meter.transform.Find(name).gameObject.SetActive(false);
	}

	IEnumerator removeIsHurt() {
		yield return new WaitForSeconds (0.7f);
		IsHurt = false;
	}

	bool IsGrounded () {
		return YVelocity > -0.2 && YVelocity < 0.2; 
	}

	IEnumerator FlashOnHurt () {
		iTween.FadeTo (this.gameObject, iTween.Hash(
			"name", "player_flash",
			"alpha", 0,
			"time", 0.1f,
			"easetype", "linear",
			"looptype", "pingPong"
		));

		yield return new WaitForSeconds (0.5f);

		iTween.StopByName ("player_flash");
		iTween.FadeTo (this.gameObject, iTween.Hash(
			"alpha", 1,
			"time", 0.1f,
			"easetype", "linear"
		));
	}
}