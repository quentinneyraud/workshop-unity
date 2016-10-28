using UnityEngine;
using System.Collections;

public class Jewel : Application {

	private Vector3 startPosition;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		startPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		SetjewelFloatingEffect ();
	}

	void SetjewelFloatingEffect() {
		Vector3 newPosition = startPosition;
		newPosition.y = startPosition.y + 0.3f * Mathf.Sin(4f * Time.time);
		iTween.MoveUpdate (this.gameObject, newPosition, 4f);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "player") {
			Destroy (this.gameObject);
		}
	}
}
