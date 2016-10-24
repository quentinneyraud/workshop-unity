using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public GameObject terrain;

	private float minX = 0f;
	private float maxX = 0f;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
		minX = terrain.transform.position.x;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate() {
		Vector3 newPosition = player.transform.position + offset;

		/*if (newPosition.x < minX) {
			newPosition.x = minX;
		} else if (newPosition.x > maxX) {
			newPosition.x = maxX;
		}
*/
		transform.position = newPosition;
	}
}
