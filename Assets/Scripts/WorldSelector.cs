using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;

public class WorldSelector : Application {

	private Image[] images;
	private float worldHeight = 10f;
	private int currentWorld = -1;
	private CameraController cameraController;

	// Use this for initialization
	void Start () {
		base.Start ();
		worldHeight = 10f;
		images = GetComponentsInChildren<Image> ();
		cameraController = camera.GetComponent<CameraController> ();

		TweenSelectorAlpha (0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		ListenMouseDown ();
		/*if (Input.GetMouseButtonUp (0)) {
			
			iTween.MoveTo (player, iTween.Hash(
				"y", player.transform.position.y + worldHeight * currentWorld,
				"time", 2f
			));
			ToggleCurrentWorld ();

			foreach (Image image in images) {
				iTween.ScaleTo (image.gameObject, new Vector3(1f, 1f, 1), 1f);
			}
			cameraController.ToggleUpSideDownEffect ();
		}*/
	}

	void ToggleCurrentWorld () {
		currentWorld *= -1;
	}

	void ListenMouseDown () {
		if (Input.GetMouseButton(0)) {
			SetTimeScale (0.5f);
			TweenSelectorAlpha (1f, 0.5f);
			SetAudioPitch (0.8f);
			foreach (Image image in images) {
				//iTween.ScaleTo (image.gameObject, new Vector3(3f, 3f, 1), 0.5f);
			}

		} else {
			TweenSelectorAlpha (0f, 0);
			SetTimeScale (1);
			SetAudioPitch (1);

			Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
			mouse = Camera.main.ScreenToWorldPoint(mouse);
			SetSelectorPosition (mouse.x, mouse.y);
		}
	}

	void TweenSelectorAlpha (float alpha, float time) {
		foreach (Image image in images) {
			image.CrossFadeAlpha(alpha, time, true);
		}
	}

	void SetSelectorPosition (float x, float y) {
		foreach (Image image in images) {
			image.transform.position = new Vector3(x, y, 0);
		}
	}

	void SetTimeScale(float timeScale) {
		Time.timeScale = timeScale;
	}

	void SetAudioPitch (float pitch) {
		ambiantAudio.pitch = pitch;
	}


}
