using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject object_to_follow;
	public GameObject camera_limit;

	private RectTransform cameraLimitRectangle;
	Camera camera;
	private float minX = 0f;
	private float maxX = 0f;
	private float offsetY = 0f;
	private float previousY = 0f;

	// Use this for initialization
	void Start () {
		this.getCameraLimitValues ();
		offsetY = transform.position.y - object_to_follow.transform.position.y;
		setPreviousY ();
	}

	// Update is called once per frame
	void Update () {

	}

	void LateUpdate() {
		this.updateCameraPosition ();
	}

	void getCameraLimitValues () {
		camera = GetComponent<Camera> ();
		float width = 2f * camera.orthographicSize * camera.aspect;
		cameraLimitRectangle = camera_limit.GetComponent<RectTransform> ();

		minX = camera_limit.transform.position.x + width / 2;
		maxX = camera_limit.transform.position.x + cameraLimitRectangle.rect.width - width / 2;
	}

	void updateCameraPosition (){
		Vector3 newPosition = transform.position;

		//newPosition.y = transform.position.y + (object_to_follow.transform.position.y - previousY) / 2;

		if (object_to_follow.transform.position.x < minX) {
			newPosition.x = minX;
		} else if (object_to_follow.transform.position.x > maxX) {
			newPosition.x = maxX;
		} else {
			newPosition.x = object_to_follow.transform.position.x;
		}

		// set x position
		transform.position = newPosition;
			
		// tween y position
		iTween.StopByName("camera_y");
		iTween.MoveUpdate (this.gameObject, iTween.Hash ("name", "camera_y", "y", transform.position.y + (object_to_follow.transform.position.y - previousY) / 2, "time", 0.2f, "delay", 0.1f));

		setPreviousY ();
	}

	void setPreviousY () {
		previousY = object_to_follow.transform.position.y;
	}

}