using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraController : Application {

	public GameObject camera_limit;

	private RectTransform cameraLimitRectangle;
	private float minX = 0f;
	private float maxX = 0f;
	private float offsetY = 0f;
	private VignetteAndChromaticAberration vignetteEffect;
	private Grayscale grayscaleEffect;

	protected override void Start () {
		base.Start ();
		GetCameraLimitValues ();

		offsetY = transform.position.y - player.transform.position.y;

		if (gameObject.tag == "MainCamera") {
			vignetteEffect = gameObject.GetComponent<VignetteAndChromaticAberration> ();
			grayscaleEffect = gameObject.GetComponent<Grayscale> ();
		}
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
		Vector3 newPosition = transform.position;

		if (player.transform.position.x < minX) {
			newPosition.x = minX;
		} else if (player.transform.position.x > maxX) {
			newPosition.x = maxX;
		} else {
			newPosition.x = player.transform.position.x;
		}
		newPosition.y = player.transform.position.y + offsetY;

		transform.position = newPosition;
	}

	// Enable vortex & saturate effects
	// Tween their values
	public void ToggleUpSideDownEffect () {
		vignetteEffect.enabled = !vignetteEffect.enabled;
		grayscaleEffect.enabled = !grayscaleEffect.enabled;

		if (vignetteEffect.enabled) {
			iTween.ValueTo(new GameObject(), iTween.Hash(
				"name", "vortex_angle_tween",
				"from", 0.32f,
				"to", 0.37f,
				"time", 0.5,
				"onupdate", "SetVignetteValue",
				"easetype", "linear",
				"looptype", "pingPong"
			));

			iTween.ValueTo(new GameObject(), iTween.Hash(
				"name", "saturate_value_tween",
				"from", 0.3f,
				"to", 0.2f,
				"time", 2,
				"onupdate", "SetGrayscaleValue",
				"easetype", "linear",
				"looptype", "pingPong"
			));
		} else {
			iTween.StopByName ("vortex_angle_tween");
			iTween.StopByName ("saturate_value_tween");
		}
	}

	void SetVignetteValue(float value) {
		Debug.Log ("set vignette effect " + value);
		vignetteEffect.intensity = value;
	}

	void SetGrayscaleValue(float value) {
		grayscaleEffect.rampOffset = value;
	}

}