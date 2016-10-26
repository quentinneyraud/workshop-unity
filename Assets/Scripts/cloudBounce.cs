using UnityEngine;
using System.Collections;

public class cloudBounce : MonoBehaviour {

	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		cloudFloatingeffect ();
	}

	void cloudFloatingeffect() {
		Vector3 newPosition = startPosition;
		newPosition.y = startPosition.y + 1f * Mathf.Sin(1f * Time.time);
		newPosition.x = transform.position.x + 5f * Mathf.Sin (1f * Time.time);
		iTween.MoveUpdate (this.gameObject, iTween.Hash("position", newPosition, "time", 20f));
	}
}
