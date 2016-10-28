using UnityEngine;
using System.Collections;

public class Portal : Application {

	public GameObject to;

	private AudioSource[] audioSources;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		audioSources = GetComponents<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "player" && to) {
			player.transform.position = to.transform.position;
			worldManager.ToggleWorld ();
			startPortalSound ();
		}
	}

	public void startPortalSound() {
		audioSources [0].Play ();
	}
}
