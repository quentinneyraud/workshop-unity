using UnityEngine;
using System.Collections;

public class jewel : MonoBehaviour {

	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		jewelFloatingeffect ();
	}

	void jewelFloatingeffect() {
		Vector3 newPosition = startPosition;
		newPosition.y = startPosition.y + 0.3f * Mathf.Sin(4f * Time.time);
		iTween.MoveUpdate (this.gameObject, newPosition, 4f);
	}
}
