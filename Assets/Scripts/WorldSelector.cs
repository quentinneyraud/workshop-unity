using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;

public class WorldSelector : Application {

	private Image[] images;
	private float worldHeight;
	private float playerStartPositionY;
	Ray ray;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		base.Start ();
		worldHeight = 10f;
		playerStartPositionY = player.transform.position.y;
		images = GetComponentsInChildren<Image> ();
		TweenSelectorAlpha (0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		ListenMouseDown ();
		if (Input.GetMouseButtonUp (0)) {
			
			iTween.MoveTo (player, iTween.Hash(
				"y", playerStartPositionY - worldHeight,
				"time", 2f
			));

			foreach (Image image in images) {
				iTween.ScaleTo (image.gameObject, new Vector3(1f, 1f, 1), 1f);
			}
		}

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit))
		{
			print (hit.collider.name);
		}
	}

	void OnMouseOver()
	{
		print (gameObject.name);
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

	void OnTriggerEnter2d (Collider2D col) {
		Debug.Log("enter");
	}
}
