using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraController : MonoBehaviour {

	public GameObject object_to_follow;
	public GameObject camera_limit;

	private RectTransform cameraLimitRectangle;
	private Camera camera;
	private float minX = 0f;
	private float maxX = 0f;
	private float offsetY = 0f;
	private float previousY = 0f;
	private Vortex vortexEffect;
	private ColorCorrectionCurves saturateEffect;

	void Start () {
		GetCameraLimitValues ();
		SetPreviousY ();

		offsetY = transform.position.y - object_to_follow.transform.position.y;
		vortexEffect = gameObject.GetComponent<Vortex> ();
		saturateEffect = gameObject.GetComponent<ColorCorrectionCurves> ();
	}

	void Update () {

	}

	void LateUpdate() {
		UpdateCameraPosition ();
	}

	// set camera min & max values for x property using camera_limit GameObject
	void GetCameraLimitValues () {
		camera = GetComponent<Camera> ();
		float width = 2f * camera.orthographicSize * camera.aspect;
		cameraLimitRectangle = camera_limit.GetComponent<RectTransform> ();

		minX = camera_limit.transform.position.x + width / 2;
		maxX = camera_limit.transform.position.x + cameraLimitRectangle.rect.width - width / 2;
	}

	// Set x camera position using object_to_follow x position
	// Tween y camera position
	void UpdateCameraPosition (){
		
		UpdateCameraXPosition ();
		TweenCameraYPosition ();
	
		SetPreviousY ();
	}

	void UpdateCameraXPosition () {
		Vector3 newPosition = transform.position;

		if (object_to_follow.transform.position.x < minX) {
			newPosition.x = minX;
		} else if (object_to_follow.transform.position.x > maxX) {
			newPosition.x = maxX;
		} else {
			newPosition.x = object_to_follow.transform.position.x;
		}

		transform.position = newPosition;
	}

	void TweenCameraYPosition () {
		float objectToFollowYPositionDifference = object_to_follow.transform.position.y - previousY;

		iTween.MoveUpdate (this.gameObject, iTween.Hash (
			"y", transform.position.y + objectToFollowYPositionDifference / 2, 
			"time", 0.2f, 
			"delay", 0.1f
		));
	}

	// Keep previous following object y
	void SetPreviousY () {
		previousY = object_to_follow.transform.position.y;
	}

	// Enable vortex & saturate effects
	// Tween their values
	public void ToggleDrugEffect () {
		saturateEffect.enabled = !saturateEffect.enabled;
		vortexEffect.enabled = !vortexEffect.enabled;

		if (saturateEffect.enabled) {
			iTween.ValueTo(new GameObject(), iTween.Hash(
				"name", "vortex_angle_tween",
				"from", -75,
				"to", 75,
				"time", 3,
				"onupdate", "SetVortexAngle",
				"easetype", "linear",
				"looptype", "pingPong"
			));

			iTween.ValueTo(new GameObject(), iTween.Hash(
				"name", "saturate_value_tween",
				"from", 2,
				"to", 5,
				"time", 1,
				"onupdate", "SetSaturationValue",
				"easetype", "linear",
				"looptype", "pingPong"
			));
		} else {
			iTween.StopByName ("vortex_angle_tween");
			iTween.StopByName ("saturate_value_tween");
		}
	}

	void SetVortexAngle(float value) {
		vortexEffect.angle = value;
	}

	void SetSaturationValue(float value) {
		saturateEffect.saturation = value;
	}

}